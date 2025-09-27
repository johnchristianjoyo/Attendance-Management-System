using AttendanceSystem2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectionString = "mongodb://localhost:27017";
var databaseName = "AttendanceSystemDB";

builder.Services.AddSingleton<MongoDbService>(provider =>
    new MongoDbService(connectionString, databaseName));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
