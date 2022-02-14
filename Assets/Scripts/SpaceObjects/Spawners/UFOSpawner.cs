using AsteroidsPlus.Core;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

namespace AsteroidsPlus.SpaceObjects.Spawner
{
	public class UFOSpawner
	{
		public Action DestoryAllUFO;
		private bool _spawnUFO;

		public void Spawn(Transform player)
		{
			var minX = Data.Instance().Settings.AsteroidsSafeZoneRadius;
			var maxX = Data.Instance().ScreenRectInWold.width - minX;

			var minY = Data.Instance().Settings.AsteroidsSafeZoneRadius;
			var maxY = Data.Instance().ScreenRectInWold.width - minY;

			var UFOPostion = new Vector3(
				Random.Range(minX, maxX) + player.position.x,
				Random.Range(minY, maxY) + player.position.y,
				0);

			GameObject.Instantiate(Data.Instance().Settings.UFOPrefab, UFOPostion, Quaternion.identity)
				.GetComponent<UFO>()
				.Launch(UFOPostion, player, this);

		}

		public async void SpawnInTime(Transform player)
		{
			_spawnUFO = true;

			while (_spawnUFO)
			{
				await Task.Delay(TimeSpan.FromSeconds(Data.Instance().Settings.UFOSpawnTime));
				if (_spawnUFO && player!=null)
					Spawn(player);
			}
		}

		public void StopSpawnInTime()
		{
			_spawnUFO = false;
		}

		public void Clear()
		{
			DestoryAllUFO?.Invoke();
		}
	}
}
