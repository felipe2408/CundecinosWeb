﻿// <auto-generated />
using System;
using CundecinosWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CundecinosWeb.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230314013457_FixModelPersonn")]
    partial class FixModelPersonn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CundecinosWeb.Models.CollegeCareer", b =>
                {
                    b.Property<Guid>("CollegeCareerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CollegeCareerId");

                    b.HasIndex("PersonID");

                    b.ToTable("CollegeCareer");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Extension", b =>
                {
                    b.Property<Guid>("ExtensionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExtensionId");

                    b.HasIndex("PersonID");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short?>("BirthYear")
                        .HasColumnType("smallint");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("CollegeCareerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("ExtensionId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("UID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("CundecinosWeb.Models.CollegeCareer", b =>
                {
                    b.HasOne("CundecinosWeb.Models.Person", "Person")
                        .WithMany("CollegeCareer")
                        .HasForeignKey("PersonID");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Extension", b =>
                {
                    b.HasOne("CundecinosWeb.Models.Person", "Person")
                        .WithMany("Extension")
                        .HasForeignKey("PersonID");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Person", b =>
                {
                    b.Navigation("CollegeCareer");

                    b.Navigation("Extension");
                });
#pragma warning restore 612, 618
        }
    }
}
