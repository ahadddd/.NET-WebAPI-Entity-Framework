using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using System.Collections.Immutable;

namespace PokemonApp.Repositories
{
    public class PokemonRepository: IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
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
    }
}
