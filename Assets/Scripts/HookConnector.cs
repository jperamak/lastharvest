using UnityEngine;

namespace Assets.Scripts
{
    public class HookConnector : MonoBehaviour
    {
        public ParticleSystem connectEffect;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var hook = collision.collider.GetComponent<GrapplingHook>();
            if (hook != null)
            {
                hook.Connect(collision.transform);
                var explosion = (ParticleSystem)Instantiate(connectEffect);
                explosion.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
        }
    }
}
