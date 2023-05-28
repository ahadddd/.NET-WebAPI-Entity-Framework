using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Collections.Immutable;
using System.Security.AccessControl;

namespace PokemonApp.Repositories
{
    public class PokemonRepository: IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerIdentity = _context.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var categoryDetails = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonowner = new PokemonOwner()
            {
                Owner = pokemonOwnerIdentity,
                Pokemon = pokemon,
            };
            _context.Add(pokemonowner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = categoryDetails,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);
            _context.Add(pokemon);

            return Save();

        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeID)
        {
            var reviews = _context.Reviews.Where(p => p.Id == pokeID);
            if (reviews.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return (decimal)reviews.Sum(r => r.Rating) / reviews.Count();
            }

        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeID)
        {
            var pokemon = _context.Pokemons.Where(p => p.Id == pokeID).FirstOrDefault();

            if(pokemon == null)
                return false;   
            return true;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }
    }
}
