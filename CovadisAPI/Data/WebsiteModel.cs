using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CovadisAPI.Data
{
    public class WebsiteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }

        public bool LastDataCorrect { get; set; }

        public int TimesWrongData { get; set; }

        public IList<ElementModel> Elements { get; set; }
    }


    public class ElementModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public string Status { get; set; }

        public WebsiteModel Website { get; set; }
    }
}
