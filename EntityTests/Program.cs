using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EntityTests
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Blog blog = new Blog();
            blog.BlogId = 1;
            blog.Url = "www.test.net";

            RssBlog rssBlog = new RssBlog();
            rssBlog.BlogId = 2;
            rssBlog.Url = "wwww.test2.net";
            rssBlog.RssUrl = "www.testistest.net";

            using (var ctx = new MyContext())
            {
                ctx.Blogs.Add(blog);
                ctx.RssBlogs.Add(rssBlog);
                ctx.SaveChanges();
            }


            Console.WriteLine("Check your database.");
            Console.ReadKey();
        }
    }

    internal class MyContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<RssBlog> RssBlogs { get; set; }


        public MyContext() : base(nameOrConnectionString: "EFTestDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MyContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<Blog>().ToTable("Blogs");
                modelBuilder.Entity<RssBlog>().ToTable("RssBlogs");
            }

        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class RssBlog : Blog
    {
        public string RssUrl { get; set; }
    }






}
