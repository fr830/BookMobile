using BookClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookClient.Models
{
    public class BookManager
    {
        private string authKey;  // authorization key
        const string url = "http://xam150.azurewebsites.net/api/books/";

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            if (string.IsNullOrEmpty(authKey))
            {
                authKey = await client.GetStringAsync(App.Settings.SiteUrl + "login");
                // The key will have quotes around it that need to be removed.
                authKey = JsonConvert.DeserializeObject<string>(authKey);
            }

            client.DefaultRequestHeaders.Add("Authorization", authKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            // TODO: use GET to retrieve books
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IEnumerable<Book>>(result);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
            // TODO: use POST to add a book
            Book book = new Book()
            {
                ISBN = string.Empty,
                Title = title,
                Authors = new ObservableCollection<string>(new[] { author }),
                Genre = genre,
                PublishDate = DateTime.Now
            };

            HttpClient client = await GetClient();
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

            book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return book;
        }

        public async Task Update(Book book)
        {
            // TODO: use PUT to update a book
            HttpClient client = await GetClient();
            string json = JsonConvert.SerializeObject(book);
            await client.PutAsync(url + "/" + book.ISBN, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
        }

        public async Task Delete(string isbn)
        {
            // TODO: use DELETE to delete a book
            HttpClient client = await GetClient();
            await client.DeleteAsync(url + "/" + isbn);
        }
    }
}

