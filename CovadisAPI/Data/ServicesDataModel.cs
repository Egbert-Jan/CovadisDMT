using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{
    public class ServicesDataModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Endpoint { get; set; }


        [MaxLength(100)]
        public string LaatsteData { get; set; }

    }
}
