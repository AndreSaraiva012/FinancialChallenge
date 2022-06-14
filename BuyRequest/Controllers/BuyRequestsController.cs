using ApplicationBuyRequest.Interfaces;
using AutoMapper;
using BuyRequestDomain.DTO_s;
using BuyRequestDomain.Entities;
using BuyRequestDomain.ViewModels;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace BuyRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyRequestsController : ControllerBase
    {
        private readonly IBuyRequestApplication _buyRequestApplication;

        public BuyRequestsController(IBuyRequestApplication buyRequestApplication)
        {
            _buyRequestApplication = buyRequestApplication;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BuyRequestDTO buyRequest)
        {
            try
            {
                var result = await _buyRequestApplication.AddBuyRequest(buyRequest);
                return Ok(result);

            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, buyRequest));
            }
        }

        [HttpGet("GetAllBuyRequests")]
        public async Task<IActionResult> GetAll([FromQuery] PageParamenters pageParamenters)
        {
            try
            {
                var result = await _buyRequestApplication.GetAllBuyRequest(pageParamenters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new BuyRequestDTO()));
            }
        }

        [HttpGet("GetByBuyRequestId")]
        public async Task<IActionResult> GetById([Required] Guid id)
        {
            try
            {
                return Ok(await _buyRequestApplication.GetBuyRequestsById(id));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new BuyRequestDTO()));
            }
        }

        [HttpGet("GetByClientId")]
        public async Task<IActionResult> GetByClientId([Required] Guid ClientId)
        {
            try
            {
                var result = await _buyRequestApplication.GetBuyRequestsByClient(ClientId); 
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new BuyRequestDTO()));
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangeRequest([FromBody] BuyRequestDTO buyRequest)
        {
            try
            {
                var result = await _buyRequestApplication.UpdateBuyRequest(buyRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, buyRequest));
            }
        }

        [HttpPut("ChangeState")]
        public async Task<IActionResult> ChangeState([Required] Guid id, Status state)
        {
            try
            {
                var result = await _buyRequestApplication.UpdateBuyRequestStatus(id, state);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new BuyRequestDTO()));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            try
            {
                var result = await _buyRequestApplication.DeleteBuyRequest(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<BuyRequestDTO>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new BuyRequestDTO()));
            }
        }
    }
}
