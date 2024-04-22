using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.SortingStrategies
{
    public class ByIDStrategy : ISortingStrategy
    {
        public void Sort(List<Animal> animals)
        {
            if (animals == null || animals.Count <= 1)
                return;

            MergeSort(animals, 0, animals.Count - 1);
            DisplaySortedAnimals(animals);
        }


        public void DisplaySortedAnimals(List<Animal> animals)
        {
            Console.WriteLine("Our animals sorted by ID:\r\n");
            foreach (Animal animal in animals)
            {
                Console.WriteLine($"Name: {animal.Name}, Age: {animal.Age}, ID: {animal.Id}");
            }
        }


        void MergeSort(List<Animal> animals, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;

                MergeSort(animals, left, mid);
                MergeSort(animals, mid + 1, right);

                MergeByID(animals, left, mid, right);
            }
        }


        void MergeByID(List<Animal> animals, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            List<Animal> leftArray = new List<Animal>(n1);
            List<Animal> rightArray = new List<Animal>(n2);

            for (int i = 0; i < n1; ++i)
                leftArray.Add(animals[left + i]);
            for (int j = 0; j < n2; ++j)
                rightArray.Add(animals[mid + 1 + j]);

            int p = 0, q = 0, k = left;

            while (p < n1 && q < n2)
            {
                if (leftArray[p].Id <= rightArray[q].Id)
                {
                    animals[k] = leftArray[p];
                    p++;
                }
                else
                {
                    animals[k] = rightArray[q];
                    q++;
                }
                k++;
            }

            while (p < n1)
            {
                animals[k] = leftArray[p];
                p++;
                k++;
            }

            while (q < n2)
            {
                animals[k] = rightArray[q];
                q++;
                k++;
            }
        }
    }
}
