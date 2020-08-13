using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    // Start is called before the first frame update

    private HeroController startPlayer;
    private CameraController startCamera;

    public Vector2 startDirection;
    void Start()
    {
        startPlayer = FindObjectOfType<HeroController>();
        startCamera = FindObjectOfType<CameraController>();
        startPlayer.lastMove = startDirection;

        startPlayer.transform.position = transform.position;
        startCamera.transform.position =
            new Vector3(transform.position.x, transform.position.y, startCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}