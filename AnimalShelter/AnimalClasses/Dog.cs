using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.AnimalClasses
{
    public class Dog : Animal
    {
        public Dog() 
        {
            Shelter = "City";
        }

        protected override void Speak(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: \"Woof\"!");
        }
    }
}
