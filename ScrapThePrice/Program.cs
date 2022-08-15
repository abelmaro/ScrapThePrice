using OpenQA.Selenium.Chrome;
using ScrapThePrice.Services;
using ScrapThePrice.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Services registration
builder.Services.AddScoped<IMLScrappingService, MLScrappingService>();
builder.Services.AddScoped<IOLXScrappingService, OLXScrappingService>();
builder.Services.AddScoped<IFravegaScrappingService, FravegaScrappingService>();
builder.Services.AddScoped<IProductHelperService, ProductHelperService>();
builder.Services.AddTransient<IWebDriverService, WebDriverService>();
builder.Services.AddTransient<IProductsService, ProductsService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => {
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    });

app.Run();
