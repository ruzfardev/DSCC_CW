using DSCC_API.Models;
using DSCC_API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DSCC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepo;
        public BookController(IRepository<Book> recipesRepository)
        {
            _bookRepo = recipesRepository;
        }
        // GET: api/<BookController>
        [HttpGet]
        public List<Book> Get()
        {
            var books = _bookRepo.GetAll().ToList();
            return books;
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var book = _bookRepo.GetById(id);
            return book;
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            var newBook = new Book();
            using (var scope = new TransactionScope())
            {
                newBook = new Book()
                {
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    ISBN = book.ISBN,
                    PublishedYear = book.PublishedYear,
                    Publisher = book.Publisher,
                };
                _bookRepo.Add(book);
                scope.Complete();
            }
            newBook = _bookRepo.GetAll().Where(book => book.ISBN == newBook.ISBN).FirstOrDefault();
            //return CreatedAtAction(nameof(Get), newBook);
            return Ok(newBook);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            if (book != null)
            {
                using (var scope = new TransactionScope())
                {
                    _bookRepo.Update(book);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepo.Delete(id);
            return new OkResult();
        }
    }
}
