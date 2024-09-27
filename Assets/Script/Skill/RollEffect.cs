using System;
using UnityEngine;

public class RollEffect : MonoBehaviour
{
    public float rollSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rollSpeed * Time.deltaTime * -1));
    }
}