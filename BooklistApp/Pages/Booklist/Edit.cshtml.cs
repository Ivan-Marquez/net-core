using System.Threading.Tasks;
using BooklistApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooklistApp.Pages.Booklist {
  public class EditModel : PageModel {
    private AppDbContext _db;
    public EditModel (AppDbContext db) {
      _db = db;
    }

    [BindProperty]
    public Book Book { get; set; }
    public async Task OnGet (int id) {
      Book = await _db.Book.FindAsync (id);
    }

    public async Task<IActionResult> OnPost () {
      if (ModelState.IsValid) {
        var bookFromDb = await _db.Book.FindAsync (Book.Id);
        bookFromDb.Name = Book.Name;
        bookFromDb.Author = Book.Author;
        bookFromDb.ISBN = Book.ISBN;

        await _db.SaveChangesAsync ();

        return RedirectToPage ("Index");
      } else {
        return RedirectToPage ();
      }
    }
  }
}