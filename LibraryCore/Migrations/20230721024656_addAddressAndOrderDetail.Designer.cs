﻿// <auto-generated />
using System;
using LibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryCore.Migrations
{
    [DbContext(typeof(LibraryDBContext))]
    [Migration("20230721024656_addAddressAndOrderDetail")]
    partial class addAddressAndOrderDetail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibraryCore.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("appointment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("AppointmentId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("BriefInfomaton")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("brief_infomaton");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("fullname");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Infomation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("infomation");

                    b.HasKey("AuthorId");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("blog_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("author");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("detail");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("BlogId");

                    b.HasIndex("UserId");

                    b.ToTable("Blog", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("Introduction")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("PublicationDate")
                        .HasColumnType("date")
                        .HasColumnName("publication_date");

                    b.Property<string>("Publisher")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("publisher");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<double?>("Rate")
                        .HasColumnType("float")
                        .HasColumnName("rate");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.BookReview", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<string>("Comment")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("comment");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<int?>("Rate")
                        .HasColumnType("int")
                        .HasColumnName("rate");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("Book_Review", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("LibraryCore.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("category_name");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Fine", b =>
                {
                    b.Property<int>("FineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("fine_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FineId"), 1L, 1);

                    b.Property<double>("FineAmount")
                        .HasColumnType("float")
                        .HasColumnName("fine_amount");

                    b.Property<DateTime>("FineDate")
                        .HasColumnType("date")
                        .HasColumnName("fine_date");

                    b.Property<int>("LoanId")
                        .HasColumnType("int")
                        .HasColumnName("loan_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("FineId");

                    b.HasIndex("LoanId");

                    b.HasIndex("UserId");

                    b.ToTable("Fine", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("loan_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("LoanDateActual")
                        .HasColumnType("date")
                        .HasColumnName("loan_date_actual");

                    b.Property<DateTime>("LoanDateFrom")
                        .HasColumnType("date")
                        .HasColumnName("loan_date_from");

                    b.Property<DateTime>("LoanDateTor")
                        .HasColumnType("date")
                        .HasColumnName("loan_date_tor");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("LoanId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Loan", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LibraryCore.Models.OrderDetail", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("LibraryCore.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reservation_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("ReservationId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservation", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("fullname");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("mail");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("password");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("LibraryCore.Models.Appointment", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany("Appointments")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK_Appointment_Book");

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Appointment_User");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Blog", b =>
                {
                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Blog_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Book", b =>
                {
                    b.HasOne("LibraryCore.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .IsRequired()
                        .HasConstraintName("FK_Book_Author");

                    b.HasOne("LibraryCore.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Book_Category");

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LibraryCore.Models.BookReview", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany("BookReviews")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK_Book_Review_Book");

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("BookReviews")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Book_Review_User");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Cart", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Fine", b =>
                {
                    b.HasOne("LibraryCore.Models.Loan", "Loan")
                        .WithMany("Fines")
                        .HasForeignKey("LoanId")
                        .IsRequired()
                        .HasConstraintName("FK_Fine_Loan");

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("Fines")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Fine_User");

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Loan", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK_Loan_Book");

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Loan_User");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Order", b =>
                {
                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.OrderDetail", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany("OrderDetails")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryCore.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("LibraryCore.Models.Reservation", b =>
                {
                    b.HasOne("LibraryCore.Models.Book", "Book")
                        .WithMany("Reservations")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservation_Book");

                    b.HasOne("LibraryCore.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Reservation_User");

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryCore.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryCore.Models.Book", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("BookReviews");

                    b.Navigation("Loans");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("LibraryCore.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("LibraryCore.Models.Loan", b =>
                {
                    b.Navigation("Fines");
                });

            modelBuilder.Entity("LibraryCore.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("LibraryCore.Models.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Blogs");

                    b.Navigation("BookReviews");

                    b.Navigation("Fines");

                    b.Navigation("Loans");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
