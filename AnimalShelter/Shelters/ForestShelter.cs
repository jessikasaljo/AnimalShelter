using AnimalShelterProgram.AnimalClasses;
using AnimalShelterProgram.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.Shelters
{
    public class ForestShelter : AnimalShelter
    {
        //Singleton
        static ForestShelter instance;

        public static ForestShelter GetInstance()
        {
            if (instance == null)
            {
                instance = new ForestShelter();
                instance.Initialize();
            }
            return instance;
        }


        //Constructor
        ForestShelter() : base() { }


        //Separate method for initializing factories etc, to avoid infinite loop
        void Initialize()
        {
            Type = "Forest";

            factories["bird"] = new BirdFactory();
            factories["fox"] = new FoxFactory();
            factories["frog"] = new FrogFactory();

            foreach (string species in factories.Keys)
            {
                animalLists[species] = new List<Animal>();
            }

            //Example animals
            Bird stella = new Bird { Name = "Stella", Age = 2, Gender = "Male", Colour = "Yellow" };
            Fox micke = new Fox { Name = "Micke", Age = 1, Gender = "Male", Colour = "Red" };
            Frog trevor = new Frog { Name = "Trevor", Age = 2, Gender = "Male", Colour = "Green" };
            Bird polly = new Bird { Name = "Polly", Age = 18, Gender = "Female", Colour = "Red and green" };
            Fox vixen = new Fox() { Name = "Vixen", Age = 5, Gender = "Female", Colour = "White" };
            Bird tweety = new Bird() { Name = "Tweety", Age = 20, Gender = "Male", Colour = "Yellow" };

            shelterManager.AddAnimalToList("bird", stella);
            shelterManager.AddAnimalToList("fox", micke);
            shelterManager.AddAnimalToList("frog", trevor);
            shelterManager.AddAnimalToList("bird", polly);
            shelterManager.AddAnimalToList("fox", vixen);
            shelterManager.AddAnimalToList("bird", tweety);
        }
    }
}
