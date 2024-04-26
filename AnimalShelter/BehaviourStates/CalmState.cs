using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.BehaviourStates
{
    public class CalmState : IBehaviourState
    {
        public void PerformBehaviour(Animal animal)
        {
            string pronoun = animal.Gender.ToLower() == "male" ? "him" : (animal.Gender.ToLower() == "female" ? "her" : "them");
            Console.WriteLine($"{animal.Name} lets you pet {pronoun}.");
        }
    }
}
