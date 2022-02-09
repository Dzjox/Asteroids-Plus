using AsteroidsPlus.Core;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

namespace AsteroidsPlus.SpaceObjects.Spawner
{
	public class UFOSpawner
	{
		public static Action DestoryAllUFO;

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

			Object.Instantiate(Data.Instance().Settings.UFOPrefab, UFOPostion, Quaternion.identity)
				.GetComponent<UFO>()
				.Launch(UFOPostion, player);

		}

		public void Clear()
		{
			DestoryAllUFO?.Invoke();
		}
	}
}
