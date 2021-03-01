using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.Models
{
    public class Mail
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int Lp { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int GroupModelId { get; set; }

    }
}
