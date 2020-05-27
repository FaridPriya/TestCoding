using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi.Models.Class
{
    [Table("TodoItem")]
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Date { get; set; }
        public int? isComplete { get; set; }
    }
}
