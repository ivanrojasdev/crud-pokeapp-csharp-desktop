using Data.Implementations;
using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;

namespace Services
{
    public class PokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonService()
        {
            _pokemonRepository = new PokemonRepository();
        }

        public bool SavePokemon(Pokemon pokemon)
        {
            List<Pokemon> list = GetAllPokemons();
            bool exists = list.Exists(p => p.Name.ToLower().Contains(pokemon.Name.ToLower()) && p.PokemonId != pokemon.PokemonId);
            if (exists)
            {
                throw new InvalidOperationException("El pokemon ya existe");
            }

            if (pokemon.PokemonId != 0)
            {
                return _pokemonRepository.UpdatePokemon(pokemon);
            }
            else
            {
                return _pokemonRepository.CreatePokemon(pokemon);
            }
        }

        public List<Pokemon> GetAllPokemons()
        {
            return _pokemonRepository.GetAllPokemons();
        }

        public bool DeletePokemon(int pokemonId)
        {
            return _pokemonRepository.DeletePokemon(pokemonId);
        }
    }
}
