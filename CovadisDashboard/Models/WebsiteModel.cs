using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {
        [DisplayName("Name: ")]
        public string Name { get; set; }

        [DisplayName("Id: ")]
        public int Id { get; set; }

        [DisplayName("Url:")]
        public string Url { get; set; }

        [DisplayName("Elements: ")]
        public List<ElementModel> Elements { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Url, Elements);
        }
    }
}
