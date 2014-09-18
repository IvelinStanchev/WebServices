namespace Music.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Music.Models;
    using Music.Data;

    class EntryPoint
    {
        private static readonly HttpClient Client = new HttpClient { BaseAddress = new Uri("http://localhost:53555/") };

        internal static void Main()
        {
            MusicDbContext context = new MusicDbContext();
            context.Albums.Any();

            // Add an Accept header for JSON format.
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Your server must be started! If there is an error check the Uri!");
            Console.WriteLine();

            //AddNewAlbum("Testing the client");
            //UpdateAlbum(1, new Album
            //{
            //    Id = 1,
            //    Title = "Test updating through client"
            //});
            //DeleteAlbum(3);

            PrintAllAlbums();
        }

        internal static void PrintAllAlbums()
        {
            HttpResponseMessage response = Client.GetAsync("api/Albums/All").Result; // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<Album>>().Result;
                foreach (var album in albums)
                {
                    Console.WriteLine("Album title: {0}", album.Title);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        internal static void AddNewAlbum(string title)
        {
            var album = new Album 
            { 
                Title = title
            };

            var response = Client.PostAsJsonAsync("api/Albums/Create", album).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        internal static void UpdateAlbum(int id, Album album)
        {
            var newAlbum = new Album
            {
                Id = id,
                Title = album.Title
            };

            var response = Client.PutAsJsonAsync("api/Albums/Update/" + id, newAlbum).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album updated!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        internal static void DeleteAlbum(int id)
        {
            var response = Client.DeleteAsync("api/Albums/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Album deleted!");
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
