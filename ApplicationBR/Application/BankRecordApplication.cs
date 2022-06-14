using ApplicationBR.Interfaces;
using AutoMapper;
using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using BankRecordDomain.Models;
using BankRecordDomain.Validators;
using DataBankRecord.Repository;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApplicationBR.Application
{
    public class BankRecordApplication : IBankRecordApplication
    {
        private readonly IMapper _mapper;
        private readonly IBankRecordRepository _bankRecordRepository;

        public BankRecordApplication(IMapper mapper, IBankRecordRepository bankRecordRepository)
        {
            _mapper = mapper;
            _bankRecordRepository = bankRecordRepository;
        }

        public async Task AddBankRecord(BankRecordDTO bankRecord)
        {
            var bankRecordMapper = _mapper.Map<BankRecord>(bankRecord);

            var validator = new BankRecordValidator();
            var validationResult = validator.Validate(bankRecordMapper);

            if (validationResult.IsValid)
                await _bankRecordRepository.AddAsync(bankRecordMapper);
            else
            {
                var errorList = new ErrorMessage<BankRecordDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    validationResult.Errors.ConvertAll(x => x.ErrorMessage), bankRecord);
                throw new Exception(ErrorList(errorList));
            }
        }
        public async Task<BankRecordModel> GetAllBankRecord(PageParamenters pageParamenters)
        {
            BankRecordModel viewModel = new BankRecordModel();

            viewModel.BankRecords = await _bankRecordRepository.GetAllWithPaging(pageParamenters);

            if (viewModel.BankRecords.Count == 0)
                throw new Exception(ErrorList(NotFoundMessage(new BankRecordDTO())));

            return viewModel;
        }

        
        public async Task<BankRecord> GetBankRecordById(Guid id)
        {
            var result = await _bankRecordRepository.GetByIdAsync(id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new BankRecordDTO())));

            return result;
        }

        
        public async Task<BankRecord> GetBankRecordByOriginId(Guid id)
        {
            var result = await _bankRecordRepository.GetAsync(x => x.OriginId == id);

            if (result == null)
                throw new Exception(ErrorList(NotFoundMessage(new BankRecordDTO())));

            return result;
        }

        
        public async Task<BankRecord> PutBankRecord(Guid id, BankRecordDTO bankRecord)
        {
            var bankRecordUpdate = await _bankRecordRepository.GetByIdAsync(id);

            if (bankRecordUpdate == null)
                throw new Exception(ErrorList(NotFoundMessage(bankRecord)));

            if (bankRecordUpdate.OriginId != null)
                throw new Exception(ErrorList(BadRequestMessage(bankRecord, "You don´t have permissions to change this data....")));

            var mapBankRecord = _mapper.Map<BankRecord>(bankRecord);
            mapBankRecord.Id = id;

            var validator = new BankRecordValidator();
            var validationResult = validator.Validate(mapBankRecord);

            if (validationResult.IsValid)
                await _bankRecordRepository.UpdateAsync(mapBankRecord);
            else
            {
                var errorList = new ErrorMessage<BankRecordDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                 validationResult.Errors.ConvertAll(x => x.ErrorMessage.ToString()), bankRecord);
                throw new Exception(ErrorList(errorList));
            }

            return mapBankRecord;
        }
        public string ErrorList(ErrorMessage<BankRecordDTO> error)
        {
            var errorList = "";

            foreach (var item in error.Message)
            {
                errorList += item.ToString();
            }
            return errorList;
        }
        public ErrorMessage<BankRecordDTO> NotFoundMessage(BankRecordDTO bankRecord)
        {
            var errorList = new List<string>();
            errorList.Add("In database doesn´t contain the data you want....");
            var error = new ErrorMessage<BankRecordDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, bankRecord);
            return error;
        } 
        public ErrorMessage<BankRecordDTO> BadRequestMessage(BankRecordDTO bankRecord, string message)
        {
            var errorList = new List<string>();
            errorList.Add(message);
            var error = new ErrorMessage<BankRecordDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, bankRecord);
            return error;
        }
    }
}
