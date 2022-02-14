using UnityEngine;
using AsteroidsPlus.Logic;
using System.Threading.Tasks;

namespace AsteroidsPlus.UI
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private GameStart _gameStart;
		[SerializeField] private GameObject _menu;
		[SerializeField] private GameObject _playingUI;
		[SerializeField] private ShipSpecificationsDisplay _shipSpecificationsDisplay;

		private void Awake()
		{
			TryGetValues();
		}

		private async void TryGetValues()
		{
			while (_gameStart.GameState == null 
				&& _gameStart.RoundsLogic == null)
			{
				await Task.Delay(1);
			}
			_gameStart.GameState.StateChange += OnGameStateChange;
			_shipSpecificationsDisplay.SetRoundsLogic(_gameStart.RoundsLogic);
		}

		private void OnGameStateChange(GameState.State state)
		{
			switch (state)
			{
				case GameState.State.StartIdle:
					StartIdle();
					break;
				case GameState.State.Playing:
					Playing();
					break;
				case GameState.State.RoundEnd:
					RoundEnd();
					break;
			}
		}

		private void StartIdle()
		{
			_menu.SetActive(true);
			_playingUI.SetActive(false);
		}

		private void Playing()
		{
			_menu.SetActive(false);
			_playingUI.SetActive(true);
		}

		private void RoundEnd()
		{
			_menu.SetActive(true);
			_playingUI.SetActive(false);
		}

	}
}
