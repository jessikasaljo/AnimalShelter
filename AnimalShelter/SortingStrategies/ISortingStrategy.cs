using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram
{
    public interface ISortingStrategy
    {
        void Sort(List<Animal> animals);

        void DisplaySortedAnimals(List<Animal> animals);
    }
}
