using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi.Models.Dto
{
    public class ResultMessageDto
    {
        public string message { get; set; }
        public bool? result { get; set; }
    }
}
