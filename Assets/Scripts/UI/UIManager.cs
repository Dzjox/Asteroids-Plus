using System;
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

		private void Awake()
		{
			TryGetGameStatusAtion();
		}

		private async void TryGetGameStatusAtion()
		{
			while (_gameStart.GameState == null)
			{
				await Task.Delay(1);
			}
			_gameStart.GameState.StateChange += OnGameStateChange;
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
