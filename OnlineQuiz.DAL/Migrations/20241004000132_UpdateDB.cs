using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_MultipleChoicesQuestions_MultipleChoicesQuestionId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "MultipleChoicesQuestions");

            migrationBuilder.DropTable(
                name: "TrueAndFalseQuestions");

            migrationBuilder.DropTable(
                name: "MultipleChoicesQuizzes");

            migrationBuilder.DropTable(
                name: "TrueAndFalseQuizzes");

            migrationBuilder.DropIndex(
                name: "IX_Options_MultipleChoicesQuestionId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "MultipleChoicesQuestionId",
                table: "Options");

            migrationBuilder.DropSequence(
                name: "QuestionsSequence");

            migrationBuilder.DropSequence(
                name: "QuizzesSequence");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Options",
                newName: "QuestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                newName: "IX_Options_QuestionsId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizzes_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quizzes_tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questions_quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuizId",
                table: "questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_InstructorId",
                table: "quizzes",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_TracksId",
                table: "quizzes",
                column: "TracksId");

            migrationBuilder.AddForeignKey(
                name: "FK_answers_questions_QuestionId",
                table: "answers",
                column: "QuestionId",
                principalTable: "questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_attempts_quizzes_QuizId",
                table: "attempts",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_questions_QuestionsId",
                table: "Options",
                column: "QuestionsId",
                principalTable: "questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_answers_questions_QuestionId",
                table: "answers");

            migrationBuilder.DropForeignKey(
                name: "FK_attempts_quizzes_QuizId",
                table: "attempts");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_questions_QuestionsId",
                table: "Options");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.RenameColumn(
                name: "QuestionsId",
                table: "Options",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_QuestionsId",
                table: "Options",
                newName: "IX_Options_QuestionId");

            migrationBuilder.CreateSequence(
                name: "QuestionsSequence");

            migrationBuilder.CreateSequence(
                name: "QuizzesSequence");

            migrationBuilder.AddColumn<int>(
                name: "MultipleChoicesQuestionId",
                table: "Options",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MultipleChoicesQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuizzesSequence]"),
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TracksId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    QuizDegree = table.Column<int>(type: "int", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
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
                    InstructorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TracksId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    QuizDegree = table.Column<int>(type: "int", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
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
                name: "MultipleChoicesQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [QuestionsSequence]"),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
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
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tittle = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Options_MultipleChoicesQuestionId",
                table: "Options",
                column: "MultipleChoicesQuestionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Options_MultipleChoicesQuestions_MultipleChoicesQuestionId",
                table: "Options",
                column: "MultipleChoicesQuestionId",
                principalTable: "MultipleChoicesQuestions",
                principalColumn: "Id");
        }
    }
}
