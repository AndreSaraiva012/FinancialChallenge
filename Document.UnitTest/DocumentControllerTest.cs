using Document.UnitTest.AutoFaker;
using DocumentAPI.Controllers;
using DocumentApplication.Interfaces;
using DocumentDomain.DTO_s;
using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Document.UnitTest
{
    public class DocumentControllerTest
    {
        private readonly AutoMocker _autoMocker;
        public DocumentControllerTest()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact(DisplayName = "Testing Adding Document")]
        public async Task TestAddDocument()
        {
            var document = DocumentAutoFaker.GenerateDocumentDTO();

            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();
            documentApplication.Setup(x => x.AddDocument(document));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.Post(document);

            documentApplication.Verify(x => x.AddDocument(It.IsAny<DocumentDTO>()), Times.Once);
        }

        [Fact(DisplayName = "Testing GetAll Documents")]
        public async Task TestGetAllDocuments()
        {
            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();
            PageParamenters paramenters = new PageParamenters();

            documentApplication.Setup(x => x.GetAllDocuments(paramenters));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.GetAll(paramenters);

            documentApplication.Verify(x => x.GetAllDocuments(paramenters), Times.Once);
        }

        [Fact(DisplayName = "Testing GetById Document")]
        public async Task TestGetByIdDocument()
        {
            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();

            var id = Guid.NewGuid();
            documentApplication.Setup(x => x.GetById(id));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.GetById(id);

            documentApplication.Verify(x => x.GetById(id), Times.Once);
        }

        [Fact(DisplayName = "Testing Update Document")]
        public async Task TestUpdateDocument()
        {
            var document = DocumentAutoFaker.GenerateDocumentDTO();
            var id = Guid.NewGuid();

            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();

            documentApplication.Setup(x => x.GetById(id));
            documentApplication.Setup(x => x.UpdateDocument(id,document));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.ChangeDocument(id, document);

            documentApplication.Verify(x => x.UpdateDocument(id,document), Times.Once());
        }

        [Fact(DisplayName = "Testing UpdatePaymentStatus Document")]
        public async Task TestUpdateStatusDocument()
        {
            var document = DocumentAutoFaker.GenerateDocumentDTO();
            var id = Guid.NewGuid();

            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();

            documentApplication.Setup(x => x.UpdatePaymentDocument(id, document.Paid));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.ChangeState(id, document.Paid);

            documentApplication.Verify(x => x.UpdatePaymentDocument(id, document.Paid), Times.Once());
        }

        [Fact(DisplayName = "Testing Delete Document")]
        public async Task TestDeleteDocument()
        {
            var id = Guid.NewGuid();

            var documentApplication = _autoMocker.GetMock<IDocumentApplication>();

            documentApplication.Setup(x => x.DeleteDocument(id));

            var controller = _autoMocker.CreateInstance<DocumentController>();
            await controller.DeleteById(id);

            documentApplication.Verify(x => x.DeleteDocument(id), Times.Once());
        }
    }
}
