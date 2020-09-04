using UnityEngine;


[CreateAssetMenu(fileName = "Config", order = 0)]
public class Config : ScriptableObject
{
    [SerializeField] public float MovementSpeed = 4.0f;
    [SerializeField] public float DistanceToReleaseTarget = 15;
}