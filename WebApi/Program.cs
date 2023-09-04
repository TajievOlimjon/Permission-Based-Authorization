var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AuthService(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerService();

var app = builder.Build();

await using var scope = app.Services.CreateAsyncScope();
var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await applicationDbContext.Database.MigrateAsync();
await RolePermissionsConfiguration.AddPermissionsToRoles(applicationDbContext);
app.Logger.LogInformation("Application Starting");


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web auth api");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
