using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codenation.Challenge.Models
{
    [Table("acceleration")]
    public class Acceleration
    {
        
        [Column("id")]
        [Key]
        [Required]
        public int Id { get; set; } // Primary Key

        [Column("created_at")]
        [Required]
        public DateTime CreateAt { get; set; }

        [Column("name")]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Column("slug")]
        [MaxLength(50)]
        [Required]
        public string Slug { get; set; }

        [Column("challenge_id")]
        [Required]
        public int ChallengeID { get; set; }//Foreign key
                
        public virtual Challenge Challenge { get; set; }// referencia

        public ICollection<Candidate> Candidates { get; set; }
    }
}