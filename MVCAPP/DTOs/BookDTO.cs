using MVCAPP.Domain.Models.Entities;
using MVCAPP.Models;

namespace MVCAPP.DTOs;

public class BookDTO
{
    public List<Book> Books { get; set; }

    public PageInfo PageInfo { get; set; }

    public BookDTO()
    {
        
    }
}
