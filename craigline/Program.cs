using craigline;
using Spectre.Console.Cli;
using Microsoft.Extensions.DependencyInjection;

var registrations = new ServiceCollection();
registrations.AddSingleton<ITokenStore, TokenStore>();
registrations.AddSingleton<CraigClient>();
var registrar = new ServiceCollectionRegistrar(registrations);
var app = new CommandApp(registrar);
app.Configure(config =>
{
        config.SetApplicationName("craigline");
        config.AddCommand<SearchCommand>("search")
            .WithDescription("Search Craigslist");
        config.AddCommand<PostCommand>("post")
            .WithDescription("Post to Craigslist");
        config.AddCommand<AuthCommand>("auth")
            .WithDescription("Sign in");
    });
app.Run(args);