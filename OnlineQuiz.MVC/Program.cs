
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.BLL.AutoMapper.AdminAutoMapper;
using OnlineQuiz.BLL.AutoMapper.Attempt;
using OnlineQuiz.BLL.AutoMapper.OptionMapper;
using OnlineQuiz.BLL.AutoMapper.InstructorMapper;
using OnlineQuiz.BLL.AutoMapper.QuestionMapper;
using OnlineQuiz.BLL.AutoMapper.QuizMapper;
using OnlineQuiz.BLL.AutoMapper.StudentMapper;
using OnlineQuiz.BLL.AutoMapper.TrackMapper;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Attempt;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.BLL.Managers.Instructor;
using OnlineQuiz.BLL.Managers.QuestionManager;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Student;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Repositoryies.AdminRepositroy;
using OnlineQuiz.DAL.Repositoryies.AttemptRepository;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.InstructorRepository;
using OnlineQuiz.DAL.Repositoryies.QuestionRepository;
using OnlineQuiz.DAL.Repositoryies.QuizRepository;
using OnlineQuiz.DAL.Repositoryies.StudentReposatory;
using OnlineQuiz.DAL.Repositoryies.TrackRepository;

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


            builder.Services.AddAutoMapper(typeof(Mapper)); 

            builder.Services.AddAutoMapper(map => map.AddProfile(new QuizMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new QuestionMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new TrackMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new StudentMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new InstructorMapper()));
    
            builder.Services.AddAutoMapper(map => map.AddProfile(new OptionMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new StudentMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new AttemptMapping()));

            builder.Services.AddScoped<IQuizRepository, QuizRepository>();
            builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();


            builder.Services.AddScoped<IQuizManager, QuizManager>();
            builder.Services.AddScoped<IQuestionManager, QuestionManager>();
            builder.Services.AddScoped<ITrackManager, TrackManager>();
            builder.Services.AddScoped<IStudentManager, StudentManager>();
            builder.Services.AddScoped<IInstructorManger, InstructorManger>();
            builder.Services.AddScoped<IStudentManager, StudentManager>();

            builder.Services.AddScoped<IAttemptManager, AttemptManager>();


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
                pattern: "{controller=Admin}/{action=DashBoard}");

            app.Run();
        }
    }
}
