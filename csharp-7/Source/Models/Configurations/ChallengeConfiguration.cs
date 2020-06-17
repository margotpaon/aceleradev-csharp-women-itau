using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source.Models.Configurations
{
    public class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
    {
        public void Configure(EntityTypeBuilder<Challenge> builder)
        {
            builder.HasMany(c => c.Submissions).WithOne(e => e.Challenge).IsRequired();
            builder.HasMany(c => c.Accelerations).WithOne(e => e.Challenge).IsRequired();
        }
    }
}
