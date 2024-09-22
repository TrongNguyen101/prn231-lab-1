using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient httpClient;
        
        public CustomerController(IHttpClientFactory httpClientFactory)
        {          
            _httpClientFactory = httpClientFactory;
             httpClient = _httpClientFactory.CreateClient("CustomerManagement");
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var httpResponseMessage = await httpClient.GetAsync($"Customer");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var customer = await JsonSerializer.DeserializeAsync<IEnumerable<Customer>>(contentStream);
                return View(customer);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        //// POST: Customers/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Fullname,Gender,Birthday,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var data = new StringContent(
                JsonSerializer.Serialize(customer),
                Encoding.UTF8,
                Application.Json);
                var httpResponseMessage = await httpClient.PostAsync($"Customer", data);
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        //// GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpResponseMessage = await httpClient.GetAsync($"Customer/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var customer = await JsonSerializer.DeserializeAsync<Customer>(contentStream);

                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
            return NotFound();
        }

        //// POST: Customers/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Fullname,Gender,Birthday,Address")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var customerJson = new StringContent(
                               JsonSerializer.Serialize(customer),
                               Encoding.UTF8,
                               Application.Json);

                using var httpResponseMessage = await httpClient.PutAsync($"Customer/{customer.Id}", customerJson);
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        //// GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpResponseMessage = await httpClient.GetAsync($"Customer/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var customer = await JsonSerializer.DeserializeAsync<Customer>(contentStream);

                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            return NotFound();           
        }

        //// POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpResponseMessage = await httpClient.GetAsync($"Customer/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var customer = await JsonSerializer.DeserializeAsync<Customer>(contentStream);

                if (customer == null)
                {
                    return NotFound();
                }

                var BookJson = new StringContent(
                                JsonSerializer.Serialize(customer),
                                Encoding.UTF8,
                                Application.Json);

                using var httpResponseMessage2 = await httpClient.DeleteAsync($"Customer/{id}");
                return RedirectToAction(nameof(Index));
            }
            return NotFound();     
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MultipleDelete(int[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var data = new StringContent(
            JsonSerializer.Serialize(selectedIds),
            Encoding.UTF8,
            Application.Json);

            var httpResponseMessage = await httpClient.PostAsync($"Customer/MultipleDelete", data);
            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index)); 
        }
    }
}