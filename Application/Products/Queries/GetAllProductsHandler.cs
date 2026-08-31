namespace Application.Products.Queries;

using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;

public class GetAllProductsHandler(IDbConnection db) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var sql = "SELECT Id, Name, Category, Quantity, UnitPrice FROM Products";
        var products = await db.QueryAsync<ProductDto>(sql);
        return products;
    }
}
