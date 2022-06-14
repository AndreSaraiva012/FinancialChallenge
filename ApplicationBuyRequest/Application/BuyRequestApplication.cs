using ApplicationBuyRequest.Interfaces;
using AutoMapper;
using BankRecordAPIClient;
using BankRecordDomain.Entities;
using BuyRequestDomain.DTO_s;
using BuyRequestDomain.Entities;
using BuyRequestDomain.Validators;
using DataBuyRequest.Repository;
using DataBuyRequest.Repository.ProductRequestRepository;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApplicationBuyRequest.Application
{
    public class BuyRequestApplication : IBuyRequestApplication
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _requestRepository;
        private readonly IBankRecordClient _bankRecordClient;
        private readonly IProductRequestRepository _productRequestRepository;

        public BuyRequestApplication(IMapper mapper, IRequestRepository requestRepository, IBankRecordClient bankRecordClient, IProductRequestRepository productRequestRepository)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _bankRecordClient = bankRecordClient;
            _productRequestRepository = productRequestRepository;
        }

        public async Task<BuyRequest> AddBuyRequest(BuyRequestDTO buyRequest)
        {
            var maprequest = _mapper.Map<BuyRequest>(buyRequest);

            var buyValidator = new BuyRequestValidator();
            var validationResult = buyValidator.Validate(maprequest);

            if (validationResult.IsValid)
                await _requestRepository.AddAsync(maprequest);
            else
            {
                var error = ErrorList(new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), validationResult.Errors.ConvertAll(x => x.ErrorMessage.ToString()), buyRequest));
                throw new Exception(error);
            }

            return maprequest;
        }

        public async Task<List<BuyRequest>> GetAllBuyRequest(PageParamenters pageParameters)
        {
            var result = await _requestRepository.GetAllWithPaging(pageParameters);

            if (result.Count() == 0)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            return result;
        }

        public async Task<BuyRequest> GetBuyRequestsById(Guid id)
        {
            var result = await _requestRepository.GetByIdAsync(id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            return result;
        }

        public async Task<BuyRequest> GetBuyRequestsByClient(Guid ClientId)
        {
            var result = await _requestRepository.GetAsync(x => x.Client == ClientId);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            return result;
        }

        public async Task<BuyRequest> UpdateBuyRequest(BuyRequestDTO buyRequest)
        {
            var findRequest = await _requestRepository.GetByIdAsync(buyRequest.Id);

            if (findRequest == null)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            if (findRequest.Status == Status.Concluded && buyRequest.Status != Status.Concluded)
                throw new Exception(ErrorList(BadRequestMessage(buyRequest, "You can only delete this Request....")));

            var maprequest = _mapper.Map<BuyRequest>(buyRequest);

            var buyValidator = new BuyRequestValidator();
            var validationResult = buyValidator.Validate(maprequest);

            if (validationResult.IsValid)
            {
                await _requestRepository.UpdateAsync(maprequest);

                var productsToDelete = findRequest.Products.Where(x => !maprequest.Products.Any(z => z.Id == x.Id)).ToList();

                foreach (var item in productsToDelete)
                {
                    var result = await _productRequestRepository.GetByIdAsync(item.Id);
                    await _productRequestRepository.DeleteAsync(result);
                }

            }
            else
            {
                var errorList = new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                validationResult.Errors.ConvertAll(x => x.ErrorMessage.ToString()), buyRequest);
                throw new Exception(ErrorList(errorList));
            }

            //Communication
            if (maprequest.Status == Status.Concluded)
            {

                if (maprequest.Status == findRequest.Status && maprequest.Status == Status.Concluded && findRequest.TotalValue > maprequest.TotalValue)
                {
                    var messageBody = _bankRecordClient.MessageBody(Origin.Document, buyRequest.Id, $"Diference purchase (Pay diference) (id: {buyRequest.Id})", BankRecordDomain.Entities.Type.Payment, -(findRequest.TotalValue - maprequest.TotalValue));

                    await _bankRecordClient.PostCashBank(messageBody);
                }
                else if (maprequest.Status == findRequest.Status && maprequest.Status == Status.Concluded && findRequest.TotalValue < maprequest.TotalValue)
                {
                    var messageBody = _bankRecordClient.MessageBody(Origin.Document, buyRequest.Id, $"Diference purchase (Receive diference) (id: {buyRequest.Id})", BankRecordDomain.Entities.Type.Receive, (maprequest.TotalValue - findRequest.TotalValue));

                    await _bankRecordClient.PostCashBank(messageBody);
                }
                else
                {
                    var messageBody = _bankRecordClient.MessageBody(Origin.PurchaseRequest, buyRequest.Id, $"Financial Transaction (id: {buyRequest.Id})", BankRecordDomain.Entities.Type.Receive, maprequest.TotalValue);

                    await _bankRecordClient.PostCashBank(messageBody);
                }
            }
            return maprequest;
        }

        public async Task<BuyRequest> UpdateBuyRequestStatus(Guid id, Status state)
        {
            var requestUpdate = await _requestRepository.GetByIdAsync(id);
            if (requestUpdate == null)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            if (requestUpdate.Status == Status.Concluded)
                throw new Exception(ErrorList(BadRequestMessage(new BuyRequestDTO(), "You can only delete this Request....")));

            requestUpdate.Status = state;

            await _requestRepository.UpdateAsync(requestUpdate);

            if (requestUpdate.Status == Status.Concluded)
            {
                var messageBody = _bankRecordClient.MessageBody(Origin.PurchaseRequest, requestUpdate.Id, $"Financial Transaction (id: {requestUpdate.Id})", BankRecordDomain.Entities.Type.Receive, requestUpdate.TotalValue);
                await _bankRecordClient.PostCashBank(messageBody);
            }
            return requestUpdate;
        }

        public async Task<BuyRequest> DeleteBuyRequest(Guid id)
        {
            var result = await _requestRepository.GetByIdAsync(id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new BuyRequestDTO())));

            await _requestRepository.DeleteAsync(result);

            if (result.Status == Status.Concluded)
            {

                var messageBody = _bankRecordClient.MessageBody(Origin.Document, result.Id, $"Revert Purshase order id: {result.Id}", BankRecordDomain.Entities.Type.Revert, -result.TotalValue);

                await _bankRecordClient.PostCashBank(messageBody);
            }

            return result;
        }

        public string ErrorList(ErrorMessage<BuyRequestDTO> error)
        {
            var errorList = "";

            foreach (var item in error.Message)
            {
                errorList += item.ToString() + " ";
            }
            return errorList;
        }
        public ErrorMessage<BuyRequestDTO> NotFoundMessage(BuyRequestDTO buyRequest)
        {
            var errorList = new List<string>();
            errorList.Add("In database doesn´t contain the data you want....");
            var error = new ErrorMessage<BuyRequestDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, buyRequest);
            return error;
        }
        public ErrorMessage<BuyRequestDTO> BadRequestMessage(BuyRequestDTO buyRequest, string message)
        {
            var errorList = new List<string>();
            errorList.Add(message);
            var error = new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, buyRequest);
            return error;
        }
    }
}
