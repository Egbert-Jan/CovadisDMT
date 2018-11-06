using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class ApiModel
    {
        [HiddenInput]
        [DisplayName("Id: ")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name: ")]
        public string Name { get; set; }

        [Required]
        [Url]
        [DisplayName("Url: ")]
        public string Url { get; set; }

        [DisplayName("Status: ")]
        public string Status { get; set; }

        [DisplayName("Error: ")]
        public string Error { get; set; }
    }
}
