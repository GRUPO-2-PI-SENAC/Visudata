using PI.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:55186",
                                              "http://localhost:55187");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



//app.UseCors(builder =>
//{
//    builder
//       .WithOrigins("https://localhost:55186", "http://localhost:55187")
//       .SetIsOriginAllowedToAllowWildcardSubdomains()
//       .AllowAnyHeader()
//       .AllowCredentials()
//       .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
//       .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
//}
//);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
