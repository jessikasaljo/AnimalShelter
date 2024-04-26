using AnimalShelterProgram.AnimalClasses;
using AnimalShelterProgram.BehaviourStates;
using AnimalShelterProgram.Factories;
using AnimalShelterProgram.Shelters;
using AnimalShelterProgram.SortingStrategies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram
{
    public class AnimalShelterManager
    {
        //Shelters
        CityShelter cityShelter;
        ForestShelter forestShelter;
        AnimalShelter currentShelter;
        public MainMenu mainMenu;


        //Dictionaries
        Dictionary<string, AnimalFactory> currentFactories;
        Dictionary<string, List<Animal>> currentAnimalLists;


        //Constructor
        AnimalShelterManager()
        {
            currentFactories = new Dictionary<string, AnimalFactory>();
            currentAnimalLists = new Dictionary<string, List<Animal>>();
        }


        //Singleton
        static AnimalShelterManager instance;

        public static AnimalShelterManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AnimalShelterManager();
                instance.Initialize();
            }
            return instance;
        }


        //Separate method for initializing shelters etc, to avoid infinite loop
        void Initialize() 
        {
            cityShelter = CityShelter.GetInstance();
            forestShelter = ForestShelter.GetInstance();
        }


        //Runs the manager
        public void RunManager()
        {
            currentShelter = mainMenu.currentShelter;
            currentFactories = mainMenu.currentFactories;
            string shelterType = currentShelter.Type;
            Console.WriteLine($"Welcome to the {shelterType} Shelter!\r\nWhat would you like to do?");



            while (true)
            {
                int userInput = GetValidIntegerInput("\r\n1. Meet the animals\r\n" +
                                                    "2. Adopt an animal\r\n" +
                                                    "3. Surrender an animal\r\n" +
                                                    "4. Sort animals\r\n" +
                                                    "5. Switch shelters\r\n" +
                                                    "9. Close application\r\n" +
                                                    "\r\nEnter your choice: ");

                Console.Clear();
                switch (userInput)
                {
                    case 1:
                        DisplayAnimalsBySpecies();
                        MeetAnimal();
                        break;
                    case 2:
                        AdoptAnimal();
                        break;
                    case 3:
                        AddAnimal();
                        break;
                    case 4:
                        SortAnimals();
                        break;
                    case 5:
                        SwitchShelter();
                        break;
                    case 9:
                        Console.WriteLine("Thank you for visiting our animal shelter!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unvalid input. Please enter a valid number.");
                        continue;
                }
            }
        }


        //Return to main menu
        void BackToMainMenu()
        {
            Console.WriteLine("\r\nPress enter to return to main menu.");
            Console.ReadLine();
            Console.Clear();
            RunManager();
        }


        //Choose animal to interact with
        void MeetAnimal()
        {
            List<Animal> currentShelterAnimals = GetCurrentAnimals();


            while (true)
            {
                int userInput = GetValidIntegerInput("Enter the ID number of the animal you are interested in: ");
                Animal animalToInteractWith = currentShelterAnimals.FirstOrDefault(animal => animal.Id == userInput);

                if (animalToInteractWith != null)
                {
                    animalToInteractWith.DisplayInfo();
                    Interact(animalToInteractWith);
                    break;
                }
                else
                {
                    Console.WriteLine("We have no animal with that ID. Please enter a valid ID number.\r\n");
                }
            }
        }


        //Interact with an animal
        void Interact(Animal animal)
        {
            string pronoun = animal.Gender.ToLower() == "male" ? "him" : (animal.Gender.ToLower() == "female" ? "her" : "them");

            while (true)
            {
                int userInput = GetValidIntegerInput($"How do you want to approach {animal.Name}?\r\n\r\n" +
                                     $"1. Say hello\r\n" +
                                     $"2. Insult {pronoun}\r\n" +
                                     $"3. Compliment {pronoun}\r\n" +
                                     $"4. Show {pronoun} a toy\r\n" +
                                     $"5. Sing {pronoun} a song\r\n" +
                                     "\r\nEnter your choice: ");

                IBehaviourState behaviourState = null;
                switch (userInput)
                {
                    case 1:
                        animal.SayHello(animal);
                        break;
                    case 2:
                        behaviourState = new AggressiveState();
                        break;
                    case 3:
                        behaviourState = new ShyState();
                        break;
                    case 4:
                        behaviourState = new PlayfulState();
                        break;
                    case 5:
                        behaviourState = new CalmState();
                        break;
                    default:
                        Console.Write("Unvalid input. Please enter a valid number: ");
                        continue;
                }
                if (userInput > 1 && userInput <= 5)
                {
                    animal.SetBehaviour(behaviourState);
                    behaviourState.PerformBehaviour(animal);
                }
                BackToMainMenu();
            }
        }


        //Displays the different lists, based on current shelter
        void DisplayAnimalsBySpecies()
        {
            string currentShelterType = mainMenu.currentShelter.Type;
            var filteredAnimalLists = currentAnimalLists.Where(kvp => kvp.Value.Any(animal => animal.Shelter == currentShelterType))
                                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Console.WriteLine($"These are all the animals in the {currentShelterType} Shelter's care:\r\n");

            foreach (var kvp in filteredAnimalLists)
            {
                string species = kvp.Key;
                List<Animal> animals = kvp.Value;

                Console.WriteLine($"Animals of species: {species}");
                foreach (Animal animal in animals.Where(animal_ => animal_.Shelter == currentShelterType))
                {
                    Console.WriteLine($"{animal.Name}, ID: {animal.Id}");
                }
                Console.WriteLine();
            }
        }


        //Surrender an animal to the shelter
        void AddAnimal()
        {
            string currentShelter = mainMenu.currentShelter.Type;

            var filteredAnimalLists = currentAnimalLists.Where(kvp => kvp.Value.Any(animal => animal.Shelter == currentShelter))
                                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Console.WriteLine("We accept the following species:\r\n");
            HashSet<string> printedSpecies = new HashSet<string>();

            foreach (var kvp in filteredAnimalLists)
            {
                string species = kvp.Key;
                List<Animal> animals = kvp.Value;

                if (!printedSpecies.Contains(species))
                {
                    Console.WriteLine(species);
                    printedSpecies.Add(species);
                }
            }

            string speciesInput = GetValidStringInput("\r\nEnter the type of animal you would like to surrender (in singular): ").ToLower();

            while (!filteredAnimalLists.ContainsKey(speciesInput))
            {
                speciesInput = GetValidStringInput("\r\nWe can't accept this type of animal. Please enter one of the accepted species in singular: ").ToLower();
            }

            while (true)
            {
                string nameInput = GetValidStringInput($"\r\nEnter a name for the {speciesInput}: ");
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                nameInput = textInfo.ToTitleCase(nameInput);

                int ageInput = GetValidIntegerInput($"Enter {nameInput}'s age in years: ");

                Console.Write($"Enter {nameInput}'s colour: ");
                string colourInput = GetValidStringInput($"Enter {nameInput}'s colour: ").ToLower();

                string genderInput = GetValidGenderInput($"Enter {nameInput}'s gender (non-binary, male or female): ");

                Animal newAnimal = currentFactories[speciesInput].GetCreatedAnimal(nameInput, ageInput, genderInput, colourInput);
                AddAnimalToList(speciesInput, newAnimal);
                Console.WriteLine($"\r\nYou have surrendered {nameInput} to the shelter.\r\n" +
                                    $"We will do our best to find {nameInput} a good home!");
                break;
            }
            BackToMainMenu();
        }


        //Add animal to list
        public void AddAnimalToList(string species, Animal animal)
        {
            if (!currentAnimalLists.ContainsKey(species))
            {
                currentAnimalLists[species] = new List<Animal>();
            }
            currentAnimalLists[species].Add(animal);
        }


        //Adopt an animal
        void AdoptAnimal()
        {
            List<Animal> currentShelterAnimals = GetCurrentAnimals();

            if (currentShelterAnimals == null || !currentShelterAnimals.Any())
            {
                Console.WriteLine("There are no animals to adopt.");
                BackToMainMenu();
                return;
            }
       
            Console.WriteLine("These are our adoptable animals:\r\n");

            foreach (Animal animal in currentShelterAnimals)
            {
                if (animal.IsAdopted == false)
                {
                    string species = animal.GetType().Name.ToLower();
                    Console.WriteLine($"{animal.Name}, {species}. ID: {animal.Id}");
                }
            }

            while (true)
            {
                int userInput = GetValidIntegerInput("\r\nEnter the ID of the animal you would like to adopt: ");
                Animal animalToAdopt = currentShelterAnimals.FirstOrDefault(animal => animal.Id == userInput);

                if (animalToAdopt != null)
                {
                    if (animalToAdopt.IsAdopted)
                    {
                        Console.Write($"\r\nSorry, {animalToAdopt.Name} is already adopted. Please enter the ID of another animal: ");
                    }
                    else
                    {
                        animalToAdopt.IsAdopted = true;
                        Console.WriteLine($"\r\nYou have adopted {animalToAdopt.Name}. Congratulations!");
                        break;
                    }
                }
                else
                {
                    Console.Write("We have no animal with that ID. Please enter a valid ID number.\r\n");
                }
            }
            BackToMainMenu();
        }


        //Switch shelters
        void SwitchShelter()
        {
            if (currentShelter is CityShelter)
            {
                UpdateCurrentShelter(forestShelter);
            }
            else if (currentShelter is ForestShelter)
            {
                UpdateCurrentShelter(cityShelter);
            }
            currentShelter.Run();
        }

        void UpdateCurrentShelter(AnimalShelter newShelter)
        {
            mainMenu.currentShelter = newShelter;
            currentShelter = mainMenu.currentShelter;
            mainMenu.currentFactories = newShelter.factories;
            currentFactories = mainMenu.currentFactories;
        }


        //Sorting code
        void SortAnimals()
        {
            List<Animal> currentShelterAnimals = GetCurrentAnimals();

            if (currentShelterAnimals == null || !currentShelterAnimals.Any())
            {
                Console.WriteLine("There are no animals to sort.");
                BackToMainMenu();
                return;
            }

            while (true)
            {
                int userInput = GetValidIntegerInput($"How do you want to sort the animals?\r\n" +
                                                     $"1. By name\r\n" +
                                                     $"2. By age\r\n" +
                                                     $"3. By ID\r\n");

                if (userInput >= 1 && userInput <= 3)
                {
                    ISortingStrategy sortingStrategy = null;
                    switch (userInput)
                    {
                        case 1:
                            sortingStrategy = new AlphabeticalStrategy();
                            break;
                        case 2:
                            sortingStrategy = new ByAgeStrategy();
                            break;
                        case 3:
                            sortingStrategy = new ByIDStrategy();
                            break;
                    }
                    sortingStrategy.Sort(currentShelterAnimals);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            BackToMainMenu();
        }


        //Get a list of all the animals in the current shelter
        List<Animal> GetCurrentAnimals()
        {
            string currentShelterType = mainMenu.currentShelter.Type;

            var filteredAnimalLists = currentAnimalLists.Where(kvp => kvp.Value.Any(animal => animal.Shelter == currentShelterType))
                                      .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            List<Animal> allAnimals = filteredAnimalLists.Values.SelectMany(animalList => animalList).ToList();
            return allAnimals;
        }


        //Get valid input from the user
        public int GetValidIntegerInput(string message)
        {
            int userInput;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        string GetValidStringInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string userInput = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please write something.");
                }
            }
        }

        string GetValidGenderInput(string message)
        {
            string[] validGenders = { "non-binary", "male", "female" };
            while (true)
            {
                Console.Write(message);
                string userInput = Console.ReadLine().ToLower();
                if (validGenders.Contains(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'non-binary', 'male', or 'female'.");
                }
            }
        }

    }
}
