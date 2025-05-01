using AutoMapper;

public class ProductsService
{
    private ProductsContext _context;
    private readonly IMapper _mapper;

    public ProductsService(
        ProductsContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products;
    }

    public Product GetById(Guid id)
    {
        return getProduct(id);
    }

    public void Create(Product model)
    {
        var product = _mapper.Map<Product>(model);

        _context.Products.Add(product);

        _context.SaveChanges();
    }

    public void Update(Guid id, Product model)
    {
        var product = getProduct(id);

        product.Price = model.Price;
        product.Discount = model.Discount;
        product.ProductName = model.ProductName;
        product.ProductDescription = model.ProductDescription;

        _context.Products.Update(product);

        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var product = getProduct(id);

        _context.Products.Remove(product);

        _context.SaveChanges();
    }

    private Product getProduct(Guid id)
    {
        var product = _context.Products.Find(id);

        if (product == null) throw new KeyNotFoundException("Product not found");

        return product;
    }
}
