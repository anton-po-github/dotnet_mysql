
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private ProductService _productService;
    private IMapper _mapper;

    public ProductController(
        ProductService productService,
        IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetById(id);
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product model)
    {
        _productService.Create(model);
        return Ok(new { message = "product created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product model)
    {
        _productService.Update(id, model);
        return Ok(new { message = "Product updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _productService.Delete(id);
        return Ok(new { message = "Product deleted" });
    }
}