namespace Application.Products.Commands;

using MediatR;

public record CreateProductCommand(string Name, string Category, int Quantity, decimal UnitPrice) : IRequest<Guid>;
