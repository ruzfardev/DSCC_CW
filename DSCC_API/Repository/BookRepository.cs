using DSCC_API.DAL;
using DSCC_API.Models;

namespace DSCC_API.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private readonly AppDbContext ctx;

        public BookRepository(AppDbContext context)
        {
            ctx = context;
        }
        public void Add(Book entity)
        {
            ctx.Books.Add(entity);
            Save();
        }

        public void Delete(int id)
        {
            ctx.Books.Remove((Book)this.GetById(id));
            Save();
        }

        public IEnumerable<Book> GetAll()
        {
            return ctx.Books.ToList();
        }

        public object GetById(int id)
        {
            var book = ctx.Books.Find(id);

            if (book is null)
                return "Book is not found";

            return book;
        }

        public void Update(Book entity)
        {
            ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
        public void Save()
        {
            ctx.SaveChanges();
        }
    }
}
