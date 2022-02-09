using AsteroidsPlus.Core;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects.Movement
{
	public class UFOMovement : SpaceObjectMovement
	{
		private Vector2[] _ways = new Vector2[5];
		private Vector2 _beastWay;
		public UFOMovement(Vector2 position, float rotation, float speed) : base(position, rotation, speed) { }

		public void MoveFixedUpdate(Transform UFO, Transform player)
		{
			FindBestWayWithTeleport(player);
			_inertiaVector = _beastWay.normalized * _speed * Time.fixedDeltaTime;
			base.MoveFixedUpdate(UFO);
		}

		private void FindBestWayWithTeleport(Transform player)
		{
			if (player == null) return;

			_beastWay = new Vector2(float.MaxValue, float.MaxValue);

			Vector2 playerPostion = player.position;

			_ways[0] = playerPostion - Position;
			_ways[1] = playerPostion - Position + new Vector2(0, 1) * Data.Instance().ScreenRectInWold.height;
			_ways[2] = playerPostion - Position - new Vector2(0, 1) * Data.Instance().ScreenRectInWold.height;
			_ways[3] = playerPostion - Position + new Vector2(1, 0) * Data.Instance().ScreenRectInWold.width;
			_ways[4] = playerPostion - Position - new Vector2(1, 0) * Data.Instance().ScreenRectInWold.width;

			for (int i = 0; i < _ways.Length; i++)
			{
				if (_beastWay.sqrMagnitude > _ways[i].sqrMagnitude) _beastWay = _ways[i];
			}
		}
	}
}
