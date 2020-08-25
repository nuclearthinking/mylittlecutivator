using Core;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Mechanics
{
    public class AttackButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();

        public void OnPointerDown(PointerEventData eventData)
        {
            inputModel.attackButtonPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputModel.attackButtonPressed = false;
        }
    }
}