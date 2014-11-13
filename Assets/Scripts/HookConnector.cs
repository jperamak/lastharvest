using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class HookConnector : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            var hook = collision.collider.GetComponent<GrapplingHook>();
            if (hook != null)
            {
                hook.Connect(this);
            }
        }
    }
}
