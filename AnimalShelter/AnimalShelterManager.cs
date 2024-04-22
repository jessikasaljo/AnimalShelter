using AnimalShelterProgram.AnimalClasses;
using AnimalShelterProgram.BehaviourStates;
using AnimalShelterProgram.Factories;
using AnimalShelterProgram.Shelters;
using AnimalShelterProgram.SortingStrategies;
using System;
using System.Collections.Generic;
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
        public MainMenu mainMenu;


        //Dictionaries
        Dictionary<string, AnimalFactory> factories;
        Dictionary<string, List<Animal>> animalLists;


        // Constructor
        AnimalShelterManager()
        {
            factories = new Dictionary<string, AnimalFactory>();
            animalLists = new Dictionary<string, List<Animal>>();
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
            AnimalShelter currentShelter = mainMenu.currentShelter;
            string shelterType = currentShelter.Type;
            Console.WriteLine($"Welcome to the {shelterType} Shelter!\r\nWhat would you like to do?\r\n");
            Console.Write("1. Meet the animals\r\n" +
                          "2. Adopt an animal\r\n" +
                          "3. Surrender an animal\r\n" +
                          "4. Sort animals\r\n" +
                          "5. Switch shelters\r\n" +
                          "9. Close application\r\n" +
                          "\r\nEnter your choice: ");

            while (true)
            {
                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (userInput)
                    {
                        case 1:
                            AnimalsBySpecies();
                            MoreInfoMenu();
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
                            if (mainMenu.currentShelter is CityShelter)
                            {
                                currentShelter = forestShelter;
                            }
                            else if (mainMenu.currentShelter is ForestShelter)
                            {
                                currentShelter = cityShelter;
                            }
                            currentShelter.Run();
                            break;
                        case 9:
                            Console.WriteLine("Thank you for visiting our animal shelter!");
                            Environment.Exit(0);
                            break;
                    }
                }
                catch
                {
                    Console.Write("Unvalid input. Please enter a valid number: ");
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


        //Interaction menu
        void MoreInfoMenu()
        {
            Console.Write("Enter the ID number of the animal you would like to know more about: ");
            while (true)
            {
                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    Animal animalToInteractWith = animalLists.Values.SelectMany(animalList => animalList).FirstOrDefault(animal => animal.Id == userInput);

                    if (animalToInteractWith != null)
                    {
                        animalToInteractWith.DisplayInfo();
                        Interact(animalToInteractWith);
                    }
                    else
                    {
                        Console.Write("Sorry, we have no animal with that ID. Please enter a valid ID number: ");
                    }
                }
                catch
                {
                    Console.Write("Unvalid input. Please enter a valid ID number: ");
                }
            }
        }


        //Functions for showing the different lists, based on current shelter
        void AnimalsBySpecies()
        {
            string currentShelter = mainMenu.currentShelter.Type;
            Console.WriteLine($"These are all the animals in the {currentShelter} Shelter's care:\r\n");

            var filteredAnimalLists = animalLists.Where(kvp => kvp.Value.Any(animal => animal.Shelter == currentShelter))
                                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var kvp in filteredAnimalLists)
            {
                string species = kvp.Key;
                List<Animal> animals = kvp.Value;

                Console.WriteLine($"Animals of species: {species}");
                foreach (Animal animal in animals.Where(animal_ => animal_.Shelter == currentShelter))
                {
                    Console.WriteLine($"{animal.Name}, ID: {animal.Id}");
                }
                Console.WriteLine();
            }
        }


        //Add new animal ÄNDRA DENNA SÅ DEN SKRIVER UT TYPERNA DYNAMISKT. BEROENDE PÅ SHELTER OCKSÅ
        void AddAnimal()
        {
            string currentShelter = mainMenu.currentShelter.Type;

            var filteredAnimalLists = animalLists.Where(kvp => kvp.Value.Any(animal => animal.Shelter == currentShelter))
                                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Console.WriteLine("We can only accept the animal you've brought if it is of one of the following species:\r\n");
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

            Console.Write("\r\nEnter the type of animal you would like to surrender (in singular): ");
            string speciesInput = Console.ReadLine().ToLower();

            while (!filteredAnimalLists.ContainsKey(speciesInput))
            { 
                    Console.Write("\r\nWe can't accept this type of animal. Please enter one of the accepted species in singular: ");
                    speciesInput = Console.ReadLine().ToLower();
            }

            while (true)
            {
                try
                {
                    Console.Write($"\r\nEnter a name for the {speciesInput}: ");
                    string nameInput = Console.ReadLine();
                    Console.Write($"Enter {nameInput}'s age in years: ");
                    int ageInput;

                    while (true)
                    {
                        try
                        {
                            ageInput = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.Write("\r\nUnvalid input, please enter a number (integer): ");
                        }

                    }

                    while (ageInput < 0)
                    {
                        Console.Write("\r\nUnvalid input, please enter a valid non-negative number for age: ");
                        ageInput = Convert.ToInt32(Console.ReadLine());
                    }

                    Console.Write($"Enter {nameInput}'s colour: ");
                    string colourInput = Console.ReadLine().ToLower();
                    Console.Write($"Enter {nameInput}'s gender (non-binary, male or female): ");
                    string genderInput = Console.ReadLine().ToLower();

                    if (genderInput == "non-binary" || genderInput == "male" || genderInput == "female")
                    {
                        Animal newAnimal = factories[speciesInput].GetCreatedAnimal(nameInput, ageInput, genderInput, colourInput);
                        AddAnimalToList(speciesInput, newAnimal);
                        Console.WriteLine($"\r\nYou have surrendered {nameInput} to the shelter.\r\n" +
                                          $"We will do our best to find {nameInput} a good home!\r\n");
                        BackToMainMenu();
                        break;
                    }
                    else
                    {
                        while (genderInput != "non-binary" && genderInput != "male" && genderInput != "female")
                        {
                            Console.Write("\r\nUnvalid gender, please enter one of the given genders: ");
                            genderInput = Console.ReadLine().ToLower();
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"Unvalid input, please enter one of the accepted species: ");
                    continue;
                }
            }
        }


        //Add animal to list
        public void AddAnimalToList(string species, Animal animal)
        {
            if (!animalLists.ContainsKey(species))
            {
                animalLists[species] = new List<Animal>();
            }
            animalLists[species].Add(animal);
        }


        //Adopt an animal
        void AdoptAnimal()
        {
            Console.WriteLine("These are our adoptable animals:\r\n");
            foreach (List<Animal> list in animalLists.Values)
            {
                foreach (Animal animal in list)
                {
                    if (animal.IsAdopted == false)
                    {
                        string species = animal.GetType().Name.ToLower();
                        Console.WriteLine($"{animal.Name}, {species}. ID: {animal.Id}");
                    }
                }
            }

            Console.Write("\r\nEnter the ID of the animal you would like to adopt: ");
            while (true)
            {
                try
                {
                    int animalToAdoptId = Convert.ToInt32(Console.ReadLine());
                    Animal animalToAdopt = animalLists.Values.SelectMany(animalList => animalList).FirstOrDefault(animal => animal.Id == animalToAdoptId);

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
                        Console.Write("Sorry, we have no animal with that ID. Please enter a valid ID number: ");
                    }
                }
                catch
                {
                    Console.Write("Unvalid input. Please enter a valid ID number: ");
                }
            }
            BackToMainMenu();
        }


        //Interact with an animal
        void Interact (Animal animal)
        {
            Console.Write($"How do you want to approach {animal.Name}?\r\n\r\n" +
                          $"1. Say hello\r\n" +
                          $"2. Insult them\r\n" +
                          $"3. Compliment them\r\n" +
                          $"4. Show them a toy\r\n" +
                          $"5. Sing a song to them\r\n" +
                          "\r\nEnter your choice: ");

            while (true)
            {
                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
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
                    if (userInput != 1)
                    {
                        animal.SetBehaviour(behaviourState);
                        behaviourState.PerformBehaviour(animal);
                    }
                    BackToMainMenu();
                }
                catch
                {
                    Console.Write("Unvalid input. Please enter a valid number: ");
                }
            }
        }

        void SortAnimals()
        {
            List<Animal> allAnimals = animalLists.Values.SelectMany(animalList => animalList).ToList();

            Console.WriteLine($"How do you want to sort the animals?\r\n" +
                  $"1. By name\r\n" +
                  $"2. By age\r\n" +
                  $"3. By ID\r\n");

            while (true)
            {
                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    ISortingStrategy sortingStrategy = null;
                    Console.Clear();
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
                        default:
                            Console.Write("Unvalid input. Please enter a valid number: ");
                            continue;
                    }
                    sortingStrategy.Sort(allAnimals);
                    BackToMainMenu();
                    break;
                }
                catch
                {
                    Console.Write("Unvalid input. Please enter a valid number: ");
                    continue;
                }
            }
        }
    }
}
