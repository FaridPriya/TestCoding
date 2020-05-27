using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi.Models.Dto
{
    public class InsertTodoDto
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Boolean? isComplete { get; set; }
    }
}
