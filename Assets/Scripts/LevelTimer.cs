using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent((typeof(Text)))]
    public class LevelTimer : MonoBehaviour
    {
        private LevelController _levelController;
        private Image _watchHand;

        private float _rotationPerSecond;

        public void Awake()
        {
            _levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();      
            _rotationPerSecond = 360/_levelController.timeInSeconds;
            _watchHand = GetComponent<Image>();
        }

        public void Update()
        {
            _watchHand.GetComponent<RectTransform>().RotateAround(transform.position, Vector3.forward, -_rotationPerSecond * Time.deltaTime);
        }
    }
}
