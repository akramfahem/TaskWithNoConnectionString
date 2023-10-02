using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using MyOwnLogger.Data;
using AutoMapper;
using MyOwnLogger.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddRazorPages();
builder.Services.AddLocalization(opt => opt.ResourcesPath = "Resources");
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient<IStudentDataService, StudentDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<IUniversityDataService, UniversityDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<ICollegeDataService, CollegeDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<IDoctorDataService,DoctorDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<ISubjectDataService, SubjectDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<IEnrollmentDataService, EnrollmentDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddHttpClient<ILoggerDataService, LoggerDataService>(client => client.BaseAddress = new Uri("http://localhost:5004"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
var SupportedCulters = new[] { "en", "ar" };
var LoclizationOptions = new RequestLocalizationOptions().SetDefaultCulture(SupportedCulters[0]).AddSupportedCultures(SupportedCulters).AddSupportedUICultures(SupportedCulters);
app.UseRequestLocalization(LoclizationOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

