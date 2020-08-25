using System;
using System.Collections.Generic;
using System.Linq;
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
        public LayerMask enemyLayers;


        private readonly InputModel inputModel = Simulation.GetModel<InputModel>();
        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

        private void Awake()
        {
        }

        private void Update()
        {
            playerModel.xInput = joystick.Horizontal;
            playerModel.yInput = joystick.Vertical;
            
            CheckIsEnemySelected();
        }

        private void CheckIsEnemySelected()
        {
            if (inputModel.attackButtonPressed)
                return;

            if (inputModel.joystickHandleActive && Input.touchCount == 0)
                return;
            
            var mouseClickPosition = GetMouseWorldClickPosition();
            var touches = GetTouchWorldPositions();

            if (mouseClickPosition != null)
            {
                Collider2D[] collides = new Collider2D[20];
                Physics2D.OverlapCircleNonAlloc((Vector2) mouseClickPosition, 0.7f, collides, enemyLayers);
                foreach (var collide in collides)
                {
                    if (collide != null)
                        Simulation.Schedule<EnemySelected>().target = collide.gameObject;
                }
            }
            else if (touches.Count > 0)
            {
                foreach (var touch in touches)
                {
                    Collider2D[] collides = new Collider2D[20];
                    Physics2D.OverlapCircleNonAlloc(touch, 0.7f, collides, enemyLayers);

                    foreach (var collide in collides)
                    {
                        if (collide != null)
                            Simulation.Schedule<EnemySelected>().target = collide.gameObject;
                    }
                }
            }
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
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.25f, 0.75f, 1f, 0.5f);
            var mouseClickPosition = GetMouseWorldClickPosition();
            var touches = GetTouchWorldPositions();
            if (mouseClickPosition != null)
            {
                Gizmos.DrawCube((Vector3) mouseClickPosition, new Vector3(0.7f, 0.7f, 0));
            }
            else if (touches.Count > 0)
            {
                foreach (var touch in touches)
                {
                    Gizmos.DrawCube(touch, new Vector3(0.7f, 0.7f, 0));
                }
            }
        }

        private Vector3? GetMouseWorldClickPosition()
        {
            var cam = Camera.main;
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;

                Vector3 mouseClickPosition = cam.ScreenToWorldPoint(new Vector3(
                        mousePosition.x,
                        mousePosition.y,
                        12
                    )
                );
                mouseClickPosition.z = 0;
                return mouseClickPosition;
            }

            return null;
        }

        private List<Vector3> GetTouchWorldPositions()
        {
            var cam = Camera.main;
            List<Vector3> touches = new List<Vector3>();
            if (Input.touchCount > 0)
            {
                foreach (var touch in Input.touches)
                {
                    var touchPoint = cam.ScreenToWorldPoint(
                        new Vector3(
                            touch.position.x,
                            touch.position.y,
                            Math.Abs(cam.transform.position.z)
                        )
                    );
                    touchPoint.z = 0.0f;
                    touches.Add(touchPoint);
                }
            }

            return touches;
        }
    }
}