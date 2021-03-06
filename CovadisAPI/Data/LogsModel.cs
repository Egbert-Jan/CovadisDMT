﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CovadisAPI.Data
{

    public class WebsiteLog
    {
        [Key]
        public int Id { get; set; }

        public int WebsiteID { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }

        [MaxLength(300)]
        public string Error { get; set; }

        [MaxLength(300)]
        public string TimeStamp { get; set; }

        public Boolean HasSendMail { get; set; }

        public IList<ElementLog> Elements { get; set; }


    }

    public class ElementLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public string Status { get; set; }

        [MaxLength(300)]
        public string TimeStamp { get; set; }

        public int WebsiteID { get; set; }
    }
}
