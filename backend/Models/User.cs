using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Registration { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LastActivity { get; set; }

        public int LifeTime { get; set; }
    }
}