using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizPlatformProject.Migrations
{
    public partial class editEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossibleAnswers_Questions_QuestionId",
                table: "PossibleAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "PossibleAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleAnswers_Questions_QuestionId",
                table: "PossibleAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PossibleAnswers_Questions_QuestionId",
                table: "PossibleAnswers");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "PossibleAnswers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PossibleAnswers_Questions_QuestionId",
                table: "PossibleAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
