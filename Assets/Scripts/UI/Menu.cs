using AsteroidsPlus.Logic;
using UnityEngine;

namespace AsteroidsPlus.UI
{
	public class Menu : MonoBehaviour
	{
		[SerializeField] private GameObject _content;

		private void Awake()
		{
			RoundsLogic.ChangeStatus += OnChangeStatus;
		}

		private void OnChangeStatus(RoundsLogic.Status status)
		{
			switch (status)
			{
				case RoundsLogic.Status.Idle:
				case RoundsLogic.Status.ShipDestroyed:
					_content.SetActive(true);
					break;
				default:
				case RoundsLogic.Status.Playing:
					_content.SetActive(false);
					break;
			}
		}
	}
}
