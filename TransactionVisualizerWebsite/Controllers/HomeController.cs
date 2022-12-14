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
    public IActionResult UpdateStock(StockUpdateParameters parameters) => TryUpdate(parameters);

    public IActionResult Settings()
    {
        return View(new StockUpdateParameters());
    }

    [HttpPost]
    public IActionResult Settings(StockUpdateParameters parameters) => RedirectToAction("Index");

    private IActionResult TryUpdate(StockUpdateParameters parameters)
    {
        var stockBefore = databaseAccess.GetProductStock(parameters.ProductId);
        bool updatedSuccessfully = false;
        int attempt = 0;
        var exceptions = new List<Exception>();
        while (attempt < parameters.MaxAttempts && !updatedSuccessfully)
        {
            try
            {
                updatedSuccessfully = databaseAccess.RemoveProductIfEnoughInStock(parameters);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            attempt++;
        }

        var stockAfter = databaseAccess.GetProductStock(parameters.ProductId);
            return View(new { updated = updatedSuccessfully, stockBefore = stockBefore, stockAfter = stockAfter, attempts = attempt, stockUpdateParameters = parameters, exceptions = exceptions });
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