using System;
using System.Collections.Generic;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _service;

        public QuoteController(IQuoteService service)
        {
            _service = service;
        }

        // GET api/quote
        [HttpGet]
        public ActionResult<QuoteView> GetAnyQuote()
        {
            var anyQuote = _service.GetAnyQuote();
            if (anyQuote != null)
            {
                var getJsonResponse = new QuoteView()
                {
                    Id = anyQuote.Id,
                    Actor = anyQuote.Actor,
                    Detail = anyQuote.Detail
                };
                return getJsonResponse;
            }
            return NotFound();
        }

        // GET api/quote/{actor}
        [HttpGet("{actor}")]
        public ActionResult<QuoteView> GetAnyQuote(string actor)
        {
            var anyQuoteActor = _service.GetAnyQuote(actor);
            if (anyQuoteActor != null)
            {
                var getJsonResponse = new QuoteView()
                {
                    Id = anyQuoteActor.Id,
                    Actor = anyQuoteActor.Actor,
                    Detail = anyQuoteActor.Detail
                };
                return getJsonResponse;
            }
            return NotFound();
        }

    }
}
