using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext _context;
        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {

            return _context.Users.Where(x => x.Id.Equals(userId))
                                              .SelectMany(x => x.Candidates)
                                              .Where(x => x.AccelerationId.Equals(accelerationId))
                                              .Select(x => x.Acceleration.Challenge)
                                              .Distinct()
                                              .ToList();//retornar uma lista de Challenges a partir do accelerationId e userId
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            var estado = challenge.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(challenge).State = estado;

            _context.SaveChanges();

            return challenge;
        }
    }
}