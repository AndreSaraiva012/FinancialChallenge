using DocumentDomain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DocumentData
{
    public class DocumentContext : DataContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options) { }
        public DbSet<Document> Documents { get; set; }

    }
}
