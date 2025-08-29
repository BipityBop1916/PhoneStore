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
}