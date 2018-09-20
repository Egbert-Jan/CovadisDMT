using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{
    public class WebsitesDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }

        [MaxLength(100)]
        public string LaatsteData { get; set; }

        [MaxLength(100)]
        public string Element1 { get; set; }

        [MaxLength(100)]
        public string Element2 { get; set; }

        [MaxLength(100)]
        public string Element3 { get; set; }
    }
}
