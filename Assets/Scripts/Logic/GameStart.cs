using AsteroidsPlus.Core;
using AsteroidsPlus.ScripObject;
using UnityEngine;


namespace AsteroidsPlus.Logic
{
	public class GameStart : MonoBehaviour
	{
		[SerializeField] private SettingScriptableObject _settings = null;

		private void Awake()
		{
			Data.Instance().AddSettings(_settings);
			new RoundsLogic();
			new ScoreSystem();
		}
	}
}
