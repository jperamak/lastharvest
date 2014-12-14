using System.Globalization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
        private bool end = false;

        public void Awake()
        {
            _gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
            _scoreText = GetComponent<Text>();
        }

        public void Update()
        {
            _scoreText.text = _gameController.Score.ToString(CultureInfo.InvariantCulture);        
            if ( !end && Application.loadedLevelName == "EndScreen" ) 
            {
                end = true;
                StartCoroutine(Restart());
            }
        }

        private IEnumerator Restart()
        {
            yield return new WaitForSeconds(10);
            Destroy(GameObject.FindGameObjectWithTag("GameController"));
            Application.LoadLevel("Start");
        }
    }
}
