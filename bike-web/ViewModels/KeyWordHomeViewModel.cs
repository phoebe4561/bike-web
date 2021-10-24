using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bike_web.ViewModels
{
    public class KeyWordHomeViewModel
    {
        [Required]
        public string SearchKey { get; set; }
    }
}