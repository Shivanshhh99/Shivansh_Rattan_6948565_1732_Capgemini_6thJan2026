using System.Net.Http.Headers;
using ECommerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers;

public class OrderController : Controller
{
    private readonly IHttpClientFactory _factory;

    public OrderController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        var client = _factory.CreateClient("api");

        // paste user token here after login
        var token = "PASTE_USER_TOKEN_HERE";

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/orders", order);

        if (response.IsSuccessStatusCode)
            return RedirectToAction("Success");

        ViewBag.Error = await response.Content.ReadAsStringAsync();
        return View(order);
    }

    public IActionResult Success()
    {
        return View();
    }
}