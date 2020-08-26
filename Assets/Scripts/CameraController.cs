using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    public float moveSpeed;

    void Update()
    {
        var followTargetPosition = followTarget.transform.position;
        var targetPosition = new Vector3(
            followTargetPosition.x, 
            followTargetPosition.y, 
            transform.position.z
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
