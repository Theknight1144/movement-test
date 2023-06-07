using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform CameraPosition = null;

    void Update()
    {
        transform.position = CameraPosition.position;
    }
}
