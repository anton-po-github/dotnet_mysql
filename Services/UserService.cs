﻿using AutoMapper;

public class UserService
{
    private UsersContext _context;
    private readonly IMapper _mapper;

    public UserService(
        UsersContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Create(CreateRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // save user
        _context.Users.Add(user);

        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // validate
        if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
            throw new AppException("User with the email '" + model.Email + "' already exists");

        // copy model to user and save
        _mapper.Map(model, user);
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    // helper methods

    private User getUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null) throw new KeyNotFoundException("User not found");

        return user;
    }
}

