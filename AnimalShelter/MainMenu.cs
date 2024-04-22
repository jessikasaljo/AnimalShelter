using AnimalShelterProgram.Shelters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram
{
    public class MainMenu
    {
        //Shelters
        AnimalShelterManager shelterManager;
        CityShelter cityShelter;
        ForestShelter forestShelter;
        public AnimalShelter currentShelter;


        public MainMenu()
        {
            shelterManager = AnimalShelterManager.GetInstance();
            cityShelter = CityShelter.GetInstance();
            forestShelter = ForestShelter.GetInstance();
            currentShelter = cityShelter;
            shelterManager.mainMenu = this;
        }


        public void Run()
        {
            while (true)
            {
                Console.Write("Which shelter would you like to visit?\r\n" +
                              "1. City shelter\r\n" +
                              "2. Forest shelter\r\n" +
                              "\r\nEnter your choice: ");

                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (userInput)
                    {
                        case 1:
                            currentShelter = cityShelter;
                            break;
                        case 2:
                            currentShelter = forestShelter;
                            break;
                        default:
                            Console.Write("Invalid input. Please enter a valid number: ");
                            continue;
                    }
                    currentShelter.Run();
                    return;
                }
                catch
                {
                    Console.Write("Invalid input. Please enter a valid number: ");
                    continue;
                }
            }
        }
    }
}
