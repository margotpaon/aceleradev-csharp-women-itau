using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext _context;
        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.CompanyId.Equals(companyId))
                                      .Select(x => x.Acceleration)
                                      .Distinct()
                                      .ToList();// lista de acelerações a partir do id da empresa
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.Find(id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            var estado = acceleration.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(acceleration).State = estado;

            _context.SaveChanges();

            return acceleration;
        }
    }
}
