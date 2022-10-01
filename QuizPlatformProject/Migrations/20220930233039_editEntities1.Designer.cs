﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizPlatformProject.Data;

namespace QuizPlatformProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220930233039_editEntities1")]
    partial class editEntities1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QuizPlatformProject.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Models.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rank");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.PossibleAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answare")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("PossibleAnswers");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CorrectAnswer")
                        .HasColumnType("int");

                    b.Property<int>("RankId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RankId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.PossibleAnswer", b =>
                {
                    b.HasOne("QuizPlatformProject.Data.Question", "Question")
                        .WithMany("PossibleAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Question", b =>
                {
                    b.HasOne("QuizPlatformProject.Data.Models.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuizPlatformProject.Data.Models.Rank", "Rank")
                        .WithMany("Questions")
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Models.Category", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Models.Rank", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuizPlatformProject.Data.Question", b =>
                {
                    b.Navigation("PossibleAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
