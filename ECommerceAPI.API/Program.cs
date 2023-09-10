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
using ECommerceAPI.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Serilog.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using Serilog.Context;
using ECommerceAPI.API.Configurations.ColumnWriters;
using Microsoft.AspNetCore.HttpLogging;
using ECommerceAPI.API.Extensions;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		// Add services to the container.

		builder.Services.AddHttpLogging(logging =>
		{
			logging.LoggingFields = HttpLoggingFields.All;
			logging.RequestHeaders.Add("sec-ch-ua");
			logging.MediaTypeOptions.AddText("application/javascript");
			logging.RequestBodyLogLimit = 4096;
			logging.ResponseBodyLogLimit = 4096;

		});


		//builder.Services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQL")),ServiceLifetime.Singleton);
		//builder.Services.AddSingleton<IProductReadRepository, ProductReadRepository>();
		//builder.Services.AddSingleton<IProductWriteRespository, ProductWriteRepository>();
		//builder.Services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
		//builder.Services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
		//builder.Services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
		//builder.Services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

		builder.Services.AddpplicationService();
		builder.Services.AddPersistenceServices(builder.Configuration);
		builder.Services.AddInfrastructureServices();
		builder.Services.AddStorage<LocalStorage>();
		//builder.Services.AddStorage(ECommerceAPI.Infrastructure.ServiceRegistration.StorageType.Local);

		Logger log = new LoggerConfiguration()
			.WriteTo.Console()
			.WriteTo.File("logs/log.txt")
			.WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",needAutoCreateTable:true,columnOptions: new Dictionary<string, ColumnWriterBase>
			{
				{"message",new RenderedMessageColumnWriter() },
				{"message_template",new MessageTemplateColumnWriter() },
				{"level",new LevelColumnWriter() },
				{"time_stamp",new TimestampColumnWriter() },
				{"exception",new ExceptionColumnWriter() },
				{"log_event",new LogEventSerializedColumnWriter() },
				{"user_name",new UsernameColumnWriter() },
			})
			.WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
			.Enrich.FromLogContext()
			.MinimumLevel.Information()
			.CreateLogger();

		builder.Host.UseSerilog(log);

		//builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));//her kes request ede biler- duzgun deyil
		builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

		builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>()).AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer("Admin", options =>
			{

				options.TokenValidationParameters = new()
				{
				
					
					ValidateAudience = true,//tokenin kimler, hansi origin/saytlarin ist. edecegini teyin edir - www.ecommerce.app
					ValidateIssuer = true, //tokeni kim paylasacaq - www.myapi.com
					ValidateLifetime = true,//tokenin muddetini teyin edir
					ValidateIssuerSigningKey = true,//tokenin bizim appa aid oldugunu teyin edir

					ValidAudience = builder.Configuration["Token:Audience"],
					ValidIssuer = builder.Configuration["Token:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
					LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
					NameClaimType = ClaimTypes.Name//JWT name = User.Identity.Name
				};
			});



		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseHttpLogging();

		//app.UseExceptionHandler();
		app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

		app.UseSerilogRequestLogging();//ozunden sonrakilari loglayir


		app.UseStaticFiles();
		app.UseCors();

		//app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();
		app.Use(async(context, next) =>
		{
			var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
			LogContext.PushProperty("user_name", username);
			await next();
		});

		app.MapControllers();

		app.Run();
	}
}