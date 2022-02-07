using UnityEngine;
using TMPro;
using AsteroidsPlus.Logic;

namespace AsteroidsPlus.UI
{
	public class ScoreDisplay : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		private void Awake()
		{
			ScoreSystem.ScoreChange += OnScoreChange;
		}

		private void OnScoreChange(int score)
		{
			_text.text = "Score: " + score.ToString();
		}
	}
}
