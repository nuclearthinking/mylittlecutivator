using System;
using Core;
using Gameplay;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mechanics
{
    public class InputController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Joystick joystick;
        public Button attackButton;
        private readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

        private void Awake()
        {
            // attackButton.onClick.AddListener(() => Simulation.Schedule<PlayerShooting>());
        }

        private void Update()
        {
            model.xInput = joystick.Horizontal;
            model.yInput = joystick.Vertical;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer down" + eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Pointer drag " + eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer UP" + eventData);
        }
        
        private void OnDrawGizmos()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                
                var cam = Camera.main;

                var touchPoint =
                    cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 12));

                touchPoint.z = 0.0f;
                
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(touchPoint, new Vector3(1, 1, 0));
                
                // Debug.Log("World point: " + touchPoint);
                Debug.Log("Touch count: "+Input.touchCount);
            }
        }
    }
}