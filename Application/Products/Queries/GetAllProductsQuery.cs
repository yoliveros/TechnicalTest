namespace Application.Products.Queries;

using MediatR;
using System.Collections.Generic;

public record ProductDto(Guid Id, string Name, string Category, int Quantity, decimal UnitPrice);

public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
