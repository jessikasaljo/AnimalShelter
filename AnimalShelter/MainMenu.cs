using AnimalShelterProgram.Factories;
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
        public Dictionary<string, AnimalFactory> currentFactories;


        //Constructor
        public MainMenu()
        {
            shelterManager = AnimalShelterManager.GetInstance();
            cityShelter = CityShelter.GetInstance();
            forestShelter = ForestShelter.GetInstance();
            currentShelter = cityShelter;
            currentFactories = cityShelter.factories;
            shelterManager.mainMenu = this;
        }


        //Runs the program
        public void Run()
        {
            while (true)
            {
                int userInput = shelterManager.GetValidIntegerInput("Which shelter would you like to visit?\r\n" +
                                                                    "1. City shelter\r\n" +
                                                                    "2. Forest shelter\r\n" +
                                                                    "\r\nEnter your choice: ");

                Console.Clear();
                switch (userInput)
                {
                    case 1:
                        currentShelter = cityShelter;
                        currentFactories = cityShelter.factories;
                        break;
                    case 2:
                        currentShelter = forestShelter;
                        currentFactories = forestShelter.factories;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid number.\r\n");
                        continue;
                }
                currentShelter.Run();
                return;
            }
        }
    }
}
