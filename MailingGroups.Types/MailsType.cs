using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MailingGroups.Types
{
    public class MailsType
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
