using Microsoft.AspNetCore.Mvc;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Models;

namespace MVCAPP.Controllers;

public class BooksController : Controller
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    public async Task<ActionResult> Index(string? filter, int itemsPerPage = 6, int page = 1)
    {
        (List<Book> books, int totalItems) = await _booksService.GetAllAsync(filter, itemsPerPage, page);
    
        BookDTO dto = new BookDTO()
        {
            Books = books,
            PageInfo = new PageInfo()
            {
                Query = filter,
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage)
            }
        };
        
        return View(dto);
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
        
        return View(book);
    }
}
