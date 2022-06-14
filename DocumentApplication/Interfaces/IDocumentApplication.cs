using DocumentDomain.DTO_s;
using DocumentDomain.Entities;
using Infrastructure.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentApplication.Interfaces
{
    public interface IDocumentApplication
    {
        Task<Document> AddDocument(DocumentDTO document);
        Task<List<Document>> GetAllDocuments(PageParamenters pageParameters);
        Task<Document> GetById(Guid id);
        Task<Document> UpdateDocument(Guid id, DocumentDTO document);
        Task<Document> UpdatePaymentDocument(Guid id, bool status);
        Task<Document> DeleteDocument(Guid id);
    }
}
