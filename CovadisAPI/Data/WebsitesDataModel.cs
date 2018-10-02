using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CovadisAPI.Data
{
    public class WebsitesDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }

        public IList<ElementsDataModel> Elements { get; set; }
    }


    public class ElementsDataModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public WebsitesDataModel Website { get; set; }
    }
}
