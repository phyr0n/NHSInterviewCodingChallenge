using Microsoft.EntityFrameworkCore;
using NHSInterviewCodingChallenge.Data;
using NHSInterviewCodingChallenge.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddDbContext<NHSAppDbContext>(options => options.UseInMemoryDatabase("MockedPatientsDb"));
builder.Services.AddOpenApi();
builder.Services.AddScoped<IPatientService, PatientService>();

WebApplication app = builder.Build();

// Populate the in-memory database from the mocked data.
using (IServiceScope scope = app.Services.CreateScope())
{
    NHSAppDbContext database = scope.ServiceProvider.GetRequiredService<NHSAppDbContext>();
    database.Populate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();