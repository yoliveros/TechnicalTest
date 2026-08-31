namespace Application.Products.Commands;

using Domain.Entities;
using Infrastructure.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class CreateProductHandler(ApplicationDbContext db) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = request.Category,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice
        };

        db.Set<Product>().Add(product);
        await db.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
