using MvcPL.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class CreateLotModel
    {
        [Display(Name = "Duration (in days)")]
        [Required]
        [Range(3, 14)]
        public uint Days { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        [Display(Name = "Print tags separated by spaces")]
        [Required]
        [RegularExpression(@"[\w\s]+", ErrorMessage = "Wrong tag string")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "Please browse your image")]
        [Display(Name = "Upload Image")]
        [ValidateFile]
        public HttpPostedFileBase Picture { get; set; }
    }
}