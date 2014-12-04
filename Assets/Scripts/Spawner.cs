using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public Transform thingToSpawn;

        public void Awake()
        {
            tag = Tags.Spawner;
        }

        public Transform Spawn()
        {
            var spawned = Instantiate(thingToSpawn) as Transform;
            spawned.transform.position = transform.position;
            return spawned;
        }
    }
}
