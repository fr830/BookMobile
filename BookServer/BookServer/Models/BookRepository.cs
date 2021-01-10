using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookServer.Models
{
    // Integrating ASP.NET Core 2 Web API and Entity Framework Core 2
    // http://www.mithunvp.com/aspnet-core-web-api-entity-framework-core/
    public class BookRepository : IBookRepository
    {
        private BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task Add(Book item)
        {
            await _context.Book.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book> Find(string isbn)
        {
            return await _context.Book.SingleOrDefaultAsync(x => x.ISBN == isbn);
        }

        public async Task Remove(string isbn)
        {
            var itemToRemove = await _context.Book.SingleOrDefaultAsync(x => x.ISBN == isbn);
            if (itemToRemove != null)
            {
                _context.Book.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Book item)
        {
            var itemToUpdate = await _context.Book.SingleOrDefaultAsync(x => x.ISBN == item.ISBN);
            if (itemToUpdate != null)
            {
                itemToUpdate.Title = item.Title;
                itemToUpdate.Genre = item.Genre;
                itemToUpdate.PublishDate = item.PublishDate;
                await _context.SaveChangesAsync();
            }
        }

        public bool CheckValidApiKey(string reqkey)
        {
            var userkeyList = new List<string>
            {
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72d"
            };

            if (userkeyList.Contains(reqkey))
            {
                return true;
            }

            return false;
        }
    }
}
