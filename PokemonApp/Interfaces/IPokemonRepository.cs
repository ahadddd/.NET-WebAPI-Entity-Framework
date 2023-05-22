﻿using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        public ICollection<Pokemon> GetPokemons();

        public Pokemon GetPokemon(string name);

        public Pokemon GetPokemon(int id);

        public decimal GetPokemonRating(int pokeID);

        public bool PokemonExists(int pokeID);
    }
}
