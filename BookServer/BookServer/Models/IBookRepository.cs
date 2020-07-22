using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookServer.Models
{
    public interface IBookRepository
    {
        Task Add(Book item);
        Task<IEnumerable<Book>> GetAll();
        Task<Book> Find(string key);
        Task Remove(string isbn);
        Task Update(Book item);
        bool CheckValidApiKey(string reqkey);
    }
}
