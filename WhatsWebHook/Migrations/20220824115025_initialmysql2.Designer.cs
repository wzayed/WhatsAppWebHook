﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhatsWebHook.Data;

#nullable disable

namespace WhatsWebHook.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220824115025_initialmysql2")]
    partial class initialmysql2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WhatsWebHook.Models.Messages", b =>
                {
                    b.Property<int>("Idmessage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("full_message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Idmessage");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("WhatsWebHook.Models.Users", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<string>("phone_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("status_id")
                        .HasColumnType("int");

                    b.Property<string>("status_name")
                        .HasColumnType("longtext");

                    b.HasKey("IdUser");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}
