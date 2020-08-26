using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class ReleaseEnemySelection : Simulation.Event<ReleaseEnemySelection>
    {
        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();

        public override void Execute()
        {
            if (inputModel.selectedTarget == null)
                return;
            
            var selectedTargetRenderer = inputModel.selectedTarget.GetComponent<SpriteRenderer>();
            selectedTargetRenderer.material = inputModel.selectedTargetDefaultMaterial;
            inputModel.selectedTarget = null;
            inputModel.selectedTargetDefaultMaterial = null;
        }
    }
}