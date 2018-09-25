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
        public int WebsiteID { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }

        [MaxLength(100)]
        public string LaatsteData { get; set; }

        //[Required]
        public IList<ElementsDataModel> Elements { get; set; }

    }


    public class ElementsDataModel
    {
        [Key]
        public int ElementID { get; set; }

        [Required]
        [MaxLength(300)]
        public string ElementName { get; set; }


        public WebsitesDataModel Website { get; set; }
    }




    //Jos:
    //public class WebsiteElements
    //{
    //    public int Id { get; set; }

    //    public int WebsiteId { get; set; }

    //    public int ElementId { get; set; }
    //}
}
