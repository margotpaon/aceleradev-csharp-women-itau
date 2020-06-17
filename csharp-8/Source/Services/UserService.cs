using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext _context;
        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return _context.Candidates.Where(x => x.Acceleration.Name.Equals(name))
                                      .Select(x => x.User)
                                      .Distinct()
                                      .ToList(); //retornar uma lista de usuários a partir do nome da aceleração            
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            
            return _context.Candidates.Where(x => x.CompanyId.Equals(companyId))
                                      .Select(x => x.User)
                                      .Distinct()
                                      .ToList();//retornar uma lista de usuários a partir do companyId
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Save(User user)
        {
            var estado = user.Id == 0 ? EntityState.Added : EntityState.Modified;

            _context.Entry(user).State = estado;

            _context.SaveChanges();

            return user;
        }
    }
}
