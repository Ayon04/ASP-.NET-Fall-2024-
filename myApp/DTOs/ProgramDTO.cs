using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace myApp.DTOs
{
    public class ProgramDTO
    {
       
        public int ProgramId { get; set; }


        [Required]
        [StringLength(50)]
  
        public string ProgramName { get; set; }
        [Required]
        [Range(0.0, 10.0)]
        public double TRPScore { get; set; }
        public int ChannelId { get; set; }
        [Required]
        public System.TimeSpan AirTime { get; set; }


    }
}