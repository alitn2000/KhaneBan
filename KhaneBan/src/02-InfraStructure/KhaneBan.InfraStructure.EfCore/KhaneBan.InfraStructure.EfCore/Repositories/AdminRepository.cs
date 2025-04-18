﻿using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AdminRepository> _logger;
    public AdminRepository(AppDbContext appDbContext, UserManager<User> userManager, ILogger<AdminRepository> logger)
    {
        _appDbContext = appDbContext;
        _userManager = userManager;
        _logger = logger;
    }
           public async Task<List<Admin>> GetAdminsAsync(CancellationToken cancellationToken)
           => await _appDbContext.Admins
           .AsNoTracking()
           .ToListAsync(cancellationToken);

    public async Task<Admin?> GetAdminByIdAsync(int userId, CancellationToken cancellationToken)
     => await _appDbContext
     .Admins
     .Include(a => a.User)
     .FirstOrDefaultAsync(c => c.User.Id == userId, cancellationToken);

    public async Task<List<Admin>> GetAdminsWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .Admins
        .AsNoTracking()
        .Include(a => a.User)
        .ToListAsync(cancellationToken);


    public async Task<bool> AddBalanceAsync(Admin admin, double addBalance, CancellationToken cancellationToken)
    {
        try
        {
            admin.User.Balance += addBalance;

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public async Task<bool> CreateAsync(Admin admin, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Admins.AddAsync(admin, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> DeleteAsync(Admin admin, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Admins.Remove(admin);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Admin admin, CancellationToken cancellationToken)
    {
        try
        {
            var existAdmin = await _appDbContext.Admins.Include(a => a.User).FirstOrDefaultAsync(c => c.Id == admin.Id, cancellationToken);

            if (existAdmin == null)
                return false;
            
                existAdmin.User.Address = admin.User.Address;
                existAdmin.User.FirstName = admin.User.FirstName;
                existAdmin.User.LastName = admin.User.LastName;
                existAdmin.User.Email = admin.User.Email;
                existAdmin.User.CityId = admin.User.CityId;
                existAdmin.User.PicturePath = admin.User.PicturePath;
                existAdmin.User.PhoneNumber = admin.User.PhoneNumber;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch 
        {
            throw new Exception("Logic Errorr!!!");
        }
        
    }

    public async Task<bool> PlusMoney(string userId, double amount, CancellationToken cancellationToken)
    {
        try
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {

                return false;
            }
            user.Balance += amount;


            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                return true;
            }
            else
            {

                _logger.LogError($"Error updating user inventory:");

                return false;
            }
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "An error occurred while increasing user inventory.");
            return false;
        }
    }

}
