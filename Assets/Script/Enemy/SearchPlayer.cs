using System;
using UnityEngine;


public class SearchPlayer : MonoBehaviour
{
    public float searchDistance;
    public GameObject player;
    EnemyState state;
    //public float timerSearch;
    private float searchTimer;

    private void Awake()
    {
        state = transform.parent.GetComponentInParent<EnemyState>();
    }

    private void Start()
    {
        player = state.player;
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= searchDistance)
        {
            state.meetPlayer = true;
            searchTimer += Time.deltaTime;
        }
        else
        {
            searchTimer -= Time.deltaTime;
            if (searchTimer <= 0)
            {
                state.meetPlayer = false;
                searchTimer = 0;
            }
            
        }
    }
}
