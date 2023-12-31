﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230611175356_IntitialMigration")]
    partial class IntitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Domains.GradeInCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseCode")
                        .HasMaxLength(4)
                        .HasColumnType("TEXT");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StudentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("GradeInCourses");
                });

            modelBuilder.Entity("Domains.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Programme")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domains.GradeInCourse", b =>
                {
                    b.HasOne("Domains.Student", null)
                        .WithMany("Grades")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Domains.Student", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
