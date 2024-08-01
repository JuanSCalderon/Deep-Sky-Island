using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed;

    void LateUpdate()
    {
        transform.Rotate(speed * Time.deltaTime * Vector3.up, Space.World);
    }
}
