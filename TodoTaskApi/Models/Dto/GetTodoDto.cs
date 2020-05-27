using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi.Models.Dto
{
    public class GetTodoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int? isComplete { get; set; }
    }
}
