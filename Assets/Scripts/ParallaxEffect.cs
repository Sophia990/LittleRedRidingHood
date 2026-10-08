using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax game object
    Vector2 startingPosition;

    // Starting Z value of the parallax game object
    float startingZ;

    Vector2 camMovesSinceStart => (Vector2)cam.transform.position - startingPosition;

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null) {
            Vector2 newPosition = startingPosition + (camMovesSinceStart * parallaxFactor);

            transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }
        
    }
}
