using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.AnimalClasses
{
    public class Fox : Animal
    {
        public Fox() 
        {
            Shelter = "Forest";
        }

        protected override void Speak(Animal animal)
        {
            Console.WriteLine($"{animal.Name} says: \"???\""); //WHAT DOES THE FOX SAY?
        }
    }
}
