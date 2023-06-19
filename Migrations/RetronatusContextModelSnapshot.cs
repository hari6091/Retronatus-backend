﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using retronatus_backend.Context;

#nullable disable

namespace retronatus_backend.Migrations
{
    [DbContext(typeof(RetronatusContext))]
    partial class RetronatusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("retronatus_backend.Model.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idcategoria");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("IdCategoria");

                    b.ToTable("categoria");
                });

            modelBuilder.Entity("retronatus_backend.Model.Comentario", b =>
                {
                    b.Property<int>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idcomentario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdComentario"));

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("IdPublicacao")
                        .HasColumnType("integer")
                        .HasColumnName("idpublicacao");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("idusuario");

                    b.Property<int?>("idpublicacao")
                        .HasColumnType("integer");

                    b.Property<int?>("idusuario")
                        .HasColumnType("integer");

                    b.HasKey("IdComentario");

                    b.HasIndex("idpublicacao");

                    b.HasIndex("idusuario");

                    b.ToTable("comentario", t =>
                        {
                            t.Property("idpublicacao")
                                .HasColumnName("idpublicacao1");

                            t.Property("idusuario")
                                .HasColumnName("idusuario1");
                        });
                });

            modelBuilder.Entity("retronatus_backend.Model.Local", b =>
                {
                    b.Property<int>("IdLocal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idlocal");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdLocal"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("IdLocal");

                    b.ToTable("local");
                });

            modelBuilder.Entity("retronatus_backend.Model.Media", b =>
                {
                    b.Property<int>("IdMedia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idmedia");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdMedia"));

                    b.Property<int>("IdPublicacao")
                        .HasColumnType("integer")
                        .HasColumnName("idpublicacao");

                    b.Property<string>("Source")
                        .HasColumnType("text")
                        .HasColumnName("source");

                    b.Property<string>("Type")
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<int?>("idpublicacao")
                        .HasColumnType("integer");

                    b.HasKey("IdMedia");

                    b.HasIndex("idpublicacao");

                    b.ToTable("media", t =>
                        {
                            t.Property("idpublicacao")
                                .HasColumnName("idpublicacao1");
                        });
                });

            modelBuilder.Entity("retronatus_backend.Model.Publicacao", b =>
                {
                    b.Property<int>("IdPublicacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idpublicacao");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPublicacao"));

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("integer")
                        .HasColumnName("idcategoria");

                    b.Property<int>("IdLocal")
                        .HasColumnType("integer")
                        .HasColumnName("idlocal");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("idusuario");

                    b.Property<int?>("LocalIdLocal")
                        .HasColumnType("integer")
                        .HasColumnName("localidlocal");

                    b.Property<string>("Status")
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<int?>("UsuarioIdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("usuarioidusuario");

                    b.Property<int?>("idcategoria")
                        .HasColumnType("integer");

                    b.Property<int?>("idlocal")
                        .HasColumnType("integer");

                    b.Property<int?>("idusuario")
                        .HasColumnType("integer");

                    b.HasKey("IdPublicacao");

                    b.HasIndex("LocalIdLocal");

                    b.HasIndex("UsuarioIdUsuario");

                    b.HasIndex("idcategoria");

                    b.HasIndex("idlocal");

                    b.HasIndex("idusuario");

                    b.ToTable("publicacao", t =>
                        {
                            t.Property("idcategoria")
                                .HasColumnName("idcategoria1");

                            t.Property("idlocal")
                                .HasColumnName("idlocal1");

                            t.Property("idusuario")
                                .HasColumnName("idusuario1");
                        });
                });

            modelBuilder.Entity("retronatus_backend.Model.Resposta", b =>
                {
                    b.Property<int>("IdResposta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idresposta");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdResposta"));

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("IdComentario")
                        .HasColumnType("integer")
                        .HasColumnName("idcomentario");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("integer")
                        .HasColumnName("idusuario");

                    b.Property<int?>("idcomentario")
                        .HasColumnType("integer");

                    b.Property<int?>("idusuario")
                        .HasColumnType("integer");

                    b.HasKey("IdResposta");

                    b.HasIndex("idcomentario");

                    b.HasIndex("idusuario");

                    b.ToTable("resposta", t =>
                        {
                            t.Property("idcomentario")
                                .HasColumnName("idcomentario1");

                            t.Property("idusuario")
                                .HasColumnName("idusuario1");
                        });
                });

            modelBuilder.Entity("retronatus_backend.Model.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idusuario");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("Is_Super_Admin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_super_admin");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("IdUsuario");

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("retronatus_backend.Model.Comentario", b =>
                {
                    b.HasOne("retronatus_backend.Model.Publicacao", null)
                        .WithMany("Comentarios")
                        .HasForeignKey("idpublicacao");

                    b.HasOne("retronatus_backend.Model.Usuario", null)
                        .WithMany()
                        .HasForeignKey("idusuario");
                });

            modelBuilder.Entity("retronatus_backend.Model.Media", b =>
                {
                    b.HasOne("retronatus_backend.Model.Publicacao", null)
                        .WithMany("Medias")
                        .HasForeignKey("idpublicacao");
                });

            modelBuilder.Entity("retronatus_backend.Model.Publicacao", b =>
                {
                    b.HasOne("retronatus_backend.Model.Local", null)
                        .WithMany("Publicacoes")
                        .HasForeignKey("LocalIdLocal");

                    b.HasOne("retronatus_backend.Model.Usuario", null)
                        .WithMany("Publicacoes")
                        .HasForeignKey("UsuarioIdUsuario");

                    b.HasOne("retronatus_backend.Model.Categoria", null)
                        .WithMany("Publicacoes")
                        .HasForeignKey("idcategoria");

                    b.HasOne("retronatus_backend.Model.Local", null)
                        .WithMany()
                        .HasForeignKey("idlocal");

                    b.HasOne("retronatus_backend.Model.Usuario", null)
                        .WithMany()
                        .HasForeignKey("idusuario");
                });

            modelBuilder.Entity("retronatus_backend.Model.Resposta", b =>
                {
                    b.HasOne("retronatus_backend.Model.Comentario", null)
                        .WithMany("Respostas")
                        .HasForeignKey("idcomentario");

                    b.HasOne("retronatus_backend.Model.Usuario", null)
                        .WithMany()
                        .HasForeignKey("idusuario");
                });

            modelBuilder.Entity("retronatus_backend.Model.Categoria", b =>
                {
                    b.Navigation("Publicacoes");
                });

            modelBuilder.Entity("retronatus_backend.Model.Comentario", b =>
                {
                    b.Navigation("Respostas");
                });

            modelBuilder.Entity("retronatus_backend.Model.Local", b =>
                {
                    b.Navigation("Publicacoes");
                });

            modelBuilder.Entity("retronatus_backend.Model.Publicacao", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("Medias");
                });

            modelBuilder.Entity("retronatus_backend.Model.Usuario", b =>
                {
                    b.Navigation("Publicacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
