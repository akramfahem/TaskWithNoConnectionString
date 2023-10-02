using APII.Model;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IStudentRepositry, StudentRepositry>();
builder.Services.AddScoped<IUniversityRepositry, UniverstiyRepositry>();
builder.Services.AddScoped<ICollegeRepositry, CollegeRepositry>();
builder.Services.AddScoped<IDoctorRepositry, DoctorRepositry>();
builder.Services.AddScoped<ISubjectRepositry, SubjectRepositry>();
builder.Services.AddScoped<iEnrollmentRepositry, EnrollmentRepositry>();
//Argumentnullexception.throwifull(nameof(logger));
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/Users/akram/Projects/Task2/MyOwnLogger/MyApi")).SetDefaultKeyLifetime(TimeSpan.FromDays(90));
builder.Services.AddScoped<IDataProtector>(serviceProvider =>
{
    var dataProtectionProvider = serviceProvider.GetRequiredService<IDataProtectionProvider>();
    var protector = dataProtectionProvider.CreateProtector("So");
    return protector;
});

builder.Services.AddCors(x => x.AddPolicy("MyPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyMethod()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();

