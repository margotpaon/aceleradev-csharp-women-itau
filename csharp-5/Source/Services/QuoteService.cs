using System;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class QuoteService : IQuoteService
    {
        private ScriptsContext _context;
        private IRandomService _randomService;

        public QuoteService(ScriptsContext context, IRandomService randomService)
        {
            this._context = context;
            this._randomService = randomService;
        }

        public Quote GetAnyQuote()
        {
            //lista inteira de Quotes
            var query = _context.Quotes.ToList();

            // se não retornar valor na lista não retorna valor na requisição
            if (query.Count == 0)
                return null;

            // a partir do tamanho da lista, retorna um valor aleatório que utilizaremos de index
            var ramdomIndex = _randomService.RandomInteger(query.Count());

            // o metodo skip ignora a lista até o index passado como argumento
            var retorno = query.Where(x => x.Actor != null)
                               .Skip(ramdomIndex)
                               .FirstOrDefault();
            
            if(retorno != null)
            //retorno Quote aleatorio
                return retorno;
            return null;
        }

        public Quote GetAnyQuote(string actor)
        {
            //lista inteira de Quotes
            var query = _context.Quotes.ToList();

            // se não retornar valor na lista não retorna valor na requisição
            if (query.Count == 0)
                return null;

            // a partir do tamanho da lista, retorna um valor aleatório que utilizaremos de index
            var ramdomIndex = _randomService.RandomInteger(query.Count());

            // o metodo skip ignora a lista até o index passado como argumento
            var ator =  _context.Quotes.Where(x => x.Actor == actor)
                                       .Skip(ramdomIndex)
                                       .FirstOrDefault();
            if (ator != null)
            //retorno Quote aleatorio por actor
                return ator;
            return null;    
        }
    }
}