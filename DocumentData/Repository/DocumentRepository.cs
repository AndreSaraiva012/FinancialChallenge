using DocumentDomain.Entities;
using Infrastructure.Repository;

namespace DocumentData.Repository
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly DocumentContext _context;

        public DocumentRepository(DocumentContext context) : base(context)
        {

            _context = context;

        }
    }
}

