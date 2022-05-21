using ASPNETCore_SignalRWebpack.Hubs;
using ASPNETCore_SignalRWebpack.Services;

var builder = WebApplication.CreateBuilder(args);
//The code added below allows the server to locate and serve the index.html file

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                      policy =>
                      {
                          policy.SetIsOriginAllowed(origin => true)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                      });
});

//Adding SignalR Service
builder.Services.AddSignalR();
builder.Services.AddHostedService<TimedHostedService>();

var app = builder.Build();

//Enables default file mapping on the current path
app.UseDefaultFiles();
//Enables static file serving for the current request path
app.UseStaticFiles();


//app.MapGet("/", () => "Hello World!");
app.MapHub<ChatHub>("/Hub");

app.UseCors("MyPolicy");

app.Run();
