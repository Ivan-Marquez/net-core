using System.Linq;
using System.Threading.Tasks;
using BooklistApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooklistApp.Controllers {
    [Route ("api/Book")]
    [ApiController]
    public class BookController : Controller {
        private readonly AppDbContext _db;
        public BookController (AppDbContext db) {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll () {
            return Json (new { data = _db.Book.ToList () });
        }
    }
}