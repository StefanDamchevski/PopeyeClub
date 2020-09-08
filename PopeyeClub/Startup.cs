using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PopeyeClub.Data;
using PopeyeClub.Hubs;
using PopeyeClub.Repositories;
using PopeyeClub.Repositories.Interfaces;
using PopeyeClub.Services;
using PopeyeClub.Services.Interfaces;

namespace PopeyeClub
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PopeyeClubDbContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
            });

            services.AddSignalR();

            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddTransient<IPostLikeService, PostLikeService>();
            services.AddTransient<IPostLikeRepository, PostLikeRepository>();

            services.AddTransient<IPostCommentService, PostCommentService>();
            services.AddTransient<IPostCommentRepository, PostCommentRepository>();

            services.AddTransient<ICommentLikeService, CommentLikeService>();
            services.AddTransient<ICommentLikeRepository, CommentLikeRepository>();

            services.AddTransient<IPostSaveService, PostSaveService>();
            services.AddTransient<IPostSaveRepository, PostSaveRepository>();

            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<IFollowRepository, FollowRepository>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<INotificationRepository, NotificationRepository>();

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IMessageRepository, MessageRepository>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            }); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=Login}/{id?}");
            });
        }
    }
}
