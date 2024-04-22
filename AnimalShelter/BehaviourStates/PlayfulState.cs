using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.BehaviourStates
{
    public class PlayfulState : IBehaviourState
    {
        public void PerformBehaviour(Animal animal)
        {
            Console.WriteLine($"{animal.Name} comes up to you and wants to play.");
        }
    }
}
