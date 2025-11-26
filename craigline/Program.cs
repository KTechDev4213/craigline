using craigline;
using Spectre.Console.Cli;
    var app = new CommandApp();
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