using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using CraiglineApi;
using FileContextCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication(builder =>
{
    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.Audience = "craigline_api";
    options.Authority = "https://dev-8att7jypkdqyxipd.us.auth0.com/";
});
builder.Services.AddAuthorization();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseFileContextDatabase();
    else if (builder.Environment.IsProduction())
        options.UseSqlite("");
});
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();


app.MapGet("search", (string? query, AppDbContext dbContext) =>
{
    if (string.IsNullOrWhiteSpace(query))
    {
        return Results.BadRequest("Query parameter is required.");
    }
    //Very besic search function. Need to add sorting and only return a specified number of results.
    var results = dbContext.Posts.Where(p => p.Name.Contains(query) || p.Description.Contains(query))
        .ToList();

    return Results.Ok(results);
})
    .WithOpenApi();

app.MapPost("product", (Post product, AppDbContext dbContext, UserService userService) =>
{
    if (product == null)
    {
        return Results.BadRequest("Product is required.");
    }
    if (product.UserId != userService.GetUserId())
    {
        return Results.BadRequest("Invalid user ID.");
    }
    dbContext.Posts.Add(product);
    return Results.Ok();
})
    .WithOpenApi()
    .RequireAuthorization();

app.MapGet("product", (string? id, AppDbContext dbContext) =>
{
    if(id == null)
    {
        return Results.BadRequest("Product ID is required");
    }
    return Results.Ok(dbContext.Posts.Where(p => p.Id == id).FirstOrDefault());
})
    .WithOpenApi();

app.MapGet("user", (AppDbContext dbContext, UserService userService) =>
{
    return Results.Ok(dbContext.Users.Where(u=> u.Id == userService.GetUserId()));
})
    .WithOpenApi()
    .RequireAuthorization();

app.MapPost("user", (User? user, AppDbContext dbContext, UserService userService) =>
{
    if (user == null)
    {
        return Results.BadRequest("Invalid loser data");
    }
    
    return Results.Ok();
})
    .WithOpenApi();


app.Run();


public record Post(string Id, string UserId, string Name, string Description, decimal Price);
public record User(string Id, string UserName, string Email);