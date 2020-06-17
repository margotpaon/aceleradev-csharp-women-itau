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
    public class SubmissionController : ControllerBase
    {
        private ISubmissionService _submissionService;
        private readonly IMapper _mapper;
        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _submissionService = service;
            _mapper = mapper;
        }

        // GET api/submission/higherScore
        [HttpGet]
        public ActionResult<SubmissionDTO> GetHigherScore(int? challengeId)
        {            
            
            if (challengeId.HasValue)
            {
                var submission = _submissionService.FindHigherScoreByChallengeId(challengeId.Value);
                return Ok(submission);
            }

            else
                return NoContent();
        }

        // GET api/submission
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {   
            if (accelerationId.HasValue && challengeId.HasValue)
            {
                var submission = _submissionService.FindByChallengeIdAndAccelerationId(accelerationId.Value, challengeId.Value).ToList();
                var retorno = _mapper.Map<List<SubmissionDTO>>(submission);

                return Ok(retorno);
            }

            else
                return NoContent();

        }

        // POST api/submission
        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            //deve receber um SubmissionDTO, apontar para o método Save e retornar um SubmissionDTO

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mapear Dto para Model
            var submission = _mapper.Map<Submission>(value);
            //Salvar
            var retorno = _submissionService.Save(submission);
            //mapear Model para Dto
            return Ok(_mapper.Map<SubmissionDTO>(retorno));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        }   
     
    }
}
