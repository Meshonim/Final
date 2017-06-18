using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class CreateBidModel
    {
        [Required]
        [Range(1, 1000000)]
        public int Price { get; set; }
        [Required]
        public int LotId { get; set; }
    }
}