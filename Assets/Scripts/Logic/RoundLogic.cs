using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects;
using AsteroidsPlus.SpaceObjects.Spawner;
using UnityEngine;

namespace AsteroidsPlus.Logic
{
	public class RoundsLogic
	{
		private AsteroidsSpawner _asteridsSpawner;
		private UFOSpawner _UFOSpawner;
		private Transform _playerTransform;

		public Ship PlayerShip;

		public RoundsLogic(GameState gameState)
		{
			_asteridsSpawner = new AsteroidsSpawner();
			_UFOSpawner = new UFOSpawner();

			gameState.StateChange += OnGameStateChange;

			_asteridsSpawner.Spawn(Vector2.zero, Data.Instance().Settings.AsteroidsInFirstRound);
		}

		private void OnGameStateChange(GameState.State state)
		{
			switch (state)
			{
				case GameState.State.StartIdle:
					StartIdle();
					break;
				case GameState.State.Playing:
					Playing();
					break;
				case GameState.State.RoundEnd:
					RoundEnd();
					break;
			}
		}

		private void StartIdle()
		{
			_asteridsSpawner.Spawn(Vector2.zero, Data.Instance().Settings.AsteroidsInIdle);
		}

		private void Playing()
		{
			_asteridsSpawner.Clear();
			_UFOSpawner.Clear();

			PlayerShip = GameObject.Instantiate(Data.Instance().Settings.ShipPrefab).GetComponent<Ship>();
			_playerTransform = PlayerShip.transform;

			_asteridsSpawner.SpawnInTime(_playerTransform);
			_UFOSpawner.SpawnInTime(_playerTransform);
		}

		private void RoundEnd()
		{
			_asteridsSpawner.StopSpawnInTime();
			_UFOSpawner.StopSpawnInTime();
		}
	}
}
