﻿using AnimalShelterProgram.AnimalClasses;
using AnimalShelterProgram.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.Shelters
{
    public class CityShelter : AnimalShelter
    {
        //Fields
        public Dictionary<string, AnimalFactory> factories = new Dictionary<string, AnimalFactory>();
        public Dictionary<string, List<Animal>> animalLists = new Dictionary<string, List<Animal>>();


        //Singleton
        static CityShelter instance;

        public static CityShelter GetInstance()
        {
            if (instance == null)
            {
                instance = new CityShelter();
                instance.Initialize();
            }
            return instance;
        }


        //Constructor
        CityShelter() : base() { }


        //Separate method for initializing factories etc, to avoid infinite loop
        void Initialize()
        {
            Type = "City";
            factories["cat"] = new CatFactory();
            factories["dog"] = new DogFactory();

            foreach (string species in factories.Keys)
            {
                animalLists[species] = new List<Animal>();
            }

            //Example animals
            Cat potRoast = new Cat { Name = "Pot Roast", Age = 3, Gender = "Female", Colour = "White" };
            Dog barkRuffalo = new Dog { Name = "Bark Ruffalo", Age = 5, Gender = "Male", Colour = "Brown" };
            Dog nala = new Dog { Name = "Nala", Age = 6, Gender = "Female", Colour = "White" };

            shelterManager.AddAnimalToList("cat", potRoast);
            shelterManager.AddAnimalToList("dog", barkRuffalo);
            shelterManager.AddAnimalToList("dog", nala);
        }


        public override void Run()
        {
            shelterManager.RunManager();
        }
    }
}
