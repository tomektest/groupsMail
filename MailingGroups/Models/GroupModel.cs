using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int Lp { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Remote(action: "VerifyGroupName", controller: "Groups")]
        public string Name { get; set; }

   //     [Required]
        public string UserId { get; set; }
    }

    public class GroupViewModel
    {
        public int Id { get; set; }

        public int Lp { get; set; }

        public string Name { get; set; }
    }
}
