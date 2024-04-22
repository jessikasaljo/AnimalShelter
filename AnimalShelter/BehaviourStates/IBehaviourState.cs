using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterProgram.BehaviourStates
{
    public interface IBehaviourState
    {
        void PerformBehaviour(Animal animal);
    }
}
