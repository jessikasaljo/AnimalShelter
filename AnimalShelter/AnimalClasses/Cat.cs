using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.AnimalClasses
{
    public class Cat : Animal
    {
        public Cat()
        {
            Shelter = "City";
        }

        protected override void Speak(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: \"Meow\"!");
        }
    }
}
