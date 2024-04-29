using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Routing;

public class ProductController : Controller
{
  // this controller depends on the NorthwindRepository
  private DataContext _dataContext;
  public ProductController(DataContext db) => _dataContext = db;
  public IActionResult Category() => View(_dataContext.Categories.OrderBy(c => c.CategoryName));
  public IActionResult Index(int id){
    ViewBag.id = id;
    return View(_dataContext.Categories.OrderBy(c => c.CategoryName));
  }
  public IActionResult Cart() => View(_dataContext.CartItems.Include("Product").OrderBy(c => c.CartItemId));
  
    // [Authorize(Roles = "northwind-customer"), HttpPost, ValidateAntiForgeryToken]
    // public async System.Threading.Tasks.Task<IActionResult> Cart(int itemId){
    //   CartItem itemToDelete = _dataContext.CartItems.FirstOrDefault(c => c.CartItemId == itemId);
    //   _dataContext.RemoveFromCart(itemToDelete);
    //   return RedirectToAction("Index", "Home");
    // } 

  [HttpGet]
  [Route("Product/Cart/{cartId:int}")]
   public IActionResult Cart(int cartId)
        {
            CartItem cart = _dataContext.CartItems.FirstOrDefault(c => c.CartItemId == cartId);
           _dataContext.RemoveFromCart(cart);

          return RedirectToAction("Redirect", "Product");
        }


public IActionResult Redirect(){
  return RedirectToAction("Cart", "Product");
}


}
