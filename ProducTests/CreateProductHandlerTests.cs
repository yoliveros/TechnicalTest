using Application.Products.Commands;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CreateProductHandlerTests
{
    [Fact]
    public async Task HandlerCreatesProductInDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_CreateProduct")
            .Options;

        await using var db = new ApplicationDbContext(options);
        var handler = new CreateProductHandler(db);

        var cmd = new CreateProductCommand("Test Prod", "Cat A", 10, 2.5m);
        var id = await handler.Handle(cmd, CancellationToken.None);

        var saved = await db.Products.FindAsync(id);
        Assert.NotNull(saved);
        Assert.Equal("Test Prod", saved.Name);
        Assert.Equal(10, saved.Quantity);
    }
}
