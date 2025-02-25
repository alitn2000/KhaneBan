using KhaneBan.Domain.Core.Contracts.Repository;
using KhaneBan.Domain.Core.Entites.BaseEntities;
using KhaneBan.Domain.Core.Entites.DTOs;
using KhaneBan.Domain.Core.Entites.User;
using KhaneBan.InfraStructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KhaneBan.InfraStructure.EfCore.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _appDbContext;

    public CityRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<CityDTO>> GetCitiesAsync(CancellationToken cancellationToken)
    {
               var cities = await _appDbContext.Cities.AsNoTracking().Select(c => new CityDTO
               {
                   Id = c.Id,
                   Title = c.Title,
               }).ToListAsync(cancellationToken);

        return cities;
    }



    public async Task<City?> GetCityByIdAsync(int id, CancellationToken cancellationToken)
        => await _appDbContext.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<City?> GetCityByName(string title, CancellationToken cancellationToken)
        => await _appDbContext.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Title == title, cancellationToken);

    public async Task<bool> CreateAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            await _appDbContext.Cities.AddAsync(city, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            _appDbContext.Cities.Remove(city);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(City city, CancellationToken cancellationToken)
    {
        try
        {
            var existCity = await _appDbContext.Cities.FirstOrDefaultAsync(c => c.Id == city.Id, cancellationToken);

            if (existCity == null)
                return false;

            existCity.Title = city.Title;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch
        {
            throw new Exception("Logic Errorr!!!");
        }

    }
}
