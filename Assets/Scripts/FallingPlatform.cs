using System.Collections;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class FallingPlatform : MonoBehaviour
    {
        public float timeToFall = 3.0f;
        public float fallingSpeed = 100f;

        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            if(collision2D.gameObject.tag.Equals(Tags.Player))
                StartCoroutine(Fall());
        }

        private const float FallingDuration = 5.0f;
        private float _elapsedTime;

        private IEnumerator Fall()
        {
            yield return new WaitForSeconds(timeToFall);
            GetComponent<BoxCollider2D>().enabled = false;
            while (_elapsedTime < FallingDuration)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - fallingSpeed * Time.deltaTime );
                _elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }
}
