using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.DataAccess.Data
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<ExamResults> ExamResults { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Exams> Exams { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Groups> Groups { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<QnAs> QnAs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Students> Students { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuiulder)
        {
            modelBuiulder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
            });
            modelBuiulder.Entity<Students>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
                entity.Property(e => e.contact).HasMaxLength(50);
                entity.Property(e => e.CVFileName).HasMaxLength(250);
                entity.Property(e => e.PictureFileName).HasMaxLength(250);
                entity.HasOne(d => d.Groups).WithMany(p => p.Students).HasForeignKey(d => d.GroupsId);
            });
            modelBuiulder.Entity<QnAs>(entity =>
            {
                entity.Property(e => e.Question).IsRequired();
                entity.Property(e => e.Option1).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option2).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option3).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Option4).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Answer).IsRequired();
                entity.HasOne(d => d.Exams).WithMany(p => p.QnAs).HasForeignKey(d => d.ExamsId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuiulder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(250);
                entity.HasOne(d => d.users).WithMany(p => p.Groups).HasForeignKey(d => d.UsersId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuiulder.Entity<Exams>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(250);  
                
                entity.HasOne(d => d.Groups).WithMany(p => p.Exams).HasForeignKey(d => d.GroupsId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuiulder.Entity<ExamResults>(entity =>
            {
                entity.HasOne(d => d.Exams).WithMany(p => p.ExamResults).HasForeignKey(d => d.ExamId);
                entity.HasOne(d => d.QnAs).WithMany(p => p.ExamResults).HasForeignKey(d => d.QnAsId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Students).WithMany(p => p.ExamResults).HasForeignKey(d => d.StudentId).OnDelete(DeleteBehavior.ClientSetNull);
            });




            base.OnModelCreating(modelBuiulder);
            
        }
    }
}

