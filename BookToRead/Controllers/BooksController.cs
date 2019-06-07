using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookToRead.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BookToRead.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            List<BookViewModel> list = new List<BookViewModel>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("aplication/json"));
            HttpResponseMessage responseMessage = client.GetAsync("books").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                list = responseMessage.Content.ReadAsAsync<List<BookViewModel>>().Result;

            }
            return View(list);
        }
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookViewModel model)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");

            client.PostAsJsonAsync<BookViewModel>("books", model);
                //.ContinueWith() => 
            
            return RedirectToAction("index");
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");
         
            HttpResponseMessage responseMessage = client.GetAsync("books/" + id.ToString()).Result;
            BookViewModel abook = responseMessage.Content.ReadAsAsync<BookViewModel>().Result;
            return View(abook);
        }

        public IActionResult Delete(BookViewModel book)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44382/api/");
            long id = book.id;
                //HTTP DELETE
            client.DeleteAsync("books/" + id.ToString());
                //deleteTask.Wait();

          
            

            return RedirectToAction("Index");
        }
    }

      

    
}