using AsteroidsPlus.Core;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

namespace AsteroidsPlus.SpaceObjects.Spawner
{
	public class AsteroidsSpawner
	{
		public Action _destoryAllAsteroids;
		private bool _spawnAseroids;

		public void Spawn(Vector2 shipPostion, int asteroidsCount)
		{
			var minX = Data.Instance().Settings.AsteroidsSafeZoneRadius;
			var maxX = Data.Instance().ScreenRectInWold.width - minX;

			var minY = Data.Instance().Settings.AsteroidsSafeZoneRadius;
			var maxY = Data.Instance().ScreenRectInWold.width - minY;

			for (int i = 0; i < asteroidsCount; i++)
			{
				var asteroidPostion = new Vector3(
					Random.Range(minX, maxX) + shipPostion.x,
					Random.Range(minY, maxY) + shipPostion.y,
					0);

					GameObject.Instantiate(Data.Instance().Settings.AsteroidPrefab, asteroidPostion, Quaternion.identity)
					.GetComponent<Asteroid>()
					.Launch(asteroidPostion, this);
			}
		}

		public async void SpawnInTime(Transform player)
		{
			_spawnAseroids = true;
			int count = 0;
			int asteroidsCount = 0;
			var data = Data.Instance().Settings;
			while (_spawnAseroids && player!=null)
			{
				asteroidsCount = data.AsteroidsInFirstRound + (int)(count * data.AsteroidsCountWithTime);
				Spawn(player.position, asteroidsCount);
				count++;				
				await Task.Delay(TimeSpan.FromSeconds(data.AsteroidsArrivalTime));
			}
		}

		public void StopSpawnInTime ()
		{
			_spawnAseroids = false;
		}

		public void SpawnMini(Vector2 asteroidPostion, int asteroidsCount, AsteroidsSpawner asteroidsSpawner, float scale = 0.3f)
		{
			for (int i = 0; i < asteroidsCount; i++)
			{
				var asteroid = GameObject.Instantiate(Data.Instance().Settings.AsteroidPrefab, asteroidPostion, Quaternion.identity);
				asteroid.GetComponent<Asteroid>().Launch(asteroidPostion , asteroidsSpawner);
				asteroid.transform.localScale *= scale;
				asteroid.tag = "MiniAsteroid";
			}
		}

		public void Clear()
		{
			_destoryAllAsteroids?.Invoke();
		}
	}
}
