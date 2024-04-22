﻿using AnimalShelterProgram.AnimalClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.Factories
{
    public class FrogFactory : AnimalFactory
    {
        protected override Animal CreateAnimal(string name, int age, string gender, string colour)
        {
            return new Frog
            {
                Name = name,
                Age = age,
                Gender = gender,
                Colour = colour
            };
        }
    }
}
