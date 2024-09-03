using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class hitboxLocation : MonoBehaviour
{
    private PlayerMovement PM;
    private Vector3 playerDistance;
    public Transform player;
    
    private void Awake()
    {
        PM = GetComponentInParent<PlayerMovement>();
        playerDistance = transform.position - player.position; // 두 위치의 차이를 벡터로 저장
    }

    private void Update()
    {
        Vector3 newPosition = transform.position; // 현재 위치를 복사
        if (PM.xAxis >= 0.1)
        {
            newPosition.x = player.position.x + playerDistance.x;
        }
        else if(PM.xAxis <= -0.1)
        {
            newPosition.x = player.position.x - playerDistance.x;
        }
        transform.position = newPosition; // 새로운 위치로 갱신
    }
}