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


        public ActionResult Index()
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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookViewModel book)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44382/api/");

            client.PostAsJsonAsync<BookViewModel>("books", book);
            //.ContinueWith() => 

            return RedirectToAction("index");
        }



        //[HttpGet("{id}")]
        //public ActionResult Details(int id)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:44382/api/");

        //    HttpResponseMessage responseMessage = client.GetAsync("books/" + id.ToString()).Result;
        //    BookViewModel abook = responseMessage.Content.ReadAsAsync<BookViewModel>().Result;
        //    return View(abook);
        //}


        // [HttpPut("{id}")]
        public ActionResult Edit(int id)
        {
            BookViewModel book = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/api/");
                client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage responseMessage = client.GetAsync("/books" + id.ToString()).Result;
                //HTTP GET
                //var responseTask = client.GetAsync("books/" + id.ToString());
                //responseTask.Wait();

                if (responseMessage.IsSuccessStatusCode)
                {
                    var readTask = responseMessage.Content.ReadAsAsync<BookViewModel>();
                    readTask.Wait();

                    book = readTask.Result;
                }
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44382/api/");
                client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("aplication/json"));

                //HTTP POST
                var putTask = client.PutAsJsonAsync("books/" + book.id.ToString(), book);
                putTask.Wait();

                // await client.PutAsJsonAsync("api/products", product);
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("index");
                }
            }
            return View(book);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44382/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("books/" + id.ToString()).Result;
               
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
       
           


            return RedirectToAction("Index");
        }
    }
}




