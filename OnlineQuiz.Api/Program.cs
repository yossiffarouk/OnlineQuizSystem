using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using OnlineQuiz.DAL.Data.DBHelper;
using OnlineQuiz.DAL.Data.Models;
using OnlineQuiz.DAL.Repositoryies.Base;
using OnlineQuiz.DAL.Repositoryies.TrackRepository;
using OnlineQuiz.BLL.AutoMapper.TrackMapper;
using OnlineQuiz.BLL.Managers.Track;
using OnlineQuiz.BLL.Managers.Base;
using OnlineQuiz.DAL.Repositoryies.QuizRepository;
using OnlineQuiz.BLL.Managers.Quiz;
using OnlineQuiz.BLL.AutoMapper.QuizMapper;
using OnlineQuiz.BLL.Managers;
using OnlineQuiz.BLL.Managers.QuestionManager;

using OnlineQuiz.DAL.Repositoryies.QuestionRepository;

using OnlineQuiz.BLL.AutoMapper.QuestionMapper;
using OnlineQuiz.BLL.AutoMapper.OptionMapper;
using OnlineQuiz.BLL.Managers.Student;
using OnlineQuiz.DAL.Repositoryies.StudentReposatory;
using OnlineQuiz.BLL.Managers.Accounts;
using OnlineQuiz.BLL.Dtos.Accounts;
using System.Text;
using OnlineQuiz.BLL.AutoMapper.StudentMapper;
using OnlineQuiz.DAL.Repositoryies.AttemptRepository;
using OnlineQuiz.BLL.AutoMapper.Attempt;
using OnlineQuiz.BLL.Managers.Attempt;
using AutoMapper.Internal.Mappers;
using OnlineQuiz.BLL.Managers.Admin;
using OnlineQuiz.BLL.Managers.Instructor;
using OnlineQuiz.DAL.Repositoryies.AdminRepositroy;
using OnlineQuiz.DAL.Repositoryies.InstructorRepository;
using OnlineQuiz.BLL.AutoMapper.AdminAutoMapper;
using OnlineQuiz.BLL.AutoMapper.InstructorMapper;
using OnlineQuiz.BLL.Managers.Answer;
using OnlineQuiz.DAL.Repositoryies.AnswerRepository;
using OnlineQuiz.BLL.AutoMapper.AnswerMapper;



namespace OnlineQuiz.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
            builder.Services.AddIdentity<Users, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<QuizContext>()
               .AddDefaultTokenProviders();



            // Add JWT Authentication
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Option =>
            {
                #region SecurityKey
                var SecretKeyString = builder.Configuration.GetSection("SecretKey").Value;
                var SecretKeyByte = Encoding.ASCII.GetBytes(SecretKeyString);
                SecurityKey securityKey = new SymmetricSecurityKey(SecretKeyByte);
                #endregion
                Option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                };
            });





            var app = builder.Build();

            // Call the SeedRoles method
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedRolesDtocs.SeedRoles(roleManager);
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }


    }
}
