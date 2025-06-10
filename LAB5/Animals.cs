using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooshopApp
{
    class Animal
    {
        int id;
        string species;
        string breed;

        public Animal(int id, string species, string breed)
        {
            this.id = id;
            this.species = species;
            this.breed = breed;
        }

        public int GetID()
        {
            return id;
        }

        public string GetSpecies()
        {
            return species;
        }

        public string GetBreed()
        {
            return breed;
        }

        public override string ToString()
        {
            return $"{id} | {species} | {breed}";
        }
    }
}