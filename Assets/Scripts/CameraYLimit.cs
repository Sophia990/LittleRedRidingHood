using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraYLimit : MonoBehaviour
{
    public float maxY = 5.0f;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, Mathf.Min(playerTransform.position.y, maxY), playerTransform.position.z);
    }
}
