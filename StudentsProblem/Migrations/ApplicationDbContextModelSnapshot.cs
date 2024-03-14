﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsProblem.Models;

#nullable disable

namespace StudentsProblem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentsProblem.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("StudentsProblem.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentsProblem.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("StudentsProblem.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("School");
                });

            modelBuilder.Entity("StudentsProblem.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("Indeks")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentsProblem.Models.StudentCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("StudentsProblem.Models.School", b =>
                {
                    b.HasOne("StudentsProblem.Models.Address", "Address")
                        .WithOne("School")
                        .HasForeignKey("StudentsProblem.Models.School", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("StudentsProblem.Models.Student", b =>
                {
                    b.HasOne("StudentsProblem.Models.Address", "Address")
                        .WithOne("Student")
                        .HasForeignKey("StudentsProblem.Models.Student", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsProblem.Models.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("School");
                });

            modelBuilder.Entity("StudentsProblem.Models.StudentCourse", b =>
                {
                    b.HasOne("StudentsProblem.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("StudentsProblem.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentsProblem.Models.Address", b =>
                {
                    b.Navigation("School");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentsProblem.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("StudentsProblem.Models.School", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentsProblem.Models.Student", b =>
                {
                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
