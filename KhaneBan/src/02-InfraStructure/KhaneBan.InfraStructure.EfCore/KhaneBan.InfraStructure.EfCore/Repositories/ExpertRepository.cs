﻿using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.Domain.Core.Enums;
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

public class ExpertRepository :   IExpertRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<ExpertRepository> _logger;
    private readonly UserManager<User> _userManager;

    public ExpertRepository(AppDbContext appDbContext, ILogger<ExpertRepository> logger, UserManager<User> userManager)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<List<Expert>> GetExpertsAsync(CancellationToken cancellationToken)
        => await _appDbContext.Experts.AsNoTracking()
        .ToListAsync(cancellationToken);

    public async Task<List<Expert>> GetExpertInfoAsync(CancellationToken cancellationToken)
       => await _appDbContext.Experts.Include(e => e.User).ToListAsync(cancellationToken);

    public async Task<Expert?> GetExpertInfoByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Experts
     .Include(c => c.User)
     .FirstOrDefaultAsync(c => c.UserId == id, cancellationToken);

    public async Task<Expert?> GetExpertByIdAsync(int id, CancellationToken cancellationToken)
     => await _appDbContext
     .Experts
     .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<List<Expert>> GetExpertsWithDetailsAsync(CancellationToken cancellationToken)
       => await _appDbContext
        .Experts
        .Include(e => e.User)
        .Include(e => e.HomeServices)
        .ThenInclude(e => e.SubCategory)
        .ThenInclude(e => e.Category)
        .Include(e => e.Suggestions)
        .ToListAsync(cancellationToken);
    public async Task<Expert?> GetExpertByIdWithDetailsAsync(int id, CancellationToken cancellationToken)

    => await _appDbContext
    .Experts
    .Include(x => x.User)
    .ThenInclude(x => x.City)
    .Include(x => x.HomeServices)
    .ThenInclude(x => x.SubCategory)
    .ThenInclude(x => x.Category)
    .Include(x => x.Suggestions)
    .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

    public async Task<ExpertProfileDTO?> GetExpertProfileByIdAsync(int id, CancellationToken cancellationToken)
    {
        var expert = await _appDbContext.Experts
     .Where(e => e.UserId == id)
     .Include(e => e.User)
     .Include(e => e.HomeServices) 
     .Include(e => e.Suggestions)
     .Include(e => e.Ratings)
     .ThenInclude(r => r.Customer) 
     .ThenInclude(c => c.User) 
     .Select(e => new ExpertProfileDTO
     {
         Id = e.Id,
         FirstName = e.User.FirstName,
         LastName = e.User.LastName,
         Email = e.User.Email,
         PhoneNumber = e.User.PhoneNumber,
         Address = e.User.Address,
         PicturePath = e.User.PicturePath ?? "/images/default.png",
         CityTitle = e.User.City.Title,
         Balance = e.User.Balance,
         IsDeleted = e.User.IsDeleted,
         RegisterDate = e.User.RegisterDate,

         HomeServices = e.HomeServices != null ? e.HomeServices.Select(hs => new ServiceDto
         {
             Id = hs.Id,
             Title = hs.Title,
             PicturePath = hs.PicturePath ?? "/images/no-image.png"
         }).ToList() : new List<ServiceDto>(),

         Suggestions = e.Suggestions.Select(s => new SuggestionDto
         {
             Id = s.Id,
             Description = s.Description,
             Price = s.Price,
             SuggestionStatus = s.SuggestionStatus
         }).ToList(),

         Ratings = e.Ratings.Select(r => new RatingDto
         {
             Rate = r.Rate,
             Title = r.Title,
             CustomerUserName = r.Customer.User.UserName,
             CustomerPicturePath = r.Customer.User.PicturePath ?? "/images/default-avatar.png",
             Comment = r.Comment,
             RegisterDate = r.RegisterDate
         }).ToList()
     })
     .FirstOrDefaultAsync(cancellationToken);

        return expert;
    }



    public async Task<int> GetCountExpertAsync(CancellationToken cancellationToken)
       => await _appDbContext
       .Experts.AsNoTracking().CountAsync(cancellationToken);

    public async Task<Expert?> GetByIdAsync(int id, CancellationToken cancellationToken)

         => await _appDbContext.Experts.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);     

    public async Task<bool> CreateAsync(Expert expert, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Experts.AddAsync(expert, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" create expert Succesfully");
            return true;
        }
        catch(Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var existExpert = await _appDbContext
                .Experts.Include(u => u.User)
                .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

            if (existExpert == null)
                return false;

            existExpert.User.IsDeleted = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" delete expert Succesfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
            return false;
        }
    }

    public async Task ActiveExpert(int userId, CancellationToken cancellationToken)
    {

        try
        {
            var existExpert = await _appDbContext
            .Experts
            .Include(u => u.User)
            .FirstOrDefaultAsync(c => c.Id == userId, cancellationToken);
            if (existExpert == null)
                return;

            existExpert.User.IsDeleted = false;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation(" Active expert Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in expert repository=======================>>>>>>>>>>>{ErrorMessage}", ex.Message);
        }
    }

   

    public async Task<bool> UpdateAsync(Expert expert, List<int> selectedHomeServiceIds, CancellationToken cancellationToken)
    {
        try
        {
            if (expert == null || expert.UserId == null)
            {
                _logger.LogError("Expert or Expert.UserId is null.");
                return false;
            }

            var existExpert = await _appDbContext.Experts
                .Include(x => x.User)
                .Include(x => x.HomeServices)
                .FirstOrDefaultAsync(x => x.UserId == expert.UserId, cancellationToken);

            if (existExpert == null)
            {
                _logger.LogWarning($"Expert with UserId {expert.UserId} not found.");
                return false;
            }

            existExpert.User.Address = expert.User.Address;
            existExpert.User.FirstName = expert.User.FirstName;
            existExpert.User.LastName = expert.User.LastName;
            existExpert.User.UserName = expert.User.UserName;
            existExpert.User.Email = expert.User.Email;
            existExpert.User.CityId = expert.User.CityId;
            existExpert.User.PicturePath = expert.User.PicturePath;
            existExpert.User.PhoneNumber = expert.User.PhoneNumber;

            if (selectedHomeServiceIds != null)
            {
                if (existExpert.HomeServices != null)
                {
                    existExpert.HomeServices.RemoveAll(hs => !selectedHomeServiceIds.Contains(hs.Id));

                    var newHomeServices = await _appDbContext.HomeServices
                        .Where(hs => selectedHomeServiceIds.Contains(hs.Id))
                        .ToListAsync(cancellationToken);

                    foreach (var newHomeService in newHomeServices)
                    {
                        if (!existExpert.HomeServices.Any(hs => hs.Id == newHomeService.Id))
                        {
                            existExpert.HomeServices.Add(newHomeService);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("existExpert.HomeServices is null.");
                }

            }
            else if (existExpert.HomeServices != null)
            {
                existExpert.HomeServices.Clear();
            }

            await _appDbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Expert updated successfully.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating expert: {ex.Message}");
            return false;
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

                foreach (var error in result.Errors)
                {
                    _logger.LogError($"Error updating user inventory: {error.Code} - {error.Description}");
                }
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
