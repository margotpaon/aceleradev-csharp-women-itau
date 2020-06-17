using System;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext context;

        public UserService(CodenationContext context)
        {
            this.context = context;
            Initialize();
        }

        public void Initialize()
        {
        
        
            // Look for any Users.
            if (context.Users.Any())
            {
                return;   // Data was already seeded
            }

            context.Users.AddRange(
                new User
                {
                    Id = 1,
                    FullName = "Chlo Capaldi",
                    Email = "ccapaldi0@exblog.jp",
                    Nickname = "Chlo",
                    Password = "coOuP45ZbK",
                    CreatedAt = DateTime.Parse("2019-06-17 06:35:28")
                },
                new User
                {
                    Id = 2,
                    FullName = "Vivia Cowwell",
                    Email = "vcowwell1@lycos.com",
                    Nickname = "Vivia",
                    Password = "KA9yknYe",
                    CreatedAt = DateTime.Parse("2018-08-25 17:45:58")
                },
                new User
                {
                    Id = 3,
                    FullName = "Osborne Wynn",
                    Email = "owynn2@themeforest.net",
                    Nickname = "Osborne",
                    Password = "XU3ydNp8iv8",
                    CreatedAt = DateTime.Parse("2019-06-06 22:01:28")
                },
                new User
                {
                    Id = 4,
                    FullName = "Moyna Terne",
                    Email = "mterne3@home.pl",
                    Nickname = "Moyna",
                    Password = "JHoTNdcCCAp",
                    CreatedAt = DateTime.Parse("2019-01-24 23:13:40")
                },
                new User
                {
                    Id = 5,
                    FullName = "Mignon Steanyng",
                    Email = "msteanyng4@japanpost.jp",
                    Nickname = "Mignon",
                    Password = "ypUHWMV0Kuh",
                    CreatedAt = DateTime.Parse("2018-10-16 03:16:12")
                },
                new User
                {
                    Id = 6,
                    FullName = "Rona Segoe",
                    Email = "rsegoe5@yale.edu",
                    Nickname = "Rona",
                    Password = "iJCbvUEJX",
                    CreatedAt = DateTime.Parse("2018-09-01 04:43:42")
                },
                new User
                {
                    Id = 7,
                    FullName = "Norbie Chisnell",
                    Email = "nchisnell6@zimbio.com",
                    Nickname = "Norbie",
                    Password = "T5RF38a",
                    CreatedAt = DateTime.Parse("2018-11-05 07:31:44")
                },
                new User
                {
                    Id = 8,
                    FullName = "Peg Toffetto",
                    Email = "ptoffetto7@theguardian.com",
                    Nickname = "Peg",
                    Password = "TV06FxQ",
                    CreatedAt = DateTime.Parse("2019-03-20 06:44:17")
                },
                new User
                {
                    Id = 9,
                    FullName = "Susy Redihough",
                    Email = "sredihough8@java.com",
                    Nickname = "Susy",
                    Password = "Uw4xwhMJNp",
                    CreatedAt = DateTime.Parse("2019-04-29 21:25:57")
                },
                new User
                {
                    Id = 10,
                    FullName = "Tim Egglestone",
                    Email = "tegglestone9@blog.com",
                    Nickname = "Tim",
                    Password = "2epRrOi",
                    CreatedAt = DateTime.Parse("2018-08-27 15:55:27")
                });

            context.SaveChanges();
        
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return context.Accelerations.
                Where(x => x.Name == name).
                SelectMany(x => x.Candidates).
                Select(x => x.User).
                Distinct().
                ToList();
        }

        public IList<User> FindByCompanyId(int companyId)
        {
            return context.Candidates.
                Where(x => x.CompanyId == companyId).
                Select(x => x.User).
                Distinct().
                ToList();
        }

        public User FindById(int id)
        {
            return context.Users.Find(id);
        }

        public User FindByEmail(string email)
        {
            return context.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

        public User Save(User user)
        {
            var state = user.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(user).State = state;
            context.SaveChanges();
            return user;
        }
    }
}
