using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        transform.position = targetPos;
    }
}
