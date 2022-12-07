using Microsoft.AspNetCore.Mvc;
using TransactionVisualizerWebsite.Database;
using TransactionVisualizerWebsite.Models;

namespace TransactionVisualizerWebsite.Controllers;
public class HomeController : Controller
{
    private static readonly int productId = 1;
    public IActionResult Index()
    {
        //var x = new DatabaseAccess().RemoveProductIfInStock(1, 5);
        return View(new StockUpdateParameters());
    }   

    [HttpPost]
    public IActionResult UpdateStock(StockUpdateParameters parameters)
    {
        var db = new DatabaseAccess();
        var updatedSuccessfully = db.RemoveProductIfInStock(parameters);
        var newStock = db.GetProductStock(parameters.ProductId);
        return View(new {updated=updatedSuccessfully, stock=newStock });
    }

    public IActionResult RefreshStock([FromQuery] int newStockAmount)
    {
        var db = new DatabaseAccess();
        db.SetStock(productId, newStockAmount);
        return Redirect(nameof(Index));
    }
}