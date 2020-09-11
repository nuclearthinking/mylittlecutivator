using System;
using System.Collections.Generic;
using Core;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    public class InputSystem : MonoBehaviour
    {
        public Joystick joystick;

        private readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();


        private void Update()
        {
            playerModel.xInput = joystick.Horizontal;
            playerModel.yInput = joystick.Vertical;
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
                        Math.Abs(cam.transform.position.z)
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
            var touches = new List<Vector3>();
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