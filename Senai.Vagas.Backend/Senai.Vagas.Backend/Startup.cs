using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Senai.Vagas.Backend.Helpers.Jobs.Implementations;
using Senai.Vagas.Infrastructure.Contexts;

namespace Senai.Vagas.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configura o AutoMapper
            // E onde irá encontrar os Profiles (perfis)
            services.AddAutoMapper(typeof(Startup).Assembly);

            //DependencyInjecions(DI) Vinculando a dependencia da classe repository com a InterfaceRepository
            //Em outras palavras, você pode usar a implementação dos métodos do repository, apenas instanciando... A interface! loucura neh? Foi o que eu pensei também!
            
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Vinculando Interfaces Queries com a implementação Queries
            
            //services.AddTransient<IInterfaceQueries, Queries>();

            //**
            //Configurações da DB
            //**
            services.AddDbContext<SenaiVagasContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Configurações swagger (melhor entendimento dos endpoints controllers) documentação
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Senai Vagas", Version = "v1" });

                //Adiciona os comentários (summary) do controller
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //*
            //Configurando o JWT (Autentificação)
            //*
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //Verificando...

                    //Quem esta solicitando
                    ValidateIssuer = true,

                    //Quem esta recebendo
                    ValidateAudience = true,

                    //Valida o tempo de expiração do token
                    ValidateLifetime = true,

                    //Forma da criptografia
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senaivagas-chave-autentificacao")),

                    //Tempo de expiração do token
                    ClockSkew = TimeSpan.FromMinutes(30),

                    // Nome da issuer, de onde está vindo
                    ValidIssuer = "Senai.Vagas.Backend",

                    // Nome da audience, de onde está vindo
                    ValidAudience = "Senai.Vagas.Backend"
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime, IServiceScopeFactory services)
        {
            //Método que aplica jobs sempre que a API é inicializada. (registra o serviço na inicialização)
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                app.ConfigureJobsAsync();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Senai.Vagas.Backend");
            });

            // Habilita o uso de autenticação
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
