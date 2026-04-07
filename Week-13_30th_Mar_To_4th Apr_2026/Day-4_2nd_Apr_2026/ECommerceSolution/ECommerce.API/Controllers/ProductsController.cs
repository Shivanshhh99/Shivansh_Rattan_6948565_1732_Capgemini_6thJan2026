using AutoMapper;
using ECommerce.API.DTOs;
using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductsController(IProductService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _service.GetProductsAsync();
        return Ok(products);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add(ProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _service.AddProductAsync(product);
        return Ok("Product Added");
    }
}