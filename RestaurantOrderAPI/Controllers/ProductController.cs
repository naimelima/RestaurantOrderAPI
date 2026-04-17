using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Iterfaces;
using RestaurantOrderAPI.Models;

namespace RestaurantOrderAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    public string message;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var product = _service.GetById(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create([FromBody]ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var created = _service.Create(product);
        
        return Ok(new { message = $"Produto com ID {created.Id} criado com sucesso."});
    }


    [HttpPut("{id}")]
    public IActionResult Update(string id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _service.Update(product);
        return Ok(new {message = $"Produto com ID {id} Atualizado com sucesso." });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _service.Delete(id);
        return Ok(new {message = $"Produto com ID {id} Deletado com sucesso." });

    }
}