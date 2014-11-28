using UnityEngine;

namespace Assets.Scripts
{
    public class Geyser : MonoBehaviour
    {
        public Vector2 size = new Vector2(2, 2);
        public float duration = 1.0f;
	    public float speed = 5.0f;

        public float intervalInSeconds = 5.0f;
        
		private float _currentTime;
        private bool _isSprouting;
        private float _sproutingTime;
        private float _timeToFullSize;

        private Vector2 _originalScale;

        public void Awake()
        {
            if (duration > intervalInSeconds)
            {
                Debug.LogError("Duration has to be shorter than interval");
            }
	        if (speed < 2.0f)
	        {
		        Debug.Log("Speed has to be greater than 2");
	        }
            _timeToFullSize = duration/speed;
            _originalScale = transform.localScale;
        }

        public void Update()
        {
            if (_currentTime >= intervalInSeconds)
            {
                _isSprouting = true;
                _currentTime = 0.0f;
            }

            if (_isSprouting)
            {
                float xSize, ySize;
                if (_sproutingTime < _timeToFullSize)
                {
                    xSize = Mathf.Lerp(_originalScale.x, size.x, _sproutingTime / _timeToFullSize);
                    ySize = Mathf.Lerp(_originalScale.y, size.y, _sproutingTime / _timeToFullSize);
                    transform.position = transform.position + new Vector3((xSize - transform.localScale.x) / 2.0f, (ySize - transform.localScale.y) / 2.0f);
					transform.localScale = new Vector3(xSize, ySize, 1);
                }
                else if (_sproutingTime > duration - _timeToFullSize)
                {
					xSize = Mathf.Lerp(size.x, _originalScale.x, (_sproutingTime - (duration - _timeToFullSize)) / _timeToFullSize);
					ySize = Mathf.Lerp(size.y, _originalScale.y, (_sproutingTime - (duration - _timeToFullSize)) / _timeToFullSize);
                    transform.position = transform.position + new Vector3((xSize - transform.localScale.x) / 2.0f, (ySize - transform.localScale.y) / 2.0f);
					transform.localScale = new Vector3(xSize, ySize, 1);
                }
                
                _sproutingTime += Time.deltaTime;
                if (_sproutingTime >= duration)
                {
                    _sproutingTime = 0.0f;
                    _isSprouting = false;
                }
            }

            _currentTime += Time.deltaTime;
        }

    }
}
