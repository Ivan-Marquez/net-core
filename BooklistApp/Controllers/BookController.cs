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

        [HttpDelete]
        public async Task<IActionResult> Delete (int id) {
            var book = await _db.Book.FirstOrDefaultAsync (u => u.Id == id);

            if (book == null) {
                return Json (new {
                    success = false, message = "Error while deleting"
                });
            }

            _db.Book.Remove (book);
            await _db.SaveChangesAsync ();

            return Json (new {
                success = true, message = "Delete successful"
            });
        }
    }
}