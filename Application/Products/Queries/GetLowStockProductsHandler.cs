namespace Application.Products.Queries;

using Dapper;
using MediatR;
using System.Data;

public class GetLowStockProductsHandler(IDbConnection db) : IRequestHandler<GetLowStockProductsQuery, IEnumerable<LowStockProductDto>>
{
    public async Task<IEnumerable<LowStockProductDto>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
    {
        var sql = "SELECT Id, Name, Category, Quantity, UnitPrice FROM Products WHERE Quantity <= @Threshold";
        var result = await db.QueryAsync<LowStockProductDto>(sql, new { request.Threshold });
        return result;
    }
}
