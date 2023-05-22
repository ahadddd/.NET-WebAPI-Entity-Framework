﻿using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();

        Country GetCountry(int id);

        Country GetCountryByOwner(int ownerID);

        ICollection<Owner> GetOwnersFromCountry(int countryID);

        bool CountryExists(int id);
    }
}
