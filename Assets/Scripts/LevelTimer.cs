using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent((typeof(Text)))]
    public class LevelTimer : MonoBehaviour
    {
        private LevelController _levelController;
        private Text _timerTimeText;
        
        public void Awake()
        {
            _levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
            _timerTimeText = GetComponent<Text>();
        }

        public void Update()
        {
            _timerTimeText.text = _levelController.timeInSeconds.ToString("0.0");
        }
    }
}
