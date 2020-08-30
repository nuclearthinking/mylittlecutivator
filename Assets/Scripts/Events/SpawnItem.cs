using Core;
using UnityEngine;
namespace Gameplay
{
    public class SpawnItem : Simulation.Event<SpawnItem>
    {
        public Transform position;
        public GameObject item;

        public override void Execute()
        {
            GameObject.Instantiate(item, position.position, Quaternion.identity);
        }
    }
}