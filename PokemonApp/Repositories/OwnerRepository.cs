using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public Owner GetOwner(int id)
        {
           return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfPokemon(int pokeID)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeID).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerID)
        {
            return _context.PokemonOwners.Where(o => o.OwnerId == ownerID).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerID)
        {
            return _context.Owners.Any(o => o.Id == ownerID);
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}
