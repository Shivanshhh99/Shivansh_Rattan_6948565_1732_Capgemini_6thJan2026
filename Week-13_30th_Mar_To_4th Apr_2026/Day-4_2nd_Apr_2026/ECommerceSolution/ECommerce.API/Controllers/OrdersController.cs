using AutoMapper;
using ECommerce.API.DTOs;
using ECommerce.API.Models;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize(Roles = "User,Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(OrderDto dto)
    {
        var order = _mapper.Map<Order>(dto);
        await _service.PlaceOrderAsync(order);
        return Ok("Order Created");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetOrdersAsync();
        return Ok(orders);
    }
}