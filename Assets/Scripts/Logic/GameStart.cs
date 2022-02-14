using AsteroidsPlus.Core;
using AsteroidsPlus.ScripObject;
using UnityEngine;
namespace AsteroidsPlus.Logic
{
	public class GameStart : MonoBehaviour
	{
		[SerializeField] private SettingScriptableObject _settings = null;

		private InputSystem _inputSystem;
		private RoundsLogic _roundsLogic;
		public GameState GameState { get; private set; }

		private void Awake()
		{
			Data.Instance().AddSettings(_settings);

			_inputSystem = new InputSystem();
			_inputSystem.Menu.Enable();

			GameState = new GameState(_inputSystem);
			_roundsLogic = new RoundsLogic(GameState);

			new ScoreSystem();
		}

		private void Start()
		{
			GameState.StartIdle();
		}
	}
}
