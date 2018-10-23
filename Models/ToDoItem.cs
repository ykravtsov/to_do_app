using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_app.Models
{
    public class ToDoItem
    {
        public int ToDOItemId { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
