using ApplicationBuyRequest.Interfaces;
using BuyRequest.Controllers;
using BuyRequest.UnitTest.AutoFaker;
using BuyRequestDomain.DTO_s;
using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BuyRequest.UnitTest
{
    public class BuyRequestControllerTest
    {
        private readonly AutoMocker _autoMocker;

        public BuyRequestControllerTest()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact(DisplayName = "Testing Adding BuyRequest")]
        public async Task TestAddBuyRequest()
        {
            var buyRequest = BuyRequestAutoFaker.GenerateBuyRequestDTO();

            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();
            buyRequestApplication.Setup(x => x.AddBuyRequest(buyRequest));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.Post(buyRequest);

            buyRequestApplication.Verify(x => x.AddBuyRequest(It.IsAny<BuyRequestDTO>()), Times.Once);
        }

        [Fact(DisplayName = "Testing GetAll BuyRequests")]
        public async Task TestGetAllBuyRequest()
        {
            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();
            PageParamenters paramenters = new PageParamenters();

            buyRequestApplication.Setup(x => x.GetAllBuyRequest(paramenters));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.GetAll(paramenters);

            buyRequestApplication.Verify(x => x.GetAllBuyRequest(paramenters), Times.Once);
        }

        [Fact(DisplayName = "Testing GetById BuyRequest")]
        public async Task TestGetByIdBuyRequest()
        {
            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();

            var id = Guid.NewGuid();
            buyRequestApplication.Setup(x => x.GetBuyRequestsById(id));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.GetById(id);

            buyRequestApplication.Verify(x => x.GetBuyRequestsById(id), Times.Once);
        }

        [Fact(DisplayName = "Testing GetByClientId BuyRequest")]
        public async Task TestGetByClientIdBuyRequest()
        {
            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();

            var clientId = Guid.NewGuid();
            buyRequestApplication.Setup(x => x.GetBuyRequestsByClient(clientId));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.GetByClientId(clientId);

            buyRequestApplication.Verify(x => x.GetBuyRequestsByClient(clientId), Times.Once);
        }

        [Fact(DisplayName = "Testing Update BuyRequest")]
        public async Task TestUpdateBuyRequest()
        {
            var buyRequest = BuyRequestAutoFaker.GenerateBuyRequestDTO();

            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();

            buyRequestApplication.Setup(x => x.GetBuyRequestsById(buyRequest.Id));
            buyRequestApplication.Setup(x => x.UpdateBuyRequest(buyRequest));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.ChangeRequest(buyRequest);

            buyRequestApplication.Verify(x => x.UpdateBuyRequest(It.IsAny<BuyRequestDTO>()), Times.Once());
        }

        [Fact(DisplayName = "Testing UpdateStatus BuyRequest")]
        public async Task TestUpdateStatusBuyRequest()
        {
            var buyRequest = BuyRequestAutoFaker.GenerateBuyRequestDTO();
            var id = Guid.NewGuid();

            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();

            buyRequestApplication.Setup(x => x.UpdateBuyRequestStatus(id, buyRequest.Status));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.ChangeState(id, buyRequest.Status);

            buyRequestApplication.Verify(x => x.UpdateBuyRequestStatus(id, buyRequest.Status), Times.Once());
        }

        [Fact(DisplayName = "Testing Delete BuyRequest")]
        public async Task TestDeleteBuyRequest()
        {
            var id = Guid.NewGuid();

            var buyRequestApplication = _autoMocker.GetMock<IBuyRequestApplication>();

            buyRequestApplication.Setup(x => x.DeleteBuyRequest(id));

            var controller = _autoMocker.CreateInstance<BuyRequestsController>();
            await controller.DeleteById(id);

            buyRequestApplication.Verify(x => x.DeleteBuyRequest(id), Times.Once());
        }
    }
}
