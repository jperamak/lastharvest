using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector2 endPoint;
        public float speed = 1.0f;

        private Vector2 _startPoint;
        private bool _movingTowardsEndPoint = true;

        private readonly List<GameObject> _gameObjectsOnPlatform = new List<GameObject>();

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

            _gameObjectsOnPlatform.ForEach(go => go.Do(o => o.transform.position += (Vector3)direction));
        }

        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            _gameObjectsOnPlatform.Add(collision2D.gameObject);   
        }

        public void OnCollisionExit2D(Collision2D collision2D)
        {
            _gameObjectsOnPlatform.Remove(collision2D.gameObject);
        }
    }
}
