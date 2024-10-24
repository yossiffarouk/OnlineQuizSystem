
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineQuiz.BLL.AutoMapper.AdminAutoMapper;
using OnlineQuiz.BLL.AutoMapper.AnswerMapper;
using OnlineQuiz.BLL.AutoMapper.Attempt;
using OnlineQuiz.BLL.AutoMapper.InstructorMapper;
using OnlineQuiz.BLL.AutoMapper.OptionMapper;
using OnlineQuiz.BLL.AutoMapper.QuestionMapper;
using OnlineQuiz.BLL.AutoMapper.QuizMapper;
using OnlineQuiz.BLL.AutoMapper.StudentMapper;
using OnlineQuiz.BLL.AutoMapper.TrackMapper;
using OnlineQuiz.BLL.Dtos.Accounts;
using OnlineQuiz.BLL.Managers.Accounts;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Answer;
using OnlineQuiz.BLL.Managers.Attempt;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.BLL.Managers.Instructor;
using OnlineQuiz.BLL.Managers.QuestionManager;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.Managers.Student;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.BLL.Middlewares;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.AdminRepositroy;
using OnlineQuiz.DAL.Repositoryies.AnswerRepository;
using OnlineQuiz.DAL.Repositoryies.AttemptRepository;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.InstructorRepository;
using OnlineQuiz.DAL.Repositoryies.QuestionRepository;
using OnlineQuiz.DAL.Repositoryies.QuizRepository;
using OnlineQuiz.DAL.Repositoryies.StudentReposatory;
using OnlineQuiz.DAL.Repositoryies.TrackRepository;
using System.Text;

namespace OnlineQuiz.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));

            });
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
           options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented); // Add this line for formatted JSON output

            //  token lifespan
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(3);  
            });

            //QuicContext
            builder.Services.AddDbContext<QuizContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));

            });



            //GenericRepository && GenericManager
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            builder.Services.AddScoped(typeof(IManager<,>), typeof(Manager<,>));

            //AuotMapper
            builder.Services.AddAutoMapper(map => map.AddProfile(new TrackMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new QuizMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new QuestionMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new OptionMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new StudentMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new AttemptMapping()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new AdminMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new InstructorMapper()));
            builder.Services.AddAutoMapper(map => map.AddProfile(new AnswerMapper()));


            //Repositories
            builder.Services.AddScoped<ITrackRepository, TrackRepository>();
            builder.Services.AddScoped<IQuizRepository, QuizRepository>();
            builder.Services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();
            builder.Services.AddScoped<IAdminRepositroy, AdminRepositroy>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();

            builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();


            //Managers
            builder.Services.AddScoped<IQuizManager, QuizManager>();
            builder.Services.AddScoped<IQuestionManager, QuestionManager>();
            builder.Services.AddScoped<ITrackManager, TrackManager>();
            builder.Services.AddScoped<IStudentManager, StudentManager>();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IAttemptManager, AttemptManager>();
            builder.Services.AddScoped<IAdminManger, AdminManger>();
            builder.Services.AddScoped<IInstructorManger, InstructorManger>();

            builder.Services.AddScoped<IAnswersManager, AnswersManager>();


            //Identity
            builder.Services.AddIdentity<Users, CustomRole>(options =>
            {
                //options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<QuizContext>()
               .AddDefaultTokenProviders();


            // Add Authentication and  Cookies
           builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";  // Specify the login path (UnAuthorize)
                    options.AccessDeniedPath = "/Home/Login";  // Specify the access denied path (Not Take Permissiopn)
                    options.ExpireTimeSpan = TimeSpan.FromDays(5);  // Set cookie expiration time
                   
                });

            var app = builder.Build();

            // Call the SeedRoles method
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<CustomRole>>();
                await SeedRolesDtocs.SeedRoles(roleManager);
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
          //  app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Instructor}/{action=Dashboared}");

            app.Run();
        }
    }
}
