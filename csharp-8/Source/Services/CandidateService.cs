using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext _context;
        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return _context.Accelerations.Where(x => x.Id.Equals(accelerationId))
                                              .SelectMany(x => x.Candidates)
                                              .Distinct()
                                              .ToList(); //lista de candidatos a partir do id da aceleração
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Companies.Where(x => x.Id.Equals(companyId))
                                     .SelectMany(x => x.Candidates)
                                     .Distinct()
                                     .ToList(); //lista candidatos a partir do id da empresa
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            // ignorar change tracker
            var existe = _context.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (existe == null)
                _context.Candidates.Add(candidate);
            else
                 existe.Status = candidate.Status;

            _context.SaveChanges();

            return candidate;
        }
    }
}
