using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects;
using System;

namespace AsteroidsPlus.Logic
{
	public class ScoreSystem
	{
		private int _score;
		public int Score
		{
			get
			{
				return _score;
			}

			private set
			{
				_score = value;
				ScoreChange?.Invoke(_score);
			}
		}

		public static Action<int> ScoreChange;

		public ScoreSystem( GameState gameState)
		{
			gameState.StateChange += OnStateChange;
			Asteroid.AsteroidDestroyed += OnAsteroidDestroyed;
			UFO.UFODestroyed += OnUFODestroyed;
		}

		private void OnStateChange(GameState.State state)
		{
			if (state == GameState.State.StartIdle || state == GameState.State.Playing) Score = 0;
		}

		private void OnAsteroidDestroyed()
		{
			Score += Data.Instance().Settings.ScoreAsteroidPriсe;
		}

		private void OnUFODestroyed()
		{
			Score += Data.Instance().Settings.ScoreAsteroidPriсe;
		}

	}
}
