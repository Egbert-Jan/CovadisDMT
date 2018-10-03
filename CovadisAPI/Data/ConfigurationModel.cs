using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{
    public class ConfigurationModel
    {

        [Key]
        public int Id { get; set; }

        public string ConfigName { get; set; }

        public string Value { get; set; }
    }
}
