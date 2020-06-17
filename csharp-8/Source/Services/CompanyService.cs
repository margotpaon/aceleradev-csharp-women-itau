using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext _context;
        public CompanyService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return _context.Candidates.Where(x => x.AccelerationId.Equals(accelerationId))
                                      .Select(x => x.Company)
                                      .Distinct()
                                      .ToList();//retornar uma lista de empresas a partir do accelerationId            
        }

        public Company FindById(int id)
        {
            return _context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return _context.Candidates.Where(x => x.UserId.Equals(userId))
                                      .Select(x => x.Company)
                                      .Distinct()
                                      .ToList();
        }

        public Company Save(Company company)
        {
            var estado = company.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(company).State = estado;

            _context.SaveChanges();

            return company;
        }
    }
}