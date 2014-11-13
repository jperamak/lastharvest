using System.Linq;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Rope : MonoBehaviour
    {
        public Transform target;
        private LineRenderer line;	
        private bool rope = false;	

        public void Awake()
        {
            line = gameObject.GetComponent<LineRenderer>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void OnMouseDown()
        {
            BuildRope(); //TODO: This should only draw the rope
            target.GetComponent<PlayerInput>().Grapple(transform);
        }


        public void Update()
        {
            if (rope && Input.GetMouseButtonDown(1))
            {
                DestroyRope();
                target.GetComponent<PlayerInput>().DetachGrappling();
            }
        }

        public void LateUpdate()
        {
            if (rope)
            {
                line.SetPosition(0, transform.position);
                line.SetPosition(1, target.transform.position);

                line.enabled = true;
            }
            else
            {
                line.enabled = false;
            }
        }



        private void BuildRope()
        {
            // Rope = true, The rope now exists in the scene!
            rope = true;
        }


        private void DestroyRope()
        {
            // Stop Rendering Rope then Destroy all of its components
            rope = false;
        }
    }
}
