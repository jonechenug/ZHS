using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ZHS.WebApi.Models
{
    public class ValidateModel
    {
        [Required(ErrorMessage = "Value是必须的")]
        public String Value { get; set; }

        public String Test { get; set; }
    }
}
