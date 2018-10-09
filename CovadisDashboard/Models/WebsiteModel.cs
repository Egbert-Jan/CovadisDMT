using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class WebsiteModel
    {

        [HiddenInput]
        [DisplayName("Id: ")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name: ")]
        public string Name { get; set; }
        
        [Required]
        [Url]
        [DisplayName("Url:")]
        public string Url { get; set; }

        [DisplayName("Elements: ")]
        public List<ElementModel> Elements { get; set; }

        //For when the API fails
        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Url, Elements);
        }
    }
}
