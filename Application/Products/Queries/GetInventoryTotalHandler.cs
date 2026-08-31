namespace Application.Products.Queries;

using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;

public class GetInventoryTotalQueryHandler(IDbConnection db) : IRequestHandler<GetInventoryTotalQuery, IEnumerable<InventoryDTO>>
{
    public async Task<IEnumerable<InventoryDTO>> Handle(GetInventoryTotalQuery request, CancellationToken cancellationToken)
    {
        var result = await db.QueryAsync<InventoryDTO>(
            "spGetInventoryValueByCategory",
            commandType: CommandType.StoredProcedure
            );
        return result;
    }
}

