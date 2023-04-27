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
    [Migration("20230427190839_AddPublicationType")]
    partial class AddPublicationType
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

                    b.HasKey("CollegeCareerId");

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

                    b.HasKey("ExtensionId");

                    b.ToTable("Extensions");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("varchar(100)");

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

                    b.HasIndex("CollegeCareerId");

                    b.HasIndex("ExtensionId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Publication", b =>
                {
                    b.Property<Guid>("PublicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EstimatedPrice")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PublicationType")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("PublicationID");

                    b.HasIndex("PersonID");

                    b.ToTable("Publication");
                });

            modelBuilder.Entity("CundecinosWeb.Models.PublicationAttachment", b =>
                {
                    b.Property<Guid>("PublicationAttachmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ImageScreen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageThumbNail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PublicationID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PublicationAttachmentID");

                    b.HasIndex("PublicationID");

                    b.ToTable("PublicationAttachments");
                });

            modelBuilder.Entity("CundecinosWeb.Models.PublicationComments", b =>
                {
                    b.Property<Guid>("PublicationCommentsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EstimatedPrice")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PersonID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProductUrl")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("PublicationID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PublicationCommentsID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PublicationID");

                    b.ToTable("PublicationComments");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Person", b =>
                {
                    b.HasOne("CundecinosWeb.Models.CollegeCareer", "CollegeCareer")
                        .WithMany("Persons")
                        .HasForeignKey("CollegeCareerId");

                    b.HasOne("CundecinosWeb.Models.Extension", "Extension")
                        .WithMany("Persons")
                        .HasForeignKey("ExtensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CollegeCareer");

                    b.Navigation("Extension");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Publication", b =>
                {
                    b.HasOne("CundecinosWeb.Models.Person", "Person")
                        .WithMany("Publication")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("CundecinosWeb.Models.PublicationAttachment", b =>
                {
                    b.HasOne("CundecinosWeb.Models.Publication", "Publication")
                        .WithMany("PublicationAttachment")
                        .HasForeignKey("PublicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("CundecinosWeb.Models.PublicationComments", b =>
                {
                    b.HasOne("CundecinosWeb.Models.Person", "Person")
                        .WithMany("PublicationComments")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CundecinosWeb.Models.Publication", "Publication")
                        .WithMany("PublicationComments")
                        .HasForeignKey("PublicationID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Publication");
                });

            modelBuilder.Entity("CundecinosWeb.Models.CollegeCareer", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Extension", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Person", b =>
                {
                    b.Navigation("Publication");

                    b.Navigation("PublicationComments");
                });

            modelBuilder.Entity("CundecinosWeb.Models.Publication", b =>
                {
                    b.Navigation("PublicationAttachment");

                    b.Navigation("PublicationComments");
                });
#pragma warning restore 612, 618
        }
    }
}
