﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTaskMVC.Data;

namespace TestTaskMVC.Migrations
{
    [DbContext(typeof(TestTaskMVCContext))]
    [Migration("20191208203030_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestTaskMVC.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday");

                    b.Property<bool>("IsMale");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("Request");

                    b.HasKey("ID");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
