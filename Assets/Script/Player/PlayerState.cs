using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerCharacterData PCD;
    public float health;
    public float might = 1;
    

    private void Start()
    {
        health = PCD.Health;
    }

    private void Update()
    {
        
    }
}
