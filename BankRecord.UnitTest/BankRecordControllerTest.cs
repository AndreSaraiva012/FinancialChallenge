using ApplicationBR.Interfaces;
using BankRecord.UnitTest.AutoFaker;
using BankRecordAPI.Controllers;
using BankRecordDomain.DTO_s;
using Infrastructure.Pagination;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankRecord.UnitTest
{
    public class BankRecordControllerTest
    {
        private readonly AutoMocker _autoMocker;

        public BankRecordControllerTest()
        {
            _autoMocker = new AutoMocker();
        }

        [Fact(DisplayName = "Testing Adding BankRecord")]
        public async Task TestAddBankRecord()
        {
            var bankRecord = BankRecordAutoFaker.GenerateBankRecordDTO();

            var bankRecordApplication = _autoMocker.GetMock<IBankRecordApplication>();
            bankRecordApplication.Setup(x => x.AddBankRecord(bankRecord));

            var controller = _autoMocker.CreateInstance<BankRecordsController>();
            await controller.Post(bankRecord);

            bankRecordApplication.Verify(x => x.AddBankRecord(It.IsAny<BankRecordDTO>()), Times.Once);
        }

        [Fact(DisplayName = "Testing GetAll BankRecord")]
        public async Task TestGetAllBankRecord()
        {
            var bankRecordApplication = _autoMocker.GetMock<IBankRecordApplication>();

            PageParamenters paramenters = new PageParamenters();

            bankRecordApplication.Setup(x => x.GetAllBankRecord(paramenters));

            var controller = _autoMocker.CreateInstance<BankRecordsController>();
            await controller.GetAll(paramenters);

            bankRecordApplication.Verify(x => x.GetAllBankRecord(paramenters), Times.Once);
        }

        [Fact(DisplayName = "Testing GetByOriginId BankRecord")]
        public async Task TestGetByOriginIdBankRecord()
        {
            var id = Guid.NewGuid();
            var bankRecordApplication = _autoMocker.GetMock<IBankRecordApplication>();

            bankRecordApplication.Setup(x => x.GetBankRecordByOriginId(id));

            var controller = _autoMocker.CreateInstance<BankRecordsController>();
            await controller.GetByOriginId(id);

            bankRecordApplication.Verify(x => x.GetBankRecordByOriginId(id), Times.Once);
        }

        [Fact(DisplayName = "Testing GetById BankRecord")]
        public async Task TestGetByIdBankRecord()
        {
            var id = Guid.NewGuid();
            var bankRecordApplication = _autoMocker.GetMock<IBankRecordApplication>();

            bankRecordApplication.Setup(x => x.GetBankRecordById(id));

            var controller = _autoMocker.CreateInstance<BankRecordsController>();
            await controller.GetById(id);

            bankRecordApplication.Verify(x => x.GetBankRecordById(id), Times.Once);
        }

        [Fact(DisplayName = "Testing Update BankRecord")]
        public async Task TestUpdateBankRecord()
        {
            var id = Guid.NewGuid();
            var bankRecord = BankRecordAutoFaker.GenerateBankRecordDTO();

            var bankRecordApplication = _autoMocker.GetMock<IBankRecordApplication>();
            bankRecordApplication.Setup(x => x.PutBankRecord(id, bankRecord));

            var controller = _autoMocker.CreateInstance<BankRecordsController>();
            await controller.ChangeBankRecord(id, bankRecord);

            bankRecordApplication.Verify(x => x.PutBankRecord(id, bankRecord), Times.Once);
        }

    }
}
