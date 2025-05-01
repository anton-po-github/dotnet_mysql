
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private ProductsService _productsService;
    private IMapper _mapper;

    public ProductsController(
        ProductsService productService,
        IMapper mapper)
    {
        _productsService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productsService.GetAll();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var product = _productsService.GetById(id);

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product model)
    {
        _productsService.Create(model);

        return Ok(new { message = "product created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Product model)
    {
        _productsService.Update(id, model);

        return Ok(new { message = "Product updated" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _productsService.Delete(id);

        return Ok(new { message = "Product deleted" });
    }
}