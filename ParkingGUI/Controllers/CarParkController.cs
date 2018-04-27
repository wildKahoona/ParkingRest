using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkingGUI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;

namespace ParkingGUI.Controllers
{
    public class CarParkController : Controller
    {
        // Hosted web API REST Service base url  
        string Baseurl = "http://ffhsparking.ddns.net";

        // GET: CarPark
        public async Task<ActionResult> Index()
        {
            var CarParkList = new List<CarPark>();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get using HttpClient  
                var response = await client.GetAsync("api/CarPark");

                //Checking the response is successful or not which is sent using HttpClient  
                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CarParkResponse = response.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the CarPark list  
                    CarParkList = JsonConvert.DeserializeObject<List<CarPark>>(CarParkResponse);
                }

                //returning the CarPark list to view  
                return View(CarParkList);
            }
        }

        // GET: CarPark/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var CarPark = new CarPark();
            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                var response = await client.GetAsync("api/CarPark/" + id);
                if (response.IsSuccessStatusCode)
                { 
                    var CarParkResponse = response.Content.ReadAsStringAsync().Result; 
                    CarPark = JsonConvert.DeserializeObject<CarPark>(CarParkResponse);
                }
                return View(CarPark);
            }
        }

        // GET: CarPark/Create
        public ActionResult Create()
        {
            var CarPark = new CarPark();
            return View(CarPark);
        }

        // POST: CarPark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                var newCarPark = new CarPark();
                newCarPark.Id = Convert.ToInt32(collection["Id"]);
                newCarPark.Nummer = Convert.ToInt32(collection["Nummer"]);
                newCarPark.Name = collection["Name"];
                newCarPark.Strasse = collection["Strasse"];
                newCarPark.Plz = Convert.ToInt32(collection["Plz"]);
                newCarPark.Ort = collection["Ort"];

                var stringData = JsonConvert.SerializeObject(newCarPark);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsync("api/CarPark", contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        //var CarParkResponse = response.Content.ReadAsStringAsync().Result;
                        //var CarPark = JsonConvert.DeserializeObject<CarPark>(CarParkResponse);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(newCarPark);
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CarPark/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var CarPark = new CarPark();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("api/CarPark/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var CarParkResponse = response.Content.ReadAsStringAsync().Result;
                    CarPark = JsonConvert.DeserializeObject<CarPark>(CarParkResponse);
                }
                return View(CarPark);
            }
        }

        // POST: CarPark/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            var editCarPark = new CarPark();
            editCarPark.Id = Convert.ToInt32(collection["Id"]);
            editCarPark.Nummer = Convert.ToInt32(collection["Nummer"]);
            editCarPark.Name = collection["Name"];
            editCarPark.Strasse = collection["Strasse"];
            editCarPark.Plz = Convert.ToInt32(collection["Plz"]);
            editCarPark.Ort = collection["Ort"];

            var stringData = JsonConvert.SerializeObject(editCarPark);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PutAsync("api/CarPark/" + id, contentData);
                if (response.IsSuccessStatusCode)
                {
                    //var CarParkResponse = response.Content.ReadAsStringAsync().Result;
                    //var CarPark = JsonConvert.DeserializeObject<CarPark>(CarParkResponse);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(editCarPark);
                }
            }
        }

        // GET: CarPark/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var CarParkList = new List<CarPark>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                var response = await client.GetAsync("api/CarPark"); 
                if (response.IsSuccessStatusCode)
                {
                    var CarParkResponse = response.Content.ReadAsStringAsync().Result;  
                    CarParkList = JsonConvert.DeserializeObject<List<CarPark>>(CarParkResponse);
                }

                return View(CarParkList.Where(x => x.Id == id).FirstOrDefault());
            }         
        }

        // POST: CarPark/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.DeleteAsync("api/CarPark/" + id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
        }
    }
}