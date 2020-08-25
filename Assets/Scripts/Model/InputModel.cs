using UnityEngine;

namespace Model
{    
    [System.Serializable]
    public class InputModel
    {
        public bool attackButtonPressed;
        public bool joystickHandleActive;
        public GameObject selectedTarget;
        public Material selectedTargetMaterial;
    }
}