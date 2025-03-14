using Microsoft.AspNetCore.Mvc;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;

namespace MVCAPP.Controllers;

public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        List<Book> books = await _booksService.GetAllAsync();

        return View(books);
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        Book book = await _booksService.GetByIdAsync(id);

        if (book.Id == 0)
        {
            TempData["danger"] = "Book Not Found";
            return RedirectToAction(nameof(Index));
        }

        // if (bookWriter.Id == 0)
        // {
        //     TempData["danger"] = "Author Not Found";
        //     return RedirectToAction(nameof(Index));
        // }

        BookDTO dto = new BookDTO
        {
            Title = book.Title,
            CoverImageUrl = book.CoverImageUrl,
            WriterName = book.AuthorFullName,
            Genre = book.Genre,
            Id = book.Id,
        };

        return View(dto);
    }
}
