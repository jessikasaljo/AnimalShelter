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
        //Fields
        public Dictionary<string, AnimalFactory> factories = new Dictionary<string, AnimalFactory>();
        public Dictionary<string, List<Animal>> animalLists = new Dictionary<string, List<Animal>>();


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

            shelterManager.AddAnimalToList("bird", stella);
            shelterManager.AddAnimalToList("fox", micke);
            shelterManager.AddAnimalToList("frog", trevor);
        }

        public override void Run()
        {
            shelterManager.RunManager();
        }
    }
}
