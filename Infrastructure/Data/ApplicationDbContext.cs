using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Domain.Entities;
using System.Threading;
using Application.Interfaces;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payroll> Payrolles { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Recruitment> Recruitments { get; set; }
        public DbSet<TravelExpense> Expenses { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Organization -> Employee (1..* : 1)
            modelBuilder.Entity<Organization>(o =>
            {
                o.HasKey(x => x.OrganizationId);
                o.Property(x => x.OrganizationName).IsRequired();
                o.HasMany(x => x.Employees)
                 .WithOne(e => e.Organization)
                 .HasForeignKey(e => e.OrganizationId)
                 .OnDelete(DeleteBehavior.Cascade);
                o.HasData(
                    new Organization
                    {
                        OrganizationId = 1,
                        OrganizationName = "Acme Corp",
                        Address = "123 Main St",
                        ContactEmail = "contact@acme.com",
                        ContactPhone = "555-0100",
                        CreatedAt = new DateTime(2024, 1, 1)
                    }
                );
            });

            // Employee -> Attendence, Payroll, Performance, Recruitment, TravelExpence (1..* : 1)
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(x => x.EmployeeId);
                e.Property(x => x.FullName).IsRequired();
                e.Property(x => x.Email).IsRequired();
                e.HasMany(x => x.Attendances)
                 .WithOne(a => a.Employee)
                 .HasForeignKey(a => a.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(x => x.Payrolls)
                 .WithOne(p => p.Employee)
                 .HasForeignKey(p => p.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(x => x.Performances)
                 .WithOne(p => p.Employee)
                 .HasForeignKey(p => p.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(x => x.Recruitments)
                 .WithOne(r => r.Employee)
                 .HasForeignKey(r => r.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasMany(x => x.TravelExpences)
                 .WithOne(t => t.Employee)
                 .HasForeignKey(t => t.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasData(
                    new Employee
                    {
                        EmployeeId = 1,
                        EmployeeCode = "E001",
                        FullName = "John Doe",
                        Email = "john.doe@acme.com",
                        Position = "Developer",
                        HireDate = new DateTime(2020, 1, 15),
                        Salary = 60000m,
                        OrganizationId = 1
                    },
                    new Employee
                    {
                        EmployeeId = 2,
                        EmployeeCode = "E002",
                        FullName = "Jane Smith",
                        Email = "jane.smith@acme.com",
                        Position = "Manager",
                        HireDate = new DateTime(2019, 6, 1),
                        Salary = 80000m,
                        OrganizationId = 1
                    }
                );
            });

            
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(x => x.UserId);
                u.Property(x => x.Email).IsRequired();
                u.HasData(
                    new User
                    {
                        UserId = 1,
                        FullName = "Admin User",
                        Email = "admin@gmail.com",
                        PasswordHash = "admin",
                        Role = "Admin",
                        IsActive = true,
                        CreatedAt = new DateTime(2024, 1, 1)
                    }
                );
            });

            // Attendence
            modelBuilder.Entity<Attendence>(a =>
            {
                a.HasKey(x => x.AttendanceId);
                a.HasData(
                    new Attendence
                    {
                        AttendanceId = 1,
                        EmployeeId = 1,
                        Date = new DateTime(2024, 12, 01),
                        CheckIn = new TimeSpan(9, 0, 0),
                        CheckOut = new TimeSpan(17, 0, 0),
                        IsPresent = true
                    }
                );
            });

            // Payroll
            modelBuilder.Entity<Payroll>(p =>
            {
                p.HasKey(x => x.PayrollId);
                p.HasData(
                    new Payroll
                    {
                        PayrollId = 1,
                        EmployeeId = 1,
                        BasicSalary = 5000m,
                        Allowance = 500m,
                        Deduction = 200m,
                        NetSalary = 5300m,
                        SalaryMonth = new DateTime(2024, 12, 1)
                    }
                );
            });

            // Performance
            modelBuilder.Entity<Performance>(p =>
            {
                p.HasKey(x => x.PerformanceId);
                p.HasData(
                    new Performance
                    {
                        PerformanceId = 1,
                        EmployeeId = 1,
                        Rating = 4,
                        Remarks = "Consistent performer",
                        ReviewDate = new DateTime(2024, 11, 1)
                    }
                );
            });

            // Recruitment (note: class uses 'PerformanceId' as PK name)
            modelBuilder.Entity<Recruitment>(r =>
            {
                r.HasKey(x => x.RecruitmentId);
                r.HasData(
                    new Recruitment
                    {
                        RecruitmentId = 1,
                        EmployeeId = 2,
                        Rating = 3,
                        Remarks = "Hired for leadership",
                        ReviewDate = new DateTime(2024, 10, 1)
                    }
                );
            });

            // TravelExpence
            modelBuilder.Entity<TravelExpense>(t =>
            {
                t.HasKey(x => x.TravelExpenceId);
                t.HasData(
                    new TravelExpense
                    {
                        TravelExpenceId = 1,
                        EmployeeId = 1,
                        Purpose = "Client meeting",
                        Amount = 150.00m,
                        TravelDate = new DateTime(2024, 10, 10),
                        Status = "Approved"
                    }
                );
            });

            // RefreshToken
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);
                entity.HasIndex(e => e.Token).IsUnique();
                entity.HasOne(e => e.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

