using BookStoreManager.Interface;
using BookStoreManager.Manager;
using BookStoreManagers.Interface;
using BookStoreManagers.Manager;
using BookstoreRepository.Interface;
using BookstoreRepository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRepository;
using UserRepository.Interface;

namespace BookStoreApplication
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
            services.AddMvc();
            services.AddTransient<IuserRepo, userRepo>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IBookRepo, BookRepo>();
            services.AddTransient<IBookManager, BookManager>();
            services.AddTransient<ICartRepo, CartRepo>();
            services.AddTransient<ICartManager, CartManager>();
            services.AddTransient<IWishListRepo, WishListRepo>();
            services.AddTransient<IWishListManager, WishListManager>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IFeedBackRepo, FeedBackRepo>();
            services.AddTransient<IFeedBackManager, FeedBackManager>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
