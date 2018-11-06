using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CovadisDashboard.Models
{
    public class ConfigModel
    {
        [HiddenInput]
        [DisplayName("Id: ")]
        public int Id { get; set; }

        [DisplayName("Name: ")]
        public string ConfigName { get; set; }

        [Required]
        [DisplayName("Value: ")]
        public string Value { get; set; }
    }
}
