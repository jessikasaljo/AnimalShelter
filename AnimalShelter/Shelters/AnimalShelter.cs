using AnimalShelterProgram.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.Shelters
{
    public class AnimalShelter
    {
        //Fields
        public Dictionary<string, AnimalFactory> factories = new Dictionary<string, AnimalFactory>();
        public Dictionary<string, List<Animal>> animalLists = new Dictionary<string, List<Animal>>();

        protected AnimalShelterManager shelterManager;
        protected AnimalShelter animalShelter;

        public string Type { get; set; }


        //Constructor
        protected AnimalShelter()
        {
            animalShelter = this;
            shelterManager = AnimalShelterManager.GetInstance();
            Type = "Animal";
        }


        //Calls the ShelterManager run function
        public void Run()
        {
            shelterManager.RunManager();
        }
    }
}
