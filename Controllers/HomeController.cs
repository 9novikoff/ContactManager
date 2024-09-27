using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using ContactManager.Services;

namespace ContactManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CsvService _csvService;
    private readonly ContactService _contactService;

    public HomeController(ILogger<HomeController> logger, CsvService csvService, ContactService contactService)
    {
        _logger = logger;
        _csvService = csvService;
        _contactService = contactService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Upload(IFormFile csvFile)
    {
        if (csvFile != null && csvFile.Length > 0)
        {
            await using var stream = csvFile.OpenReadStream();
            try
            {
                var contacts = _csvService.ReadCsvFile<ContactDto>(stream);
                await _contactService.CreateContactsAsync(contacts.Select(c => new Contact
                {
                    Name = c.Name,
                    DateOfBirth = c.DateOfBirth,
                    Married = c.Married,
                    Phone = c.Phone,
                    Salary = c.Salary
                }).ToList());
                return RedirectToAction("Show");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Please select a valid CSV file.");
        }
        return RedirectToAction("Error");
    }
    
    public IActionResult Show()
    {
        return View(_contactService.GetContacts());
    }
    
    public async Task<IActionResult> Edit(int id, Contact contact)
    {
        await _contactService.UpdateContactAsync(id, contact);
        
        return RedirectToAction("Show");
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        await _contactService.DeleteContactAsyncById(id);
        
        return RedirectToAction("Show");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}