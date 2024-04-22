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
            Console.WriteLine($"{animal.Name} lets you pet them.");
        }
    }
}
