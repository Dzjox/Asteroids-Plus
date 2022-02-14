using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects;
using System;

namespace AsteroidsPlus.Logic
{
    public class GameState
    {
		public enum State
		{
			StartIdle,
			Playing,
			RoundEnd
		}

		private InputSystem _inputSystem;
		private State _currentState;
		private State CurrentState 
		{
			get { return _currentState; }
			set
			{
				_currentState = value;
				StateChange?.Invoke(value);
			}
		}

		public Action<State> StateChange;

		public GameState(InputSystem inputSystem)
		{
			_inputSystem = inputSystem;
			Ship.ShipDestroyed += OnShipDestroyed;
		}

		public void StartIdle()
		{
			CurrentState = State.StartIdle;
			_inputSystem.Menu.PressAnyKey.performed += OnPressAnyKey;
		}

		private void OnPressAnyKey(UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			_inputSystem.Menu.PressAnyKey.performed -= OnPressAnyKey;
			CurrentState = State.Playing;
		}

		private void OnShipDestroyed()
		{
			if (_currentState == State.Playing)
			{
				CurrentState = State.RoundEnd;
				_inputSystem.Menu.PressAnyKey.performed += OnPressAnyKey;
			}
		}
	}
}
