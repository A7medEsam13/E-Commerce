using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.DTOs
{
    public record ProductDto
    (
        int Id,
        string Name,
        string Description,
        decimal Price,
        int CategoryId,
        List<string> PhotoUrls
    );

    public record AddProductDto
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal OldPrice {set;get;}
        public decimal NewPrice {set;get;}
        public IFormFileCollection Photos { set; get; }
        public int CategoryId { set; get; }
    }
}
