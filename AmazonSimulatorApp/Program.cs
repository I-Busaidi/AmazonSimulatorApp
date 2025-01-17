using AmazonSimulatorApp.Components;
using AmazonSimulatorApp.Data.Repositories;
using AmazonSimulatorApp.Repositories;
using AmazonSimulatorApp.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace AmazonSimulatorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(

                options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

                );

            builder.Services.AddScoped<IClientRepo, ClientRepo>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IUserRepo, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISellerRepo, SellerRepo>();
            builder.Services.AddScoped<ISellerService, SellerService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();
            builder.Services.AddScoped<IProductReviewService, ProductReviewService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
            builder.Services.AddScoped<ICompoundService, CompoundService>();
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
