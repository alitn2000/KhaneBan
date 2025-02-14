using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken)
           => await _appDbContext.Customers
           .ToListAsync(cancellationToken);

    public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Customers
     .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<List<Customer>> GetCustomersWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .Customers
        .Include(c => c.Requests)
        .ThenInclude(c => c.Suggestions)
        .Include(c => c.Ratings)
        .Include(c => c.User)
        .ThenInclude(c => c.City)
        .ToListAsync(cancellationToken);


    public async Task<Customer?> GetCustomerByIdWithDetailsAsync(int userId, CancellationToken cancellationToken)
        => await _appDbContext
        .Customers
        .Include(c => c.Requests)
        .ThenInclude(c => c.Suggestions)
        .Include(c => c.Ratings)
        .Include(c => c.User)
        .ThenInclude(c => c.City)
        .FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);

    public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Customers.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Customers.Remove(customer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }



    public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            var existCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);

            if (existCustomer == null)
                return false;

            existCustomer.User.Address = customer.User.Address;
            existCustomer.User.FirstName = customer.User.FirstName;
            existCustomer.User.LastName = customer.User.LastName;
            existCustomer.User.Email = customer.User.Email;
            existCustomer.User.CityId = customer.User.CityId;
            existCustomer.User.PicturePath = customer.User.PicturePath;
            existCustomer.User.PhoneNumber = customer.User.PhoneNumber;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch 
        {
            throw new Exception("Logic Errorr!!!");
        }




    }


    //public async Task<bool> AddBalanceAsync(Customer customer, double addBalance, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        customer.User.Balance += addBalance;

    //        await _appDbContext.SaveChangesAsync(cancellationToken);
    //        return true;
    //    }
    //    catch
    //    {
    //        return false;
    //    }
    //}

}
