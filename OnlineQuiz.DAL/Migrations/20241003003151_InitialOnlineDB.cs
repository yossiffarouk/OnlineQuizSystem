using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialOnlineDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "QuestionsSequence");

            migrationBuilder.CreateSequence(
                name: "QuizzesSequence");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    InstructorIsApproved = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false),
                    StateForExam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attempts_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorStudent",
                columns: table => new
                {
                    InstructorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorStudent", x => new { x.InstructorsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InstructorStudent_AspNetUsers_InstructorsId",
                        column: x => x.InstructorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstructorStudent_AspNetUsers_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoicesQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuizzesSequence]"),
                    Tittle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    QuizDegree = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    TracksId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoicesQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoicesQuizzes_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultipleChoicesQuizzes_tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrueAndFalseQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuizzesSequence]"),
                    Tittle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    QuizDegree = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    TracksId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueAndFalseQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrueAndFalseQuizzes_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrueAndFalseQuizzes_tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmittedAnswer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_answers_attempts_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "attempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoicesQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuestionsSequence]"),
                    Tittle = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    CorrectOptionId = table.Column<int>(type: "int", nullable: false),
                    MultipleChoicesQuizId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoicesQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultipleChoicesQuestions_MultipleChoicesQuizzes_MultipleChoicesQuizId",
                        column: x => x.MultipleChoicesQuizId,
                        principalTable: "MultipleChoicesQuizzes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrueAndFalseQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuestionsSequence]"),
                    Tittle = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    IsTrue = table.Column<bool>(type: "bit", nullable: false),
                    TrueAndFalseQuizId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueAndFalseQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrueAndFalseQuestions_TrueAndFalseQuizzes_TrueAndFalseQuizId",
                        column: x => x.TrueAndFalseQuizId,
                        principalTable: "TrueAndFalseQuizzes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    MultipleChoicesQuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_Options_MultipleChoicesQuestions_MultipleChoicesQuestionId",
                        column: x => x.MultipleChoicesQuestionId,
                        principalTable: "MultipleChoicesQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_AttemptId",
                table: "answers",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_answers_QuestionId",
                table: "answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_attempts_QuizId",
                table: "attempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_attempts_StudentId",
                table: "attempts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorStudent_StudentsId",
                table: "InstructorStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoicesQuestions_MultipleChoicesQuizId",
                table: "MultipleChoicesQuestions",
                column: "MultipleChoicesQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoicesQuestions_QuizId",
                table: "MultipleChoicesQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoicesQuizzes_InstructorId",
                table: "MultipleChoicesQuizzes",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoicesQuizzes_TracksId",
                table: "MultipleChoicesQuizzes",
                column: "TracksId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_MultipleChoicesQuestionId",
                table: "Options",
                column: "MultipleChoicesQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueAndFalseQuestions_QuizId",
                table: "TrueAndFalseQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueAndFalseQuestions_TrueAndFalseQuizId",
                table: "TrueAndFalseQuestions",
                column: "TrueAndFalseQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueAndFalseQuizzes_InstructorId",
                table: "TrueAndFalseQuizzes",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_TrueAndFalseQuizzes_TracksId",
                table: "TrueAndFalseQuizzes",
                column: "TracksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "InstructorStudent");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "TrueAndFalseQuestions");

            migrationBuilder.DropTable(
                name: "attempts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MultipleChoicesQuestions");

            migrationBuilder.DropTable(
                name: "TrueAndFalseQuizzes");

            migrationBuilder.DropTable(
                name: "MultipleChoicesQuizzes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tracks");

            migrationBuilder.DropSequence(
                name: "QuestionsSequence");

            migrationBuilder.DropSequence(
                name: "QuizzesSequence");
        }
    }
}
