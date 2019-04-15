using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services.Configuration;
using Data.Repository.Configuration;
using Domain.Model.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TurnItUpWebApi.Middleware;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure.CrossCutting.Settings;
using TurnItUpWebApi.Filters;
using System.Reflection;
using System.IO;
using System;

namespace TurnItUpWebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddDbContext<ApplicationDbContext>();

			// add identity
			var builder = services.AddIdentityCore<AppUser>(o =>
			{
				// configure identity options
				o.Password.RequireDigit = false;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 6;
			});
			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
			builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

			services.AddAutoMapper();

            // Register the ConfigurationBuilder instance of FacebookAuthSettings
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            services.ConfigureDependencies(this.Configuration);

			services.AddTokenConfiguration(this.Configuration);

			// Register the Swagger generator, defining 1 or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "TurnItUp API", Version = "v1" });

                c.OperationFilter<FormFileSwaggerFilter>();

            var security = new Dictionary<string, IEnumerable<string>>
				{
					{"Bearer", new string[] { }},
				};

				c.AddSecurityDefinition("Bearer", new ApiKeyScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = "header",
					Type = "apiKey"
				});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //c.AddSecurityRequirement(security);
            });

			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
			});

			services.AddClaimsPolicy(this.Configuration);

			services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); ;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseHttpStatusCodeExceptionMiddleware();
				app.UseSwagger();

				// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
				// specifying the Swagger JSON endpoint.
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "TurnItUp V1");

					c.DocExpansion("none");
				});
			}
			else
			{
				app.UseHttpStatusCodeExceptionMiddleware();
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			dbContext.Database.EnsureCreated();

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseIdentity();

			app.UseMvc();
		}
	}
}
