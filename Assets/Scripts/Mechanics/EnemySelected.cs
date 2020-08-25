using Core;
using Model;
using UnityEngine;

namespace Mechanics
{
    public class EnemySelected : Simulation.Event<EnemySelected>
    {
        public GameObject target;

        private readonly Material outlineShaderMaterial = Resources.Load<Material>("Materials/OutlineMaterial");

        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();

        public override void Execute()
        {
            if (target == null)
                return;
            var targetRenderer = target.GetComponent<SpriteRenderer>();
            
            // Если цель уже была выбрана, возвращаем ей ее дефолтный материал.
            if (inputModel.selectedTarget != null)
                inputModel.selectedTarget.GetComponent<SpriteRenderer>().material = inputModel.selectedTargetDefaultMaterial;
            
            inputModel.selectedTarget = target;
            inputModel.selectedTargetDefaultMaterial = targetRenderer.material;
            targetRenderer.material = outlineShaderMaterial;
        }
    }
}