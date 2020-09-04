using System;
using UnityEngine;

namespace Mechanics
{
    public class SpawnPointController : MonoBehaviour
    {
        public string prefabPath;
        public float spawnTreshold = 5f;
        [SerializeField] private float spawnTimer;
        [SerializeField] private GameObject spawnedCreature;
        private GameObject creatureToSpawn;
        private void Start()
        {
            spawnTimer = spawnTreshold;
            if (prefabPath != null)
            {
                creatureToSpawn = Resources.Load<GameObject>(prefabPath);
                spawnedCreature = Instantiate(creatureToSpawn, transform.position, Quaternion.identity);
            }
        }

        private void Update()
        {
            if (IsAttachedCreatureDead())
            {
                spawnTimer -= Time.deltaTime;    
            }
            if (spawnTimer <= .0f)
            {
                spawnedCreature = Instantiate(creatureToSpawn, transform.position, Quaternion.identity);
                spawnTimer = spawnTreshold;
            }
        }


        bool IsAttachedCreatureDead()
        {
            if (spawnedCreature != null)
            {
                return spawnedCreature.activeSelf;
            }
            return spawnedCreature == null;
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
    }
}