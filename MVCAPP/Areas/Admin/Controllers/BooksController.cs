using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCAPP.Areas.Admin.Models;
using MVCAPP.Domain.Models.Abstractions.Books;
using MVCAPP.Domain.Models.Entities;
using MVCAPP.DTOs;
using MVCAPP.Infrastructure.Abstractions;
using MVCAPP.Models;

namespace MVCAPP.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BooksController : Controller
{
    private readonly IBooksService _booksService;
    private readonly IFileManager _fileManager;

    public BooksController(IBooksService booksService, IFileManager fileManager)
    {
        _booksService = booksService;
        _fileManager = fileManager;
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
    public ActionResult Add()
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
        
        int result = await _booksService.AddAsync(
            request.Title!,
            request.AuthorFullName!,
            request.Genre!,
            path
        );

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
            TempData["danger"] = "Book Wasn't Found";
            return RedirectToAction(nameof(Index));
        }

        return View(book);
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        Book book = await _booksService.GetByIdAsync(id);

        if (book.Id == 0)
        {
            TempData["danger"] = "Book Wasn't Found";
            return RedirectToAction(nameof(Index));
        }

        BookRequest request = new BookRequest(
            book.Id,
            book.Title,
            book.Genre,
            book.AuthorFullName,
            book.CoverImageUrl
        );

        return View(request);
    }

    [HttpPost]
    public async Task<ActionResult> EditAction(BookRequest request)
    {
        Book currentBook = await _booksService.GetByIdAsync(request.Id);

        request.CoverImageUrl = currentBook.CoverImageUrl;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.ToList());
        }

        if (request.ImageFile is not null)
        {
            request.CoverImageUrl = await _fileManager.UploadFile(request.ImageFile);
            await _fileManager.DeleteFile(currentBook.CoverImageUrl!);
        }

        int result = await _booksService.UpdateAsync(
            request.Id,
            request.Title!,
            request.AuthorFullName!,
            request.Genre!,
            request.CoverImageUrl
        );

        if (result == -1)
        {
            TempData["danger"] = "Book Wasn't Changed";
            return RedirectToAction(nameof(Index));
        }

        TempData["success"] = "Book Was Changed";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        Book book = await _booksService.GetByIdAsync(id);

        if (book.Id == 0)
        {
            TempData["danger"] = "Book Wasn't Found";
            return RedirectToAction(nameof(Index));
        }
        
        return View(book);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteAction(int id)
    {
        int result = await _booksService.DeleteAsync(id);

        if (result == -1)
        {
            TempData["danger"] = "Book Wasn't Deleted";
            return RedirectToAction(nameof(Index));
        }
        
        TempData["success"] = "Book Was Deleted";
        return RedirectToAction(nameof(Index));
    }
}
