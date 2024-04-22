using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram
{
    public class AlphabeticalStrategy : ISortingStrategy
    {
        public void Sort(List<Animal> animals)
        {
            if (animals == null || animals.Count <= 1)
                return;

            QuickSort(animals, 0, animals.Count - 1);
            DisplaySortedAnimals(animals);
        }


        public void DisplaySortedAnimals(List<Animal> animals)
        {
            Console.WriteLine("Our animals in alphabetical order:\r\n");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"Name: {animal.Name}, Age: {animal.Age}, ID: {animal.Id}");
            }
        }


        private void QuickSort(List<Animal> animals, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(animals, low, high);
                QuickSort(animals, low, pivotIndex - 1);
                QuickSort(animals, pivotIndex + 1, high);
            }
        }


        private int Partition(List<Animal> animals, int low, int high)
        {
            Animal pivot = animals[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (string.Compare(animals[j].Name, pivot.Name) < 0)
                {
                    i++;
                    Swap(animals, i, j);
                }
            }

            Swap(animals, i + 1, high);
            return i + 1;
        }


        private void Swap(List<Animal> animals, int i, int j)
        {
            Animal temp = animals[i];
            animals[i] = animals[j];
            animals[j] = temp;
        }
    }
}
