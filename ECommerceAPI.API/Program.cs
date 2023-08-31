using ECommerceAPI.Persistence;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Application.Repositories.Product_App;
using ECommerceAPI.Persistence.Repositories.Product_Pers;
using ECommerceAPI.Application.Repositories.Customer_App;
using ECommerceAPI.Persistence.Repositories.Customer_Pers;
using ECommerceAPI.Application.Repositories.Order_App;
using ECommerceAPI.Persistence.Repositories.Order_Pers;
using FluentValidation.AspNetCore;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		//builder.Services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQL")),ServiceLifetime.Singleton);
		//builder.Services.AddSingleton<IProductReadRepository, ProductReadRepository>();
		//builder.Services.AddSingleton<IProductWriteRespository, ProductWriteRepository>();
		//builder.Services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
		//builder.Services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
		//builder.Services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
		//builder.Services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

		builder.Services.AddPersistenceServices(builder.Configuration);
		builder.Services.AddInfrastructureServices();
		builder.Services.AddStorage<LocalStorage>();
		//builder.Services.AddStorage(ECommerceAPI.Infrastructure.ServiceRegistration.StorageType.Local);


		//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));//her kes request ede biler- duzgun deyil
		builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

		builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseStaticFiles();
		app.UseCors();

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}