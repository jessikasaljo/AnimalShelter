using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AnimalShelterProgram.Factories
{
    public class AnimalFactory
    {
        //Functions for creating an new animal
        protected virtual Animal CreateAnimal(string name, int age, string gender, string colour)
        {
            return new Animal
            {
                Name = name,
                Age = age,
                Gender = gender,
                Colour = colour
            };
        }

        public Animal GetCreatedAnimal(string name, int age, string gender, string colour)
        {
            Animal animal = CreateAnimal(name, age, gender, colour);
            return animal;
        }
    }
}
