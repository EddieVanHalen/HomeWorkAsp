using Microsoft.AspNetCore.Mvc;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Abstractions.Writers;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;

namespace MVCAPP.Controllers;

public class BooksController : Controller
{
    private readonly IBooksService _booksService;
    private readonly IWritersService _writersService;

    public BooksController(IBooksService booksService, IWritersService writersService)
    {
        _booksService = booksService;
        _writersService = writersService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return View(await _booksService.GetAllAsync());
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
        
        BookDTO dto  = new BookDTO
        {
            Title = book.Title,
            CoverImageUrl = book.CoverImageUrl,
            WriterName = book.AuthorFullName,
            Genre = book.Genre,
            Id = book.Id,
        };
        
        return View(dto); 
    }


    
    [HttpGet]
    public async Task<ActionResult> EditAction(int id, string title, string writer, string genre, string coverImageUrl)
    {
        // Book book = await _booksService.GetByIdAsync(id);
        //
        // if (book.Id == 0)
        // {
        //     TempData["danger"] = "Book Not Found";
        //     return RedirectToAction(nameof(Index));
        // }
        //
        // return View(book);
        
        return RedirectToAction(nameof(Index));
    }
}