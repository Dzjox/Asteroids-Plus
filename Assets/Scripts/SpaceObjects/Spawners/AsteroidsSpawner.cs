using AsteroidsPlus.Core;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects.Spawner
{
	public class AsteroidsSpawner
	{
		public delegate void Handler();
		public static event Handler DestoryAllAsteroids;

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
					.Launch(asteroidPostion);
			}
		}

		public void SpawnMini(Vector2 asteroidPostion, int asteroidsCount, float scale = 1)
		{
			for (int i = 0; i < asteroidsCount; i++)
			{
				var asteroid = Object.Instantiate(Data.Instance().Settings.AsteroidPrefab, asteroidPostion, Quaternion.identity);
				asteroid.GetComponent<Asteroid>().Launch(asteroidPostion);
				asteroid.transform.localScale *= scale;
				asteroid.tag = "MiniAsteroid";
			}
		}

		public void Clear()
		{
			DestoryAllAsteroids?.Invoke();
		}

	}
}
