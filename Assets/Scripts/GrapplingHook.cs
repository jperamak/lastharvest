using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class GrapplingHook : MonoBehaviour
    {
        [TagSelector]
        [SerializeField]
        private string _hookConnectorTag;

        private Vector3 _velocity;
        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        [SerializeField]
        private float _maxLength = 30.0f;
        public float MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
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
            if((transform.position - _player.position).magnitude >= MaxLength)
            {
                Destroy(gameObject);
            }
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

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.tag.Equals(_hookConnectorTag))
            {
                Destroy(gameObject);
            }
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool AllowUntagged;
    }
}
