using System.Globalization;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    [RequireComponent(typeof(Text))]
    public class ScoreCounter : MonoBehaviour
    {
        private GameController _gameController;
        private Text _scoreText;

        public void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
            _scoreText = GetComponent<Text>();
        }

        public void Update()
        {
            _scoreText.text = _gameController.Score.ToString(CultureInfo.InvariantCulture);
        }
    }
}
