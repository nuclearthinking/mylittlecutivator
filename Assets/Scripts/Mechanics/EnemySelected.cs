using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class EnemySelected : Simulation.Event<EnemySelected>
    {
        public GameObject target;

        private Material outlineShaderMaterial = Resources.Load<Material>("Materials/OutlineMaterial");

        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();
        private readonly GameModel gameModel = Simulation.GetModel<GameModel>();

        public override void Execute()
        {
            if (target == null)
                return;
            var targetRenderer = target.GetComponent<SpriteRenderer>();
            if (inputModel.selectedTarget != null)
            {
                inputModel.selectedTarget.GetComponent<SpriteRenderer>().material = inputModel.selectedTargetMaterial;
                inputModel.selectedTarget = target;
                inputModel.selectedTargetMaterial = targetRenderer.material;
                targetRenderer.material = outlineShaderMaterial;
            }
            else
            {
                inputModel.selectedTarget = target;
                inputModel.selectedTargetMaterial = targetRenderer.material;
                targetRenderer.material = outlineShaderMaterial;
            }
            gameModel.player.selectedTarget = target;
        }
    }
}