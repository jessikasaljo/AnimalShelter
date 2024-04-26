using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram
{
    public class ByAgeStrategy : ISortingStrategy
    {
        //Merge sort by age
        public void Sort(List<Animal> animals)
        {
            if (animals == null || animals.Count <= 1)
                return;

            MergeSort(animals, 0, animals.Count - 1);
            DisplaySortedAnimals(animals);
        }


        public void DisplaySortedAnimals(List<Animal> animals)
        {
            Console.Clear();
            Console.WriteLine("Our animals sorted by age:\r\n");
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

                MergeByAge(animals, left, mid, right);
            }
        }


        void MergeByAge(List<Animal> animals, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            List<Animal> leftArray = new List<Animal>();
            List<Animal> rightArray = new List<Animal>();

            for (int i = 0; i < n1; ++i)
            {
                leftArray.Add(animals[left + i]);
            }

            for (int j = 0; j < n2; ++j)
            {
                rightArray.Add(animals[mid + 1 + j]);
            }

            int iLeft = 0, iRight = 0;
            int k = left;

            while (iLeft < n1 && iRight < n2)
            {
                if (leftArray[iLeft].Age <= rightArray[iRight].Age)
                {
                    animals[k] = leftArray[iLeft];
                    iLeft++;
                }
                else
                {
                    animals[k] = rightArray[iRight];
                    iRight++;
                }
                k++;
            }

            while (iLeft < n1)
            {
                animals[k] = leftArray[iLeft];
                iLeft++;
                k++;
            }

            while (iRight < n2)
            {
                animals[k] = rightArray[iRight];
                iRight++;
                k++;
            }
        }
    }
}
