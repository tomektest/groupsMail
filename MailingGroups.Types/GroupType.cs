using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailingGroups.Types
{
    public class GroupType
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public int Lp { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
       // [Remote(action: "VerifyGroupName", controller: "Groups")]
        public string Name { get; set; }

        //     [Required]
        public string UserId { get; set; }
    }
}
