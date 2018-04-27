using Microsoft.AspNetCore.Mvc;
using ParkingRest.DataAccess;
using ParkingRest.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParkingRest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarParkController : Controller
    {
        // GET – zum Abfragen einer Ressource

        // POST – zum Neu-Anlegen einer Ressource
        // z.B. POST http://www.acme.com/basedata/customers/4712 zum Anlegen des Kunden mit der Kundennummer 4712. 
        // Die Kundendaten werden im Body des HTTP-Requests übertragen.

        // PUT – zum Neu-Anlegen oder Aktualisieren einer Ressource
        // z.B.PUT http://www.acme.com/basedata/customers/4713 zum Aktualisieren des Kunden mit der Kundennummer 4713 (z.B. neue Post-Adresse). 
        // Die Kundendaten werden auch hier im Body des HTTP-Requests übertragen.

       // DELETE – zum Löschen einer Ressource

        private readonly ApplicationDbContext _context;

        /// <summary>
        /// CarPark Controller
        /// </summary>
        /// <param name="context"></param>
        public CarParkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CarPark
        /// <summary>
        /// Retrieve all existing CarParks http GET. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CarPark> Get()
        {
            return _context.CarParks.ToList();
        }

        // GET: api/CarPark/5
        /// <summary>
        /// Retrieve an existing CarPark with an id via http GET.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public CarPark Get(int id)
        {
            var findCarPark = _context.CarParks.Where(x => x.Id == id).FirstOrDefault();
            return findCarPark;
        }

        // POST: api/CarPark
        /// <summary>
        /// Create a new CarPark via http POST and JSON payload representing the CarPark to create. 
        /// </summary>
        /// <param name="carPark"></param>
        /// <returns></returns>
        [HttpPost]
        public CarPark Post([FromBody]CarPark carPark)
        {
            _context.CarParks.Add(carPark);
            _context.SaveChanges();
            return carPark;
        }

        // PUT: api/CarPark/5
        /// <summary>
        /// // Update an existing CarPark with id via http PUT and JSON payload representing the changed CarPark
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carPark"></param>
        [HttpPut("{id}")]
        public CarPark Put(int id, [FromBody]CarPark carPark)
        {
            var findCarPark = _context.CarParks.Where(x => x.Id == id).FirstOrDefault();
            if (findCarPark != null)
            {
                // CarPark updaten
                findCarPark.Nummer = carPark.Nummer;
                findCarPark.Name = carPark.Name;
                findCarPark.Strasse = carPark.Strasse;
                findCarPark.Plz = carPark.Plz;
                findCarPark.Ort = carPark.Ort;
                _context.SaveChanges();
            }
            else
            {
                // Neuer CarPark erstellen
                _context.CarParks.Add(carPark);
                _context.SaveChanges();
            }
            return carPark;
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete an existing CarPark with an id via http DELETE. 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var findCarPark = _context.CarParks.Where(x => x.Id == id).FirstOrDefault();
            if (findCarPark != null)
            {
                _context.CarParks.Remove(findCarPark);
                _context.SaveChanges();
            }
        }
    }
}
