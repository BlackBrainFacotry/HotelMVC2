using HotelMVC2.Data;
using HotelMVC2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
namespace HotelMVC2.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HotelBookingContext _context;
        List<double?> salmons = new List<double?>();
        List<string> customersID = new List<string>();
        public CustomersController()
        {
            // Create database context
            var options = new DbContextOptionsBuilder<HotelBookingContext>()
                .UseInMemoryDatabase("HotelBookingDb")
                .Options;
            _context = new HotelBookingContext(options);
            
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {

            foreach (var customer in _context.Customer.ToList())
            {
                var countofreservation = from b in _context.Booking where b.CustomerId == customer.Id select b;
                customer.BookingsCount = countofreservation.Count();
                salmons.Add((double)countofreservation.Count());
                customersID.Add((customer.Id).ToString());
            }
            ViewBag.ReservationsCustomer = salmons;

            Chart chart = new Chart();

            chart.Type = Enums.ChartType.Line;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = customersID;

            BarDataset dataset = new BarDataset()
            {
                Label = "Ranking pracowników",
                Data = salmons,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 1),
                    ChartColor.FromRgba(54, 162, 235, 1),
                    ChartColor.FromRgba(255, 206, 86, 1),
                    ChartColor.FromRgba(75, 192, 192, 1),
                    ChartColor.FromRgba(153, 102, 255, 1),
                    ChartColor.FromRgba(255, 159, 64, 1)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            ViewData["chart"] = chart;


            return View(await _context.Customer.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
