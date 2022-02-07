using AsteroidsPlus.Core;
using UnityEngine;

namespace AsteroidsPlus.SpaceObjects.Movement
{
	public class BorderTeleport
	{
		private SpaceObjectMovement _spaceObject;
		private Rect _screenRect;

		public BorderTeleport(SpaceObjectMovement spaceObject)
		{
			_spaceObject = spaceObject;
			_screenRect = Data.Instance().ScreenRectInWold;
		}

		public bool Check()
		{
			return !_screenRect.Contains(_spaceObject.Position);
		}

		public void Teleport()
		{
			if (_spaceObject.Position.y > _screenRect.yMin) _spaceObject.MoveWithVector(new Vector2(0, -_screenRect.height));
			else if (_spaceObject.Position.y < _screenRect.yMax) _spaceObject.MoveWithVector(new Vector2(0, _screenRect.height));

			if (_spaceObject.Position.x < _screenRect.xMin) _spaceObject.MoveWithVector(new Vector2(_screenRect.width, 0));
			else if (_spaceObject.Position.x > _screenRect.xMax) _spaceObject.MoveWithVector(new Vector2(-_screenRect.width, 0));
		}
	}
}
