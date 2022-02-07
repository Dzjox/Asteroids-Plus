using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects;

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

		public delegate void ScoreHandler(int score);
		public static event ScoreHandler ScoreChange;

		public void Init()
		{
			RoundsLogic.ChangeStatus += OnChangeStatus;
			Asteroid.AsteroidDestroyed += OnAsteroidDestroyed;
			UFO.UFODestroyed += OnUFODestroyed;
		}

		private void OnChangeStatus(RoundsLogic.Status status)
		{
			if (status == RoundsLogic.Status.Playing || status == RoundsLogic.Status.Idle) Score = 0;
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
