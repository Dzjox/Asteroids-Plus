using AsteroidsPlus.Core;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;


namespace AsteroidsPlus.SpaceObjects.Spawner
{
	public class AsteroidsSpawner
	{
		private Action _destoryAllAsteroids;

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

					Object.Instantiate(Data.Instance().Settings.AsteroidPrefab, asteroidPostion, Quaternion.identity)
					.GetComponent<Asteroid>()
					.Launch(asteroidPostion, _destoryAllAsteroids);
			}
		}

		public void SpawnMini(Vector2 asteroidPostion, int asteroidsCount, Action destoryAllAsteroids, float scale = 0.3f)
		{
			for (int i = 0; i < asteroidsCount; i++)
			{
				var asteroid = Object.Instantiate(Data.Instance().Settings.AsteroidPrefab, asteroidPostion, Quaternion.identity);
				asteroid.GetComponent<Asteroid>().Launch(asteroidPostion , destoryAllAsteroids);
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
