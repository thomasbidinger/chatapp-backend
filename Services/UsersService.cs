using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaceBackend.Data;
using PaceBackend.DTOs.Requests;
using PaceBackend.Entities;

namespace PaceBackend.Services;

public class UsersService(ILogger<UsersService> logger, DataContext context)
{
    public async Task<List<User>> List()
    {
        return await context.Users.ToListAsync();
    }  
    
    public User? Get(int id)
    {
        return context.Users.Find(id);
    }
    
    public async Task<bool> Create(CreateUserRequest createUserRequest)
    {
        try
        {
            User newUser = new User()
            {
                Subject = createUserRequest.Subject,
                Name = createUserRequest.Name,
                Email = createUserRequest.Email,
                FirstName = "",
                LastName = "",
            };


            context.Users.Add(newUser);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not create user");
            throw;
        }
        
        return true;
    }
}