﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SGBI.SGBI.API.Data;

#nullable disable

namespace SGBI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240522043208_aa")]
    partial class aa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.AseoInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadTrimestre")
                        .HasColumnType("integer");

                    b.Property<bool>("EstaSiendoIncluido")
                        .HasColumnType("boolean");

                    b.Property<double>("MontoTotalAnoActual")
                        .HasColumnType("double precision");

                    b.Property<double>("MontoTotalAnosAnteriores")
                        .HasColumnType("double precision");

                    b.Property<int>("TramiteInformacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TramiteInformacionId");

                    b.ToTable("AseosInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.BasuraInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadTrimestre")
                        .HasColumnType("integer");

                    b.Property<bool>("EstaSiendoIncluido")
                        .HasColumnType("boolean");

                    b.Property<double>("MontoAnoActual")
                        .HasColumnType("double precision");

                    b.Property<double>("MontoAnoAnteriores")
                        .HasColumnType("double precision");

                    b.Property<double>("Tarifa")
                        .HasColumnType("double precision");

                    b.Property<int>("TramiteInformacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TramiteInformacionId");

                    b.ToTable("BasuraInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.BienesInmueblesInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadTrimestre")
                        .HasColumnType("integer");

                    b.Property<bool>("EstaSiendoIncluido")
                        .HasColumnType("boolean");

                    b.Property<double>("MontoTotalAnoActual")
                        .HasColumnType("double precision");

                    b.Property<double>("MontoTotalAnosAnteriores")
                        .HasColumnType("double precision");

                    b.Property<int>("TramiteInformacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TramiteInformacionId");

                    b.ToTable("BienesInmueblesInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.ExoneracionInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CantidadTrimestre")
                        .HasColumnType("integer");

                    b.Property<double?>("Excedente")
                        .HasColumnType("double precision");

                    b.Property<double?>("MontoExonerar")
                        .HasColumnType("double precision");

                    b.Property<double?>("MontoExonerarAnoActual")
                        .HasColumnType("double precision");

                    b.Property<double?>("MontoExonerarAnoAnteriores")
                        .HasColumnType("double precision");

                    b.Property<int>("TramiteInformacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TramiteInformacionId");

                    b.ToTable("ExoneracionesInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.ParqueInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadTrimestre")
                        .HasColumnType("integer");

                    b.Property<bool>("EstaSiendoIncluido")
                        .HasColumnType("boolean");

                    b.Property<double>("MontoTotalAnoActual")
                        .HasColumnType("double precision");

                    b.Property<double>("MontoTotalAnosAnteriores")
                        .HasColumnType("double precision");

                    b.Property<int>("TramiteInformacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TramiteInformacionId");

                    b.ToTable("ParquesInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.Tarifa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("MontoColones")
                        .HasColumnType("double precision");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tarifas");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.Tramite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("text");

                    b.Property<bool>("esTramitePorDefecto")
                        .HasColumnType("boolean");

                    b.Property<bool>("estaActivo")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Tramites");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.TramiteCampo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("CodigoDepartamento")
                        .HasColumnType("boolean");

                    b.Property<bool>("Descripcion")
                        .HasColumnType("boolean");

                    b.Property<bool>("DuenoActual")
                        .HasColumnType("boolean");

                    b.Property<bool>("DuenoAnterior")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("FincaMadre")
                        .HasColumnType("boolean");

                    b.Property<bool>("FolioReal")
                        .HasColumnType("boolean");

                    b.Property<bool>("ImponibleActual")
                        .HasColumnType("boolean");

                    b.Property<bool>("ImponibleAnterior")
                        .HasColumnType("boolean");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TramiteCampos");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.TramiteInformacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoDepartamento")
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<string>("DuenoActual")
                        .HasColumnType("text");

                    b.Property<string>("DuenoAnterior")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FincaMadre")
                        .HasColumnType("text");

                    b.Property<List<string>>("FolioReal")
                        .HasColumnType("text[]");

                    b.Property<double?>("ImponibleActual")
                        .HasColumnType("double precision");

                    b.Property<double?>("ImponibleAnterior")
                        .HasColumnType("double precision");

                    b.Property<string>("NumeroDocumento")
                        .HasColumnType("text");

                    b.Property<int>("TramiteId")
                        .HasColumnType("integer");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TramiteId");

                    b.ToTable("TramitesInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CambiarContrasenaFechaExpiracion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CambiarContrasenaToken")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenFechaExpiracion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("SegundoApellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SGBI.SGBI.Core.Entities.CodigoDepartamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UsuarioCreacion")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioModificacion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CodigoDepartamentos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SGBI.SBGI.Core.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.AseoInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.TramiteInformacion", "TramiteInformacion")
                        .WithMany()
                        .HasForeignKey("TramiteInformacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TramiteInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.BasuraInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.TramiteInformacion", "TramiteInformacion")
                        .WithMany()
                        .HasForeignKey("TramiteInformacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TramiteInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.BienesInmueblesInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.TramiteInformacion", "TramiteInformacion")
                        .WithMany()
                        .HasForeignKey("TramiteInformacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TramiteInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.ExoneracionInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.TramiteInformacion", "TramiteInformacion")
                        .WithMany()
                        .HasForeignKey("TramiteInformacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TramiteInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.ParqueInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.TramiteInformacion", "TramiteInformacion")
                        .WithMany()
                        .HasForeignKey("TramiteInformacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TramiteInformacion");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.TramiteCampo", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.Tramite", "Tramite")
                        .WithOne("TramiteCampo")
                        .HasForeignKey("SGBI.SBGI.Core.Entities.TramiteCampo", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tramite");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.TramiteInformacion", b =>
                {
                    b.HasOne("SGBI.SBGI.Core.Entities.Tramite", "Tramite")
                        .WithMany("TramiteInformacion")
                        .HasForeignKey("TramiteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tramite");
                });

            modelBuilder.Entity("SGBI.SBGI.Core.Entities.Tramite", b =>
                {
                    b.Navigation("TramiteCampo");

                    b.Navigation("TramiteInformacion");
                });
#pragma warning restore 612, 618
        }
    }
}
