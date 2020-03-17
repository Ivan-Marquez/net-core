using System.Threading.Tasks;
using BooklistApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BooklistApp.Pages.Booklist {
  public class CreateModel : PageModel {
    private readonly AppDbContext _db;
    public CreateModel (AppDbContext db) {
      _db = db;
    }

    [BindProperty]
    public Book Book { get; set; }

    public async Task<IActionResult> OnPost () {
      if (ModelState.IsValid) {
        await _db.Book.AddAsync (Book);
        await _db.SaveChangesAsync ();

        return RedirectToPage ("Index");
      } else {
        return Page ();
      }
    }
  }
}