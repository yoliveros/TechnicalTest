namespace Application.Products.Queries;

using MediatR;

public record LowStockProductDto(Guid Id, string Name, string Category, int Quantity, decimal UnitPrice);

public record GetLowStockProductsQuery(int Threshold) : IRequest<IEnumerable<LowStockProductDto>>;
