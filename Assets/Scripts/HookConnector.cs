using UnityEngine;

namespace Assets.Scripts
{
    public class HookConnector : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            var hook = collision.collider.GetComponent<GrapplingHook>();
            if (hook != null)
            {
                hook.Connect(collision.transform);
            }
        }
    }
}
