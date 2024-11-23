using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myApp.DTOs
{
    public class ProgramGroupDTO
    {
        public string ChannelName { get; set; }
        public List<ProgramDTO> Programs { get; set; }

    }
}