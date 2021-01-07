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
        const string url = "https://bookserver.cogentcoder.com/api/books/";

        private async Task<HttpClient> GetClientAsync()
        {
            HttpClient client = new HttpClient();
            //if (string.IsNullOrEmpty(authKey))
            //{
            //    authKey = await client.GetStringAsync(App.Settings.SiteUrl + "login");
            //    // The key will have quotes around it that need to be removed.
            //    authKey = JsonConvert.DeserializeObject<string>(authKey);
            //}

            authKey = App.Settings.AuthKey;
            client.DefaultRequestHeaders.Add("Authorization", authKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            //if (string.IsNullOrEmpty(authKey))
            //{
            //    authKey = client.GetStringAsync(App.Settings.SiteUrl + "login").Result;
            //    // The key will have quotes around it that need to be removed.
            //    authKey = JsonConvert.DeserializeObject<string>(authKey);
            //}
            authKey = App.Settings.AuthKey;
            client.DefaultRequestHeaders.Add("Authorization", authKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<IList<Book>> GetAll()
        {
            // TODO: use GET to retrieve books
            HttpClient client = await GetClientAsync();
            string result = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<IList<Book>>(result);
        }

        public IList<Book> GetBooks()
        {
            // TODO: use GET to retrieve books
            HttpClient client = GetClient();
            string result = client.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IList<Book>>(result);
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

            HttpClient client = await GetClientAsync();
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));

            book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return book;
        }

        public async Task Update(Book book)
        {
            // TODO: use PUT to update a book
            HttpClient client = await GetClientAsync();
            string json = JsonConvert.SerializeObject(book);
            await client.PutAsync(url + "/" + book.ISBN, new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json"));
        }

        public async Task Delete(string isbn)
        {
            // TODO: use DELETE to delete a book
            HttpClient client = await GetClientAsync();
            await client.DeleteAsync(url + "/" + isbn);
        }
    }
}

