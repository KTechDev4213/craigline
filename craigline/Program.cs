using craigline;
using Spectre.Console.Cli;
var app = new CommandApp();
app.Configure(config =>
{
    config.SetApplicationName("craigline");
    config.AddCommand<SearchCommand>("search")
        .WithDescription("Search Craigline.");
});
app.Run(args);