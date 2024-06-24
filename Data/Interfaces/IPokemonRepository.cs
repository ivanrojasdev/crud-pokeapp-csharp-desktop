using Entities;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IPokemonRepository
    {
        bool CreatePokemon(Pokemon pokemon);
        bool UpdatePokemon(Pokemon pokemon);
        bool DeletePokemon(int pokemonId);
        List<Pokemon> GetAllPokemons();
    }
}
