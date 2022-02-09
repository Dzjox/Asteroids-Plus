using AsteroidsPlus.Core;
using AsteroidsPlus.SpaceObjects;
using AsteroidsPlus.SpaceObjects.Spawner;
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AsteroidsPlus.Logic
{
	public class RoundsLogic
	{
		public enum Status
		{
			Idle,
			Playing,
			ShipDestroyed
		}

		public static Action<Status> ChangeStatus;

		private InputSystem _inputActions;
		private AsteroidsSpawner _asteridsSpawner;
		private UFOSpawner _UFOSpawner;
		private Transform _player;
		private Coroutine _steroidsSpawnCoroutine;
		private Coroutine _UFOSpawnCoroutine;


		public void Init()
		{
			_inputActions = new InputSystem();
			_inputActions.Menu.Enable();
			_inputActions.Menu.PressAnyKey.performed += OnPressAnyKey;

			_asteridsSpawner = new AsteroidsSpawner();
			_asteridsSpawner.Spawn(Vector2.zero, Data.Instance().Settings.AsteroidsInFirstRound);

			_UFOSpawner = new UFOSpawner();

			ChangeStatus?.Invoke(Status.Idle);
		}


		private void OnPressAnyKey(UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			_inputActions.Menu.PressAnyKey.performed -= OnPressAnyKey;
			StartPlaying();
		}

		private void StartPlaying()
		{
			_asteridsSpawner.Clear();
			_UFOSpawner.Clear();
			SpawnPlayer();
			_asteridsSpawner.Spawn(_player.position, Data.Instance().Settings.AsteroidsInFirstRound);
			_steroidsSpawnCoroutine = Coroutiner.Instance.DoCoroutine(SpawnAsteroidsInTime());
			_UFOSpawnCoroutine = Coroutiner.Instance.DoCoroutine(SpawnUFOInTime());
			ChangeStatus?.Invoke(Status.Playing);
		}


		private void SpawnPlayer()
		{
			var ship = Object.Instantiate(Data.Instance().Settings.ShipPrefab).GetComponent<Ship>();
			_player = ship.transform;
			ship.ShipDestroyed += OnShipDestroyed;
		}

		private void OnShipDestroyed()
		{
			Coroutiner.Instance.StopDoCoroutine(_steroidsSpawnCoroutine);
			Coroutiner.Instance.StopDoCoroutine(_UFOSpawnCoroutine);
			_inputActions.Menu.PressAnyKey.performed += OnPressAnyKey;
			ChangeStatus?.Invoke(Status.ShipDestroyed);
		}

		private IEnumerator SpawnAsteroidsInTime()
		{
			int asteroidsCount = 20;
			do
			{
				yield return new WaitForSeconds(Data.Instance().Settings.AsteroidsArrivalTime);
				_asteridsSpawner.Spawn(_player.position, asteroidsCount / 10);
				asteroidsCount++;
			} while (true);
		}

		private IEnumerator SpawnUFOInTime()
		{
			do
			{
				yield return new WaitForSeconds(Data.Instance().Settings.UFOSpawnTime);
				_UFOSpawner.Spawn(_player);
			} while (true);
		}
	}
}
