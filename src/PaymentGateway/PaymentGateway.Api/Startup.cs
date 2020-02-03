using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.Api.Settings;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Services.AcquiringBank;
using PaymentGateway.Data;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Domain.Repositories;

namespace PaymentGateway.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<OAuthSettings>(Configuration.GetSection(nameof(OAuthSettings)));

            services.Configure<AcquiringBankSettings>(Configuration.GetSection(nameof(AcquiringBankSettings)));

            services.AddDbContext<PaymentGatewayDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient("AcquiringBank", c =>
            {
                var acquiringBankSettings = Configuration.GetSection(nameof(AcquiringBankSettings)).Get<AcquiringBankSettings>();
                c.BaseAddress = new Uri(acquiringBankSettings.BaseUrl);
            })
            .AddTypedClient(c => Refit.RestService.For<IAcquiringBankClient>(c));

            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    var oauthSettings = Configuration.GetSection(nameof(OAuthSettings)).Get<OAuthSettings>();

                    options.Authority = oauthSettings.Authority;
                    options.RequireHttpsMetadata = false;

                    options.Audience = "payment-gateway";
                });

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var applicationAssembly = typeof(ProcessPaymentRequestCommandValidator).Assembly;
            services.AddMediatR(applicationAssembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
