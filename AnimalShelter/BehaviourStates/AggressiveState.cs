using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.BehaviourStates
{
    public class AggressiveState : IBehaviourState
    {
        public void PerformBehaviour(Animal animal)
        {
            Console.WriteLine($"{animal.Name} attacks you!");
        }
    }
}
