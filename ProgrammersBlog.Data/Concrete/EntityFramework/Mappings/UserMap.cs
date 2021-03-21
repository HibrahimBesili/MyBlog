using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(20);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30);
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            builder.Property(u => u.CreatedByName).IsRequired();
            builder.Property(u => u.CreatedByName).HasMaxLength(50);
            builder.Property(u => u.ModifiedByName).IsRequired();
            builder.Property(u => u.ModifiedByName).HasMaxLength(50);
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.ModifiedDate).IsRequired();
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.IsDeleted).IsRequired();
            builder.Property(u => u.Note).HasMaxLength(500);
            builder.ToTable("Users");

            builder.HasData(new User
            {
                Id = 1,
                RoleId = 1,
                FirstName = "Alper",
                LastName = "Tunga",
                UserName = "alpertunga",
                Email = "alper@altu.dev",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedDate = DateTime.Now,
                ModifiedByName = "InitialCreate",
                ModifiedDate = DateTime.Now,
                Description = "İlk Admin Kullanıcı",
                Note = "Admin Kullanıcısı",
                Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU",
                PasswordHash = Encoding.ASCII.GetBytes("0192023a7bbd73250516f069df18b500")
            }) ;
            //    var adminUser = new User
            //    {
            //        Id = 1,
            //        UserName = "adminuser",
            //        NormalizedUserName = "ADMINUSER",
            //        Email = "adminuser@gmail.com",
            //        NormalizedEmail = "ADMINUSER@GMAIL.COM",
            //        PhoneNumber = "+905555555555",
            //        Picture = "defaultUser.png",
            //        EmailConfirmed = true,
            //        PhoneNumberConfirmed = true,
            //        SecurityStamp = Guid.NewGuid().ToString()
            //    };
            //    adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");
            //    var editorUser = new User
            //    {
            //        Id = 2,
            //        UserName = "editoruser",
            //        NormalizedUserName = "EDITORUSER",
            //        Email = "editoruser@gmail.com",
            //        NormalizedEmail = "EDITORUSER@GMAIL.COM",
            //        PhoneNumber = "+905555555555",
            //        Picture = "defaultUser.png",
            //        EmailConfirmed = true,
            //        PhoneNumberConfirmed = true,
            //        SecurityStamp = Guid.NewGuid().ToString()
            //    };
            //    editorUser.PasswordHash = CreatePasswordHash(editorUser, "editoruser");

            //    builder.HasData(adminUser, editorUser);
            //}

            //private string CreatePasswordHash(User user, string password)
            //{
            //    var passwordHasher = new PasswordHasher<User>();
            //    return passwordHasher.HashPassword(user, password);
        }
    }
}
