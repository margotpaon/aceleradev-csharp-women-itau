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
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _accelerationService;
        private readonly IMapper _mapper;
        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _accelerationService = service;
            _mapper = mapper;
        }

        // GET api/acceleration
        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {            
            if (companyId.HasValue)
            {
                var acceleration = _accelerationService.FindByCompanyId(companyId.Value).ToList();
                var retorno = _mapper.Map<List<AccelerationDTO>>(acceleration);

                return Ok(retorno);
            }

            else
                return NoContent();
        }

        // GET api/acceleration/{id}
        [HttpGet]
        public ActionResult<AccelerationDTO> Get(int id)
        {   
            //deve apontar para o método FindById e retornar um AccelerationDTO         
            var acceleration = _accelerationService.FindById(id);


            if (acceleration != null)
            {
                var retorno = _mapper.Map<AccelerationDTO>(acceleration);

                return Ok(retorno);
            }

            else
                return NotFound();

        }

        // POST api/acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            //deve receber um AccelerationDTO, apontar para o método Save e retornar um AccelerationDTO

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var acceleration = _mapper.Map<Acceleration>(value);
            //Salvar
            var retorno = _accelerationService.Save(acceleration);
            //mapear Model para Dto
            return Ok(_mapper.Map<AccelerationDTO>(retorno));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }   
     
    }    
    
}
