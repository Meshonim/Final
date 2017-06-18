using MvcPL.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class UpdateUserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidateRole]
        public int RoleId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}