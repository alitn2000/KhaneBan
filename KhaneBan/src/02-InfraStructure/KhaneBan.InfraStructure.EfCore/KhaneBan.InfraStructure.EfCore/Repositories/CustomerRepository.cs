using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(AppDbContext appDbContext, ILogger<CustomerRepository> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken)
           => await _appDbContext.Customers
           .ToListAsync(cancellationToken);

    public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Customers
     .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    public async Task<Customer?> GetCustomerInfoByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Customers
     .Include(c => c.User)
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

    public async Task<List<Customer>> GetCustomerInfoAsync()
    {
        try
        {
            var x = await _appDbContext.Customers.Include(c => c.User).ToListAsync();
            return x;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



    public async Task<Customer?> GetCustomerByIdWithDetailsAsync(int userId, CancellationToken cancellationToken)
        => await _appDbContext
        .Customers
        .Include(c => c.Requests)
        .ThenInclude(c => c.Suggestions)
        .Include(c => c.Ratings)
        .Include(c => c.User)
        .ThenInclude(c => c.City)
        .FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);

    public async Task<int> GetCountCustomerAsync(CancellationToken cancellationToken)
        => await _appDbContext
        .Customers.AsNoTracking().CountAsync(cancellationToken);

    public async Task<bool> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Customers.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Category Added Succesfully");
            return true;
        }
        catch
        {
            _logger.LogError("something is wrong in create category");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var existcustomer = await _appDbContext
                .Customers.Include(u => u.User)
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

            if (existcustomer == null)
                return false;

            existcustomer.User.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        catch (Exception ex)
        {
            _logger.LogError("Error in customer repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }

    }

    public async Task ActiveCustomer(int userId, CancellationToken cancellationToken)
    {

        try
        {
            var existcustomer = await _appDbContext
            .Customers
            .Include(u => u.User)
            .FirstOrDefaultAsync(c => c.Id == userId, cancellationToken);

            existcustomer.User.IsDeleted = false;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active customer Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in customer repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }



    public async Task<bool> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            var existCustomer = await _appDbContext.Customers.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == customer.Id, cancellationToken);

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
            _logger.LogInformation(" update customer Succesfully");
        
            return true;

        }
        catch(Exception ex)
        {

            _logger.LogError("Error in customer repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
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
