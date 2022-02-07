using AsteroidsPlus.ScripObject;
using UnityEngine;

namespace AsteroidsPlus.Core
{
	public class Data
	{
		private static Data _instance = null;

		public static Data Instance()
		{
			if (_instance == null) _instance = new Data();
			return _instance;
		}

		public SettingScriptableObject Settings { get; private set; }

		public void AddSettings(SettingScriptableObject setting)
		{
			Settings = setting;
		}

		private Rect _screenRectInWold = new Rect();

		public Rect ScreenRectInWold
		{
			get
			{
				if (_screenRectInWold == new Rect())
				{
					var camera = Camera.main;
					Vector2 downLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
					Vector2 upRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, 0));
					_screenRectInWold = new Rect(downLeft, upRight - downLeft);
				}
				return _screenRectInWold;
			}
		}

	}
}
