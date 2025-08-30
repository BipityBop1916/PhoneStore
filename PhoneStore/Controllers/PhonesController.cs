namespace PhoneStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;

public class PhonesController : Controller
{
    private readonly MobileContext _db;

    public PhonesController(MobileContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var phones = _db.Phones.ToList();
        return View(phones);
    }

    public IActionResult Add()
    {
        return View();
    }
    
    public IActionResult DownloadFile(string phoneName)
    {
        if (string.IsNullOrEmpty(phoneName))
            return NotFound();

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhoneFiles", phoneName + ".txt");

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        return PhysicalFile(filePath, "text/plain", phoneName + ".txt");
    }
    
    public IActionResult RedirectToManufacturer(string url)
    {
        if (string.IsNullOrEmpty(url))
            return NotFound();

        return Redirect(url);
    }
    
    [HttpPost]
    public IActionResult Add(Phone phone)
    {
        if (phone != null)
        {
            _db.Phones.Add(phone);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Phone phone = _db.Phones.FirstOrDefault(p => p.Id == id);
        if (phone == null) return NotFound();
        return View(phone);
    }
    
    [HttpPost]
    public IActionResult Edit(Phone phone)
    {
        if (!ModelState.IsValid)
            return View(phone);

        _db.Phones.Update(phone);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id == null) return NotFound();
    
        var phone = _db.Phones.FirstOrDefault(p => p.Id == id);
        if (phone == null) return NotFound();

        return View(phone);
    }

    [HttpPost]
    public IActionResult ConfirmDelete(int? id)
    {
        if (id != null)
        {
            var phone = _db.Phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                _db.Phones.Remove(phone);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }
}