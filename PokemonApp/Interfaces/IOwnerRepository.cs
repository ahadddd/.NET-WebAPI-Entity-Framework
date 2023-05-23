using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int id);

        ICollection<Owner> GetOwnerOfPokemon(int pokeID);

        ICollection<Pokemon> GetPokemonByOwner(int ownerID);

        bool OwnerExists(int ownerID);

    }
}
