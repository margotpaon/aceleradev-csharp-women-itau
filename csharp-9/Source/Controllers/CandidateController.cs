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
    public class CandidateController : ControllerBase
    {
        private ICandidateService _candidateService;
        private readonly IMapper _mapper;
        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _candidateService = service;
            _mapper = mapper;
        }

        // GET api/candidate
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {            
            
            if (accelerationId.HasValue && companyId == null)
            {
                var acceleration = _candidateService.FindByAccelerationId(accelerationId.Value).ToList();
                var retorno = _mapper.Map<List<CandidateDTO>>(acceleration);

                return Ok(retorno);
            }

            else if (companyId.HasValue && accelerationId == null)
            {
                var company = _candidateService.FindByCompanyId(companyId.Value).ToList();
                var retorno = _mapper.Map<List<CandidateDTO>>(company);
                return Ok(retorno);
            }

            else
                return NoContent();
        }

        // GET api/candidate/{userId}/{accelerationId}/{companyId}
        [HttpGet]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {   
            //deve apontar para o método FindById e retornar um CandidateDTO         
            
            var candidate = _candidateService.FindById(userId, accelerationId, companyId);

            if (candidate != null)
            {
                var retorno = _mapper.Map<CandidateDTO>(candidate);

                return Ok(retorno);
            }

            else
                return NotFound();

        }

        // POST api/candidate
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            //deve receber um UserDTO, apontar para o método Save e retornar um UserDTO

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var candidate = _mapper.Map<Candidate>(value);
            //Salvar
            var retorno = _candidateService.Save(candidate);
            //mapear Model para Dto
            return Ok(_mapper.Map<CandidateDTO>(retorno));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }   
     
    }
    
}
