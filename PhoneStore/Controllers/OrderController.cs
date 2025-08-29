namespace PhoneStore.Controllers;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;
using Microsoft.EntityFrameworkCore;

public class OrdersController : Controller
{
    private readonly MobileContext _db;

    public OrdersController(MobileContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<Order> orders = _db.Orders.Include(o => o.Phone).ToList();
        return View(orders);
    }
    
    public IActionResult Create(int phoneId)
    {
        Phone? phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
        if (phone == null) return NotFound();

        return View(new Order { Phone = phone });
    }
    
    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (order != null)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}