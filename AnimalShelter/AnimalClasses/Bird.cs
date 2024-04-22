using AnimalShelterProgram.Shelters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.AnimalClasses
{
    public class Bird : Animal
    {
        public Bird()
        {
            Shelter = "Forest";
        }

        protected override void Speak(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: \"Tweet\"!");
        }
    }
}
