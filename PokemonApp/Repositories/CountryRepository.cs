using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CountryRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context; 
            
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(country => country.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerID)
        {
            return _context.Owners.Where(o => o.Id == ownerID).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryID)
        {
            return _context.Owners.Where(o => o.Country.Id == countryID).ToList();
        }
    }
}
