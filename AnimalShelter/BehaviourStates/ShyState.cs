using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.BehaviourStates
{
    public class ShyState : IBehaviourState
    {
        public void PerformBehaviour(Animal animal)
        {
            Console.WriteLine($"{animal.Name} is embarrassed and hides under a table.");
        }
    }
}
