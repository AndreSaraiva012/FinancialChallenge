using AutoMapper;
using BankRecordAPIClient;
using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using DocumentApplication.Interfaces;
using DocumentData.Repository;
using DocumentDomain.DTO_s;
using DocumentDomain.Entities;
using DocumentDomain.Validators;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DocumentApplication.Application
{
    public class DocumentApplication : IDocumentApplication
    {
        private readonly IMapper _mapper;
        private readonly IDocumentRepository _documentRepository;
        private readonly IBankRecordClient _bankRecordClient;

        public DocumentApplication(IMapper mapper, IDocumentRepository documentRepository, IBankRecordClient bankRecordClient)
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
            _bankRecordClient = bankRecordClient;
        }

        public async Task<Document> AddDocument(DocumentDTO document)
        {
            var mapDoc = _mapper.Map<Document>(document);
            var validator = new DocumentsValidator();
            var validation = validator.Validate(mapDoc);

            //Save Document
            if (validation.IsValid)
            {
                await _documentRepository.AddAsync(mapDoc);

                if (mapDoc.Paid == true)
                {
                    if (mapDoc.Operations == Operations.Entry)
                    {

                        var messageBody = _bankRecordClient.MessageBody(Origin.Document, mapDoc.Id, $"Revert Purshase order id: {mapDoc.Id}", BankRecordDomain.Entities.Type.Revert, mapDoc.Total);

                        await _bankRecordClient.PostCashBank(messageBody);
                    }
                    else
                    { 
                        var messageBody = _bankRecordClient.MessageBody(Origin.Document, mapDoc.Id, $"Financial Transaction (id: {mapDoc.Id})", BankRecordDomain.Entities.Type.Payment, -mapDoc.Total);

                        await _bankRecordClient.PostCashBank(messageBody);
                    }
                }
            }
            else
            {
                var errorList = new ErrorMessage<Document>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    validation.Errors.ConvertAll(x => x.ErrorMessage.ToString()), mapDoc);
                throw new Exception(ErrorList(errorList));
            }

            return mapDoc;
        }

        public async Task<List<Document>> GetAllDocuments(PageParamenters pageParameters)
        {
            var result = await _documentRepository.GetAllWithPaging(pageParameters);

            if (result.Count == 0)
            {
                var error = _documentRepository.NotFoundMessage(new Document());
                throw new Exception(ErrorList(error));
            }

            return result;
        }

        public async Task<Document> GetById(Guid id)
        {
            var result = await _documentRepository.GetByIdAsync(id);

            if (result == null)
            {
                var error = _documentRepository.NotFoundMessage(new Document());
                throw new Exception(ErrorList(error));
            }

            return result;
        }

        public async Task<Document> UpdateDocument(Guid id, DocumentDTO input)
        {
            var documentUpdate = await _documentRepository.GetByIdAsync(id);
            var totalValueOld = documentUpdate.Total;

            if (documentUpdate == null)
            {
                var error = _documentRepository.NotFoundMessage(new Document());
                throw new Exception(ErrorList(error));
            }

            if (documentUpdate.Paid == true && input.Paid == false)
            {
                var result = _documentRepository.BadRequestMessage(new Document(), "Document is already payed....");
                var listError = ErrorList(result);
                throw new Exception(listError);
            }

            var mapperDoc = _mapper.Map(input, documentUpdate);

            var validator = new DocumentsValidator();
            var valid = validator.Validate(mapperDoc);

            var TotalUpdated = documentUpdate.Total - totalValueOld;

            if (valid.IsValid)
            {

                if (documentUpdate.Paid == false && input.Paid == true || TotalUpdated != totalValueOld && documentUpdate.Paid == true)
                {

                    var messageBody = _bankRecordClient.MessageBody(Origin.Document, id, $"Diference in Document id: {documentUpdate.Id}", BankRecordDomain.Entities.Type.Revert, TotalUpdated);

                    await _bankRecordClient.PostCashBank(messageBody);

                    if (documentUpdate.Paid == false && input.Paid == true)
                    {

                        messageBody = _bankRecordClient.MessageBody(Origin.Document, id, $"Document Payed with id: {documentUpdate.Id}", BankRecordDomain.Entities.Type.Revert, -TotalUpdated);

                        await _bankRecordClient.PostCashBank(messageBody);
                    }
                }

                await _documentRepository.UpdateAsync(mapperDoc);

                return mapperDoc;
            }
            else
            {
                var errorList = new ErrorMessage<Document>(HttpStatusCode.BadRequest.GetHashCode().ToString(),
                    valid.Errors.ConvertAll(x => x.ErrorMessage.ToString()), new Document());
                throw new Exception(ErrorList(errorList));
            }
        }

        public async Task<Document> UpdatePaymentDocument(Guid id, bool status)
        {
            //Get Documents
            var documentUpdate = await _documentRepository.GetByIdAsync(id);

            if (documentUpdate == null)
            {
                var error = _documentRepository.NotFoundMessage(new Document());
                var listError = ErrorList(error);
                throw new Exception(listError);
            }
            if (documentUpdate.Paid == true)
            {
                var result = _documentRepository.BadRequestMessage(new Document(), "You can only delete the Document....");
                var listError = ErrorList(result);
                throw new Exception(listError);
            }

            documentUpdate.Paid = status;
            await _documentRepository.UpdateAsync(documentUpdate);

            if (documentUpdate.Paid == true)
            {

                var messageBody = _bankRecordClient.MessageBody(Origin.Document, id, $"Financial Transaction (id: {documentUpdate.Id})", BankRecordDomain.Entities.Type.Receive, documentUpdate.Total);

                await _bankRecordClient.PostCashBank(messageBody);
            }

            return documentUpdate;
        }

        public async Task<Document> DeleteDocument(Guid id)
        {
            var documentDelete = await _documentRepository.GetByIdAsync(id);

            if (documentDelete == null)
            {
                var error = _documentRepository.NotFoundMessage(new Document());
                var listError = ErrorList(error);
                throw new Exception(listError);
            }
            else
            {
                await _documentRepository.DeleteAsync(documentDelete);

                if (documentDelete.Paid == true)
                {
                    var messageBody = _bankRecordClient.MessageBody(Origin.Document, id, $"Revert Document Deleted with id: {documentDelete.Id}", BankRecordDomain.Entities.Type.Revert, -documentDelete.Total);

                    await _bankRecordClient.PostCashBank(messageBody);
                }
            }
            return documentDelete;
        }

        public string ErrorList(ErrorMessage<Document> error)
        {
            var errorList = "";

            foreach (var item in error.Message)
            {
                errorList += item.ToString();
            }
            return errorList;
        }
    }
}
