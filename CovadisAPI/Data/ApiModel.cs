using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{
    public class ApiModel
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(300)]
        public string Url { get; set; }

        public string Status { get; set; }

        public string LastData { get; set; }

        public string Error { get; set; }


    }
}



