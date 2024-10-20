
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.BLL.AutoMapper.AdminAutoMapper;
using OnlineQuiz.BLL.AutoMapper.QuestionMapper;
using OnlineQuiz.BLL.AutoMapper.QuizMapper;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.BLL.Managers.QuestionManager;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Repositoryies.AdminRepositroy;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.QuestionRepository;
using OnlineQuiz.DAL.Repositoryies.QuizRepository;

namespace OnlineQuiz.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));

            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IAdminRepositroy, AdminRepositroy>();
            builder.Services.AddScoped<IAdminManger, AdminManger>();
            builder.Services.AddAutoMapper(map => map.AddProfile(new AdminMapper()));
            //GenericRepository && GenericManager
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            builder.Services.AddScoped(typeof(IManager<,>), typeof(Manager<,>));

            builder.Services.AddAutoMapper(map => map.AddProfile(new QuizMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new QuestionMapper()));

            builder.Services.AddScoped<IQuizRepository, QuizRepository>();
            builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();

            builder.Services.AddScoped<IQuizManager, QuizManager>();
            builder.Services.AddScoped<IQuestionManager, QuestionManager>();
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
