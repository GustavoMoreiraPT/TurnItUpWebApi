using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

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

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TurnItUp API", Version = "v1" });

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
	            //c.AddSecurityRequirement(security);
			});

			//==== Add JTW Stuff=====
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;

					cfg.SaveToken = true;

					cfg.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = Configuration["JwtSettings:JwtIssuer"],

						ValidAudience = Configuration["JwtSettings:JwtIssuer"],

						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:JwtKey"])),

						ClockSkew = TimeSpan.Zero // remove delay of token when expire
					};
				})
				.AddCookie(cfg => cfg.SlidingExpiration = true);

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
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
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			dbContext.Database.EnsureCreated();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
