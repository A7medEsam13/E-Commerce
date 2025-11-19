using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.DTOs
{
    public record CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
