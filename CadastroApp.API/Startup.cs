using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using cadastro.Models;
using cadastro.Data;

namespace cadastro {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
             services.AddScoped<ClienteRepository>();
            //especifica o esquema usado para autenticacao do tipo Bearer
            // e 
            //define configurações como chave,algoritmo,validade, data expiracao...
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "aplicacao",
                    ValidAudience = "canal",
                    IssuerSigningKey = new SymmetricSecurityKey (
                    Encoding.UTF8.GetBytes (Configuration["SecurityKey"]))
                    };

                    options.Events = new JwtBearerEvents {
                        OnAuthenticationFailed = context => {
                                Console.WriteLine ("Token inválido..:. " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context => {
                                Console.WriteLine ("Toekn válido...: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                    };
                });

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo { Title = "Cadastro", Version = "v1" });
                // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                c.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "bearer"
                });

                c.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        }, new List<string> ()
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseAuthentication ();
            app.UseMvc ();
            app.UseSwagger ();

            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Cadastro");
            });

        }
    }
}