using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myApp.DTOs
{
    public class ChannelDTO
    {
       
        public int ChannelId { get; set; }
        [Required]
        [StringLength(50)]
        public string ChannelName { get; set; }
        [Required]
        [Range(1900, 2024)]
        public int EstablishedYear { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
    }
}