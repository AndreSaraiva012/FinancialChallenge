using ApplicationBR.Interfaces;
using BankRecordDomain.DTO_s;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace BankRecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankRecordsController : ControllerBase
    {

        private readonly IBankRecordApplication _ibankRecordApplication;

        public BankRecordsController(IBankRecordApplication ibankRecordApplication)
        {
            _ibankRecordApplication = ibankRecordApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BankRecordDTO bankRecord)
        {
            try
            {
                await _ibankRecordApplication.AddBankRecord(bankRecord);
                return Ok(bankRecord);

            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BankRecordDTO>(HttpStatusCode.BadRequest.GetHashCode().ToString(), errorList, bankRecord));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParamenters paramenters)
        {
            try
            {
                return Ok(await _ibankRecordApplication.GetAllBankRecord(paramenters));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BankRecordDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new BankRecordDTO()));
            }
        }

        [HttpGet("GetByBankRecordOriginId")]
        public async Task<IActionResult> GetByOriginId([Required] Guid originId)
        {
            
            try
            {
                var bankRecord = await _ibankRecordApplication.GetBankRecordByOriginId(originId);
                return Ok(bankRecord);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BankRecordDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new BankRecordDTO()));
            }
        }

        [HttpGet("GetByBankRecordId")]
        public async Task<IActionResult> GetById([Required] Guid id)
        {
            try
            {
                var bankRecord = await _ibankRecordApplication.GetBankRecordById(id);
                return Ok(bankRecord);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BankRecordDTO>(HttpStatusCode.NoContent.GetHashCode().ToString(), errorList, new BankRecordDTO()));
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeBankRecord(Guid Id, [FromBody] BankRecordDTO bankRecord)
        {
            try
            {
                return Ok(await _ibankRecordApplication.PutBankRecord(Id, bankRecord));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BankRecordDTO>(HttpStatusCode.BadRequest.GetHashCode().
                     ToString(), errorList, new BankRecordDTO()));
            }
        }

    }
}
