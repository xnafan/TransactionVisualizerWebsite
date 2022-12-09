using Microsoft.AspNetCore.Mvc;
using TransactionVisualizerWebsite.Database;
using TransactionVisualizerWebsite.Models;

namespace TransactionVisualizerWebsite.Controllers;
public class HomeController : Controller
{
    private DatabaseAccess databaseAccess;

    public HomeController(DatabaseAccess dbAccess) => databaseAccess = dbAccess;

    private static readonly int PRODUCT_ID = 1;
    public IActionResult Index()
    {
        ViewData["currentStock"] = databaseAccess.GetProductStock(PRODUCT_ID);
        return View(new StockUpdateParameters());
    }   

    [HttpPost]
    public IActionResult UpdateStock(StockUpdateParameters parameters)
    {
        var stockBefore = databaseAccess.GetProductStock(parameters.ProductId);
        var desiredQuantityInOrder = parameters.Quantity;
        var updatedSuccessfully = databaseAccess.RemoveProductIfEnoughInStock(parameters);
        var stockAfter = databaseAccess.GetProductStock(parameters.ProductId);
        return View(new {updated=updatedSuccessfully, stockBefore = stockBefore, stockAfter = stockAfter, desiredQuantityInOrder= desiredQuantityInOrder });
    }

    public IActionResult SetStock([FromQuery] int newStockAmount)
    {
        databaseAccess.SetStock(PRODUCT_ID, newStockAmount);
        return Redirect(nameof(Index));
    }

    public IActionResult ChangeStock([FromQuery] int changeInStock)
    {
        databaseAccess.AlterStock(PRODUCT_ID, changeInStock);
        return Redirect(nameof(Index));
    }
}