/***************************************************************************************
*                                                                                      *
*    Que diremos, pois, a estas coisas? Se Deus é por nós, quem será contra nós?       *
*                                                                                      *
*    Romanos 8:31                                                                      *
*                                                                                      *
****************************************************************************************/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services
    .AddInfrastructureApi(builder.Configuration)
    .AddInfrastructureJWT()
    .AddInfrastructureSwagger()
    .AddInfrastructureCORS(builder.Configuration);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paschoalotto Carteira API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(builder.Configuration.GetSection("Cors:PolicyName").Value!);
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
