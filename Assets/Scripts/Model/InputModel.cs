using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class InputModel
    {
        public GameObject selectedTarget;
        public Material selectedTargetDefaultMaterial;
        public List<GameObject> nearEnemies = new List<GameObject>(); 
    }
}