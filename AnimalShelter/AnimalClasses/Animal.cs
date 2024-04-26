using AnimalShelterProgram.BehaviourStates;
using AnimalShelterProgram.Shelters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimalShelterProgram.AnimalShelterManager;

namespace AnimalShelterProgram
{
    public class Animal
    {
        //Fields
        private static int lastId = 0;
        public int Id { get; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Colour { get; set; }
        public string Shelter { get; set; }
        public bool IsAdopted { get; set; }
        public IBehaviourState Behaviour { get; set; }


        //Constructor with default values
        public Animal()
        {
            Id = ++lastId;
            Name = "Unknown";
            Age = 0;
            Gender = "Unknown";
            Colour = "Unknown";
            Shelter = "Unknown";
            IsAdopted = false;
        }


        //Functions for saying hello (Template Method Pattern)
        public void SayHello(Animal animal)
        {
            Console.Write($"\r\nYou say hello to {animal.Name}. ");
            Speak(animal);
        }
        
        protected virtual void Speak(Animal animal) { }


        //Sets behaviour based on user input
        public void SetBehaviour(IBehaviourState behaviour)
        {
            Behaviour = behaviour;
        }


        //Displays info about the animal
        public void DisplayInfo()
        {
            Console.Clear();
            string species = GetType().Name.ToLower();
            Console.WriteLine($"{Name} is a {Age} year old {Gender.ToLower()} {Colour.ToLower()} {species}.");
            
            if (IsAdopted)
            {
                Console.WriteLine($"{Name} has been adopted.\r\n");
            }
            else
            {
                Console.WriteLine($"{Name} is available for adoption!\r\n");
            }                 
        }
    }
}
