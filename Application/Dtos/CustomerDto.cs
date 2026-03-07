using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Phone { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
