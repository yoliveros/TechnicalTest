namespace Application.Products.Queries;

using MediatR;

public record InventoryDTO(string Category, decimal TotalValue);
public record GetInventoryTotalQuery : IRequest<IEnumerable<InventoryDTO>>;
