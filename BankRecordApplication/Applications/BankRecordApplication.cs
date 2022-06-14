using ApplicationBankRecord.Interface;
using AutoMapper;
using BankRecordData.Repository;
using BankRecordDomain.DTO_s;
using BankRecordDomain.Entities;
using BankRecordDomain.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationBankRecord.Applications
{
    public class ApplicationBankRecord : IBankRecordApplication
    {
        private readonly IMapper _mapper;
        private readonly IBankRecordRepository _bankRecordRepository;

        public ApplicationBankRecord(IMapper mapper, IBankRecordRepository bankRecordRepository)
        {
            _mapper = mapper;
            _bankRecordRepository = bankRecordRepository;
        }

        public async Task AddBankRecord(BankRecordDTO bankRecord)
        {
            var aux = _mapper.Map<BankRecord>(bankRecord);

            var validator = new BankRecordValidator();
            var validation = validator.Validate(aux);

            if (bankRecord.Origin == Origin.Null)
            {
                aux.OriginId = null;
            }

            if (validation.IsValid)
            {
                await _bankRecordRepository.AddAsync(aux);
            }
            else
            {
                var returnList = "";

                foreach (var item in validation.Errors)
                {
                    returnList += item.ToString();
                }
                throw new Exception(returnList);
            }
        }

        public async Task<List<BankRecord>> GetAllBankRecord(PageParamenters pageParamenters)
        {
            return _bankRecordRepository.GetAll(pageParamenters);
        }

        public async Task<BankRecord> GetBankRecordById(Guid Id)
        {

            if (Id != Guid.Empty)
                return await _bankRecordRepository.GetByIdAsync(Id);
            return null;
        }

        public async Task<BankRecord> GetBankRecordByOriginId(Guid Id)
        {
            if (Id != Guid.Empty)
                return await _bankRecordRepository.GetByOriginIdAsync(Id);
            return null;
        }

        public async Task PutBankRecord(Guid id, BankRecordDTO cashbook)
        {
            var bankRecordUpdate = await _bankRecordRepository.GetByIdAsync(id);
            //var requests = _requestRepository.GetAll().ToList();

            if (bankRecordUpdate == null)
            {
                var returnList = "No data available with this id in database";
                throw new Exception(returnList);
            }
            //foreach (var item in requests)
            //{
            //    if (bankRecordUpdate.OriginId == item.Id)
            //    {
            //        var returnList = "The record you want to edit cannot be edited";
            //        throw new Exception(returnList);
            //    }
            //}
            var mapBankRecord = _mapper.Map<BankRecord>(cashbook);
            mapBankRecord.Id = id;
            var validator = new BankRecordValidator();
            var validation = validator.Validate(mapBankRecord);

            if (validation.IsValid)
            {
                await _bankRecordRepository.UpdateAsync(mapBankRecord);
            }
            else
            {
                var returnList = "";

                foreach (var item in validation.Errors)
                {
                    returnList += item.ToString();
                }
                throw new Exception(returnList);
            }
        }
    }
}
