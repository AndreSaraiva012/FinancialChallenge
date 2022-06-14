using DocumentApplication.Interfaces;
using DocumentDomain.DTO_s;
using Infrastructure.ErrorMessage;
using Infrastructure.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace DocumentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentApplication _documentApplication;

        public DocumentController(IDocumentApplication documentApplication)
        {
            _documentApplication = documentApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocumentDTO document)
        {
            try
            {
                return Ok(await _documentApplication.AddDocument(document));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

        [HttpGet("GetAllDocuments")]
        public async Task<IActionResult> GetAll([FromQuery] PageParamenters paramenters)
        {
            try
            {
                return Ok(await _documentApplication.GetAllDocuments(paramenters));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

        [HttpGet("GetByID")]
        public async Task<IActionResult> GetById([Required] Guid id)
        {
            try
            {
                return Ok(await _documentApplication.GetById(id));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

        [HttpPut("ChangeDocument")]
        public async Task<IActionResult> ChangeDocument([Required] Guid id, [FromBody] DocumentDTO document)
        {
            try
            {
                return Ok(await _documentApplication.UpdateDocument(id, document));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

        [HttpPut("ChangePaymentState")]
        public async Task<IActionResult> ChangeState([Required] Guid id, bool Status)
        {
            try
            {
                return Ok(await _documentApplication.UpdatePaymentDocument(id, Status));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById([Required] Guid id)
        {
            try
            {
                return Ok(await _documentApplication.DeleteDocument(id));
            }
            catch (Exception ex)
            {
                var errorList = new List<string>();
                errorList.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new ErrorMessage<DocumentDomain.Entities.Document>(HttpStatusCode.BadRequest.GetHashCode().
                    ToString(), errorList, new DocumentDomain.Entities.Document()));
            }
        }

    }
}
