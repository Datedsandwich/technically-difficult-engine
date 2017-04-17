using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicallyDifficult.Entities.EntityComponents
{
    public class StateMachine : EntityComponent
    {
        // First attempt at state machine. 
        // State(s) are an enum, so we can switch between states but the states can't store behaviour, which seems wrong to me. - Luke
        // Also, it doesn't work. Transitions always throw invalid exceptions.
        // ---------------------------------------------------------------------
        Dictionary<StateTransition, State> transitions;
        public State CurrentState { get; private set; }
        // ---------------------------------------------------------------------
        public enum State
        {
            Idle,
            Running,
            Interacting,
            Jumping
        }
        // ---------------------------------------------------------------------
        public enum Command
        {
            Run,
            Interact,
            Jump,
            Stop
        }
        // ---------------------------------------------------------------------
        class StateTransition
        {
            // This class is used as the Key for the Dictionary transitions.
            readonly State CurrentState;
            readonly Command Command;

            public StateTransition(State _currentState, Command _command)
            {
                CurrentState = _currentState;
                Command = _command;
            }
            // Equality of keys is important, so implementations of GetHashCode and Equals have been overridden.
            // Dictionary uses HashCode, so two equal objects (State and Transition) must return the same hash code,
            // or when it comes to run, we'll get invalid transition exceptions.
            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }
        // ---------------------------------------------------------------------
        public State GetNext(Command command)
        {
            StateTransition transition = new StateTransition(CurrentState, command);
            State nextState;
            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception("Invalid transition: " + CurrentState + " -> " + command);
            return nextState;
        }
        // ---------------------------------------------------------------------
        public State MoveNext(Command command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }
        // ---------------------------------------------------------------------
        public StateMachine()
        {
            CurrentState = State.Idle;
            transitions = new Dictionary<StateTransition, State>
            {
                { new StateTransition(State.Idle, Command.Run), State.Running },
                { new StateTransition(State.Idle, Command.Interact), State.Interacting },
                { new StateTransition(State.Idle, Command.Jump), State.Jumping },
                { new StateTransition(State.Running, Command.Interact), State.Interacting },
                { new StateTransition(State.Running, Command.Jump), State.Jumping },
                { new StateTransition(State.Running, Command.Stop), State.Idle },
                { new StateTransition(State.Jumping, Command.Run), State.Running },
                { new StateTransition(State.Jumping, Command.Stop), State.Idle },
                { new StateTransition(State.Interacting, Command.Run), State.Running },
                { new StateTransition(State.Interacting, Command.Jump), State.Jumping },
                { new StateTransition(State.Interacting, Command.Stop), State.Idle }
            };
        }
    }
}
