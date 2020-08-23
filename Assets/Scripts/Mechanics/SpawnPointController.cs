using System;
using UnityEngine;

namespace Mechanics
{
    public class SpawnPointController : MonoBehaviour
    {
        public string prefabPath;
        public float spawnTreshold = 5f;


        private float killedAt;
        private GameObject spawnedCreature;
        private GameObject creatureToSpawn;
        private void Start()
        {
            if (prefabPath != null)
            {
                creatureToSpawn = Resources.Load<GameObject>(prefabPath);
                spawnedCreature = Instantiate(creatureToSpawn, transform.position, Quaternion.identity);
            }
        }

        private void Update()
        {
            if (spawnedCreature == null && Time.time >= killedAt + spawnTreshold)
            {
                killedAt = Time.time;
                spawnedCreature = Instantiate(creatureToSpawn, transform.position, Quaternion.identity);
            }
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
    }
}