using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {    
        private ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _companyService = service;
            _mapper = mapper;
        }

        // GET api/company
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {            
            
            if (accelerationId.HasValue && userId == null)
            {
                var acceleration = _companyService.FindByAccelerationId(accelerationId.Value).ToList();
                var retorno = _mapper.Map<List<CompanyDTO>>(acceleration);

                return Ok(retorno);
            }

            else if (userId.HasValue && accelerationId == null)
            {
                var user = _companyService.FindByUserId(userId.Value).ToList();
                var retorno = _mapper.Map<List<CompanyDTO>>(user); 
                return Ok(retorno);
            }

            else
                return NoContent();
        }

        // GET api/company/{id}
        [HttpGet]
        public ActionResult<CompanyDTO> Get(int id)
        {   
            //deve apontar para o método FindById e retornar um CompanyDTO         
            var company = _companyService.FindById(id);

            if (company != null)
            {
                var retorno = _mapper.Map<CompanyDTO>(company);

                return Ok(retorno);
            }

            else
                return NotFound();
        }

        // POST api/company
        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            //deve receber um CompanyDTO, apontar para o método Save e retornar um CompanyDTO

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var company = _mapper.Map<Company>(value);
            //Salvar
            var retorno = _companyService.Save(company);
            //mapear Model para Dto
            return Ok(_mapper.Map<CompanyDTO>(retorno));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }   
     
    }
}
