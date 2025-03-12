using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCAPP.Areas.Admin.Models;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Abstractions.Writers;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Infrastructure.Abstractions;

namespace MVCAPP.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BooksController : Controller
{
    private readonly IBooksService _booksService;
    private readonly IWritersService _writersService;
    private readonly IFileManager _fileManager;

    public BooksController(IBooksService booksService, IWritersService writersService, IFileManager fileManager)
    {
        _booksService = booksService;
        _writersService = writersService;
        _fileManager = fileManager;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return View(await _booksService.GetAllAsync());
    }

    [HttpGet]
    public async Task<ActionResult> Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> AddAction(BookRequest request)
    {
        string path = string.Empty;

        if (request.ImageFile is not null)
        {
            path = await _fileManager.UploadFile(request.ImageFile);
        }

        //
        int result = await _booksService.AddAsync(1, request.Title, request.Genre, path);

        if (result == 0)
        {
            TempData["danger"] = "Books Wasn't Added";
        }

        TempData["success"] = "Books Was Added";
        return RedirectToAction(nameof(Index));
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

        Writer bookWriter = await _writersService.GetByIdAsync(book.WriterId);

        // if (bookWriter.Id == 0)
        // {
        //     TempData["danger"] = "Author Not Found";
        //     return RedirectToAction(nameof(Index));
        // }

        BookDTO dto  = new BookDTO
        {
            Title = book.Title,
            Writer = bookWriter.Name + " " +bookWriter.Surname,
            CoverImageUrl = book.CoverImageUrl,
            WriterId = bookWriter.Id,
            Genre = book.Genre,
            Id = book.Id,
        };

        return View(dto);
    }

    // [HttpGet]
    // public async Task<ActionResult> Edit(int id)
    // {
    //     Book book = await _booksService.GetByIdAsync(id);
    //
    //     if (book.Id == 0)
    //     {
    //         TempData["danger"] = "Book Not Found";
    //         return RedirectToAction(nameof(Index));
    //     }
    //
    //     return View(book);
    // }

    // [HttpPost]
    // public async Task<ActionResult> EditAction(int id, string title, string genre, string coverImageUrl)
    // {
    //     // Book book = await _booksService.GetByIdAsync(id);
    //     //
    //     // if (book.Id == 0)
    //     // {
    //     //     TempData["danger"] = "Book Not Found";
    //     //     return RedirectToAction(nameof(Index));
    //     // }
    //     //
    //     // return View(book);
    //
    //     return RedirectToAction(nameof(Index));
    // }
}
