﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using lan_side_project.Data;

#nullable disable

namespace lan_side_project.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241216124226_AddUserFields")]
    partial class AddUserFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("integer")
                        .HasColumnName("permissions_id");

                    b.Property<int>("RolesId")
                        .HasColumnType("integer")
                        .HasColumnName("roles_id");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("permission_role");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer")
                        .HasColumnName("roles_id");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer")
                        .HasColumnName("users_id");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("role_user");
                });

            modelBuilder.Entity("lan_side_project.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.ToTable("permissions");
                });

            modelBuilder.Entity("lan_side_project.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("lan_side_project.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login_date");

                    b.Property<DateTime?>("LastPasswordChangedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_password_changed_date");

                    b.Property<DateTime?>("LastPasswordResetDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_password_reset_date");

                    b.Property<int>("LoginFailedAttempts")
                        .HasColumnType("integer")
                        .HasColumnName("login_failed_attempts");

                    b.Property<DateTime?>("LoginLockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("login_lockout_end");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<int>("ResetPasswordFailedAttempts")
                        .HasColumnType("integer")
                        .HasColumnName("reset_password_failed_attempts");

                    b.Property<DateTime?>("ResetPasswordLockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reset_password_lockout_end");

                    b.Property<string>("ResetPasswordToken")
                        .HasColumnType("text")
                        .HasColumnName("reset_password_token");

                    b.Property<DateTime?>("ResetPasswordTokenExpiration")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("reset_password_token_expiration");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updated_by");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("lan_side_project.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lan_side_project.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("lan_side_project.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("lan_side_project.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
