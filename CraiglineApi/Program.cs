var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication(builder =>
{
    builder.DefaultAuthenticateScheme = "Bearer";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();


app.MapGet("search", (string? query) =>
{
    if (string.IsNullOrWhiteSpace(query))
    {
        return Results.BadRequest("Query parameter is required.");
    }

    var results = new[]
    {
        $"Result 1 for '{query}'",
        $"Result 2 for '{query}'",
        $"Result 3 for '{query}'"
    };
    return Results.Ok(results);
})
    .WithOpenApi();

app.MapPost("product", (Post product) =>
{
    if(product == null)
    {
        return Results.BadRequest("Product is required.");
    }
    return Results.Ok(new Post(23, 69, "Bigg ass dildo", "A really big dildo", 3000));
})
    .WithOpenApi();

app.MapGet("product", (int? id) =>
{
    if(id == null)
    {
        return Results.BadRequest("Product ID is required");
    }
    return Results.Ok(new Post(69, 1, "Fuel - Eminem", "He didn't just spell rapper without a p did he.", 6969));
})
    .WithOpenApi();

app.MapGet("user", (int? id) =>
{
    if (id == null)
    {
        return Results.BadRequest("Bro what's ur id");
    }
    return Results.Ok(new User(67, "Loser", "fu@ass.hole"));
})
    .WithOpenApi();

app.MapPost("user", (User? user) =>
{
    if (user == null)
    {
        return Results.BadRequest("Invalid loser data");
    }
    return Results.Ok();
})
    .WithOpenApi();


app.Run();


public record Post(int Id, int UserId, string Name, string Description, decimal Price);
public record User(int Id, string UserName, string Email);