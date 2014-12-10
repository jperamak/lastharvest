using System.Collections;
using Assets.Scripts.Helpers;
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

        public float preventFromHookingSeconds = 0.05f;

        public SoundEffect throwSound;
        public SoundEffect grabSound;
        public SoundEffect collideSound;
        public SoundEffect pullBackSound;
        public SoundEffect pullBackEndSound;
        public SoundEffect releaseSound;

        private Transform _player;

        private LineRenderer line;	

        public void Awake()
        {

            throwSound = (SoundEffect)Instantiate(throwSound);
            throwSound.transform.SetParent(this.transform);
            grabSound = (SoundEffect)Instantiate(grabSound);
            grabSound.transform.SetParent(this.transform); 
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            if (gc.collideSound == null)
            {
                collideSound = (SoundEffect)Instantiate(collideSound);
                collideSound.transform.SetParent(gc.transform);
            }
            else
                collideSound = gc.collideSound;
            pullBackSound = (SoundEffect)Instantiate(pullBackSound);
            pullBackSound.transform.SetParent(this.transform); 
            pullBackEndSound = (SoundEffect)Instantiate(pullBackEndSound);
            pullBackEndSound.transform.SetParent(this.transform); 
            releaseSound = (SoundEffect)Instantiate(releaseSound);
            releaseSound.transform.SetParent(this.transform);

            line = gameObject.GetComponent<LineRenderer>();
            line.SetColors(Color.white, Color.black);
            line.SetWidth(0.9f, 0.9f);
            line.SetVertexCount(2);
            line.material = new Material(Shader.Find("Particles/Additive"));
            _player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            throwSound.Do(s => s.PlayEffect());
            StartCoroutine(PreventFromHooking(preventFromHookingSeconds));
        }

        private IEnumerator PreventFromHooking(float seconds)
        {
            collider2D.enabled = false;
            yield return new WaitForSeconds(seconds);
            collider2D.enabled = true;
            yield return null;
        }

        public void FixedUpdate()
        {
            transform.position += _velocity;
            if((transform.position - _player.position).magnitude >= MaxLength)
            {
                pullBackSound.Do(s => s.PlayEffect());
                Destroy(gameObject);
            }
        }

        public void LateUpdate()
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, _player.transform.position);

            line.enabled = true;
        }

        public void Connect(HookConnector connector)
        {
            _velocity = Vector3.zero;
            _player.GetComponent<PlayerInput>().Grapple(connector.transform);
            grabSound.Do(s => s.PlayEffect());
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.tag.Equals(_hookConnectorTag))
            {
                collideSound.Do(s => s.PlayEffect());
                grabSound.Stop();
                //pullBackSound.PlayEffect();
                _player.GetComponent<PlayerInput>().DetachGrappling();
            }
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool AllowUntagged;
    }
}
