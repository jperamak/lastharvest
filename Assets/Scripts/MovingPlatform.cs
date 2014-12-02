using UnityEngine;

namespace Assets.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector2 endPoint;
        public float speed = 1.0f;

        private Vector2 _startPoint;
        private bool _movingTowardsEndPoint = true;

        public void Awake()
        {
            _startPoint = transform.position;
        }

        public void Update()
        {
            var goal = _movingTowardsEndPoint ? endPoint : _startPoint;
            var direction = (goal - (Vector2) transform.position).normalized * speed * Time.deltaTime;
            transform.position += (Vector3)direction;
            if (_movingTowardsEndPoint && Vector3.Distance(transform.position, endPoint) < 0.5f)
            {
                _movingTowardsEndPoint = false;
            }
            else if (!_movingTowardsEndPoint && Vector3.Distance(transform.position, _startPoint) < 0.5f)
            {
                _movingTowardsEndPoint = true;
            }
        }
    }
}
