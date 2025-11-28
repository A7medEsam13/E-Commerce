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
}
