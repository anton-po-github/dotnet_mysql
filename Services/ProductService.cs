using AutoMapper;

public class ProductService
{
    private ProductsContext _context;
    private readonly IMapper _mapper;

    public ProductService(
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

    public Product GetById(int id)
    {
        return getProduct(id);
    }

    public void Create(Product model)
    {
        // validate
        /* if (_context.Products.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists"); */

        // map model to new user object
        var product = _mapper.Map<Product>(model);

        // save user
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(int id, Product model)
    {
        var user = getProduct(id);

        // validate
        /*    if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
               throw new AppException("User with the email '" + model.Email + "' already exists"); */

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Products.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getProduct(id);
        _context.Products.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private Product getProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) throw new KeyNotFoundException("User not found");
        return product;
    }
}
