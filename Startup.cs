using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesafioApiRest.Model;



namespace DesafioApiRest
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
            services.AddDbContext<ApiContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString("MyDb")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio Api Rest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Api Rest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApiContext>();
            SeedData(context);
        }

        public static void SeedData(ApiContext context)
        {
            Produto prd1 = new Produto
            {
                Id = 1,
                Nome = "Achocolatado Nescau",
                Codigo_barras = "7891000338087",
                Preco = 18.60M
            };

            Produto prd2 = new Produto
            {
                Id = 2,
                Nome = "Leite em Po Nestle",
                Codigo_barras = "7891000259047",
                Preco = 16.40M
            };



            context.Produtos.AddRange(prd1,prd2);

            //adicionar Loja

            Loja lj1 = new Loja
            {
                Id = 1,
                Nome = "Supermercado Menor Preco",
                Razao = "Com. Var. Alimentos Ltda",
                Cnpj = "111.111.111/0001-11",
                Endereco = "Rua de Teste",
                Numero = "999",
                Bairro = "Novo Teste",
                Cep = "22333-444",
                Cidade = "Cidade Grande",
                Uf = "PB"
            };

            context.Lojas.Add(lj1);


            //adicionar um estoque inicial para cada produto
            Estoque est1 = new Estoque
            {
                Id = 11,
                produto_id = 1,
                loja_id = 1,
                quantidade = 10M,
                DataRegistro =  DateTime.Now
            };

            Estoque est2 = new Estoque
            {
                Id = 12,
                produto_id = 2,
                loja_id = 1,
                quantidade = 12M,
                DataRegistro = DateTime.Now
            };

            context.Estoques.AddRange(est1, est2);


            context.SaveChanges();
        }
    }
}
