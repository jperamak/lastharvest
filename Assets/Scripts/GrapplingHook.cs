using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class GrapplingHook : MonoBehaviour
    {
        private Vector3 _velocity;

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        private Transform _player;

        private LineRenderer line;	
        private bool _rope = true;	

        public void Awake()
        {
            line = gameObject.GetComponent<LineRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void FixedUpdate()
        {
            transform.position += _velocity;
        }

        public void LateUpdate()
        {
            if (_rope)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, _player.transform.position);

                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
        }

        public void Connect(HookConnector connector)
        {
            _velocity = Vector3.zero;
            _player.GetComponent<PlayerInput>().Grapple(connector.transform);
        }
    }
}
