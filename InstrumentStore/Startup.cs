using AutoMapper;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interfaces;
using InstrumentsShop.Providers.MapperProfiles;
using InstrumentStore.DbInit;
using InstrumentStore.Providers;
using InstrumentStore.Providers.Interfaces;
using InstrumentStore.Services;
using InstrumentStore.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace InstrumentStore
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
            AddDbServices(services);
            AddControllerServices(services);
            AddStorageProviders(services);
            ConfigureMapper(services);

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            services.AddControllersWithViews();
        }

        private void ConfigureMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new BrandProfile());
                mc.AddProfile(new OrderProfile());
                mc.AddProfile(new InstrumentProfile());
                mc.AddProfile(new StoreProfile());
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddStorageProviders(IServiceCollection services)
        {
            services.AddScoped<IBrandsStorageProvider, BrandsStorageProvider>();
            services.AddScoped<IInstrumentsStorageProvider, InstrumentsStorageProvider>();
            services.AddScoped<IOrdersStorageProvider, OrdersStorageProvider>();
            services.AddScoped<IStoreStorageProvider, StoreStorageProvider>();
        }

        private void AddControllerServices(IServiceCollection services)
        {
            services.AddScoped<IBrandsService, BrandsService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IInstrumentsService, InstrumentsService>();
            services.AddScoped<IStoreService, StoreService>();
        }

        private void AddDbServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnetionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IInstrumentsRepository, InstrumentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreItemRepository, StoreItemRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            //Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Instruments}/{action=Index}/{id?}");
            });

            //Seed database
            ApplicationDbInitializer.Seed(app);
            ApplicationDbInitializer.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}
