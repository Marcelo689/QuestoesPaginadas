﻿// <auto-generated />
using System;
using Banco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BancoProject.Migrations
{
    [DbContext(typeof(DBClass))]
    [Migration("20231219120955_003")]
    partial class _003
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DTO.BancoClasses.Login.Entidades.EstudanteFolder.Estudante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Estudante");
                });

            modelBuilder.Entity("DTO.BancoClasses.Login.Entidades.ProfessorFolder.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("DTO.BancoClasses.Login.Entidades.ProfessorFolder.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("DTO.BancoClasses.Login.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAccountDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("DTO.BancoClasses.ProvaFolder.Prova", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Prova");
                });

            modelBuilder.Entity("DTO.BancoClasses.ProvaFolder.Questao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoOpcao1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoOpcao2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoOpcao3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoOpcao4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescricaoOpcao5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OpcaoCorretaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProvaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpcaoCorretaId");

                    b.HasIndex("ProvaId");

                    b.ToTable("Questao");
                });

            modelBuilder.Entity("DTO.BancoClasses.ProvaFolder.QuestaoOpcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Opcao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuestaoOpcao");
                });

            modelBuilder.Entity("DTO.BancoClasses.Login.Entidades.EstudanteFolder.Estudante", b =>
                {
                    b.HasOne("DTO.BancoClasses.Login.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DTO.BancoClasses.Login.Entidades.ProfessorFolder.Professor", b =>
                {
                    b.HasOne("DTO.BancoClasses.Login.Entidades.ProfessorFolder.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("DTO.BancoClasses.Login.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Area");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DTO.BancoClasses.ProvaFolder.Prova", b =>
                {
                    b.HasOne("DTO.BancoClasses.Login.Entidades.ProfessorFolder.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("DTO.BancoClasses.ProvaFolder.Questao", b =>
                {
                    b.HasOne("DTO.BancoClasses.ProvaFolder.QuestaoOpcao", "OpcaoCorreta")
                        .WithMany()
                        .HasForeignKey("OpcaoCorretaId");

                    b.HasOne("DTO.BancoClasses.ProvaFolder.Prova", "Prova")
                        .WithMany()
                        .HasForeignKey("ProvaId");

                    b.Navigation("OpcaoCorreta");

                    b.Navigation("Prova");
                });
#pragma warning restore 612, 618
        }
    }
}