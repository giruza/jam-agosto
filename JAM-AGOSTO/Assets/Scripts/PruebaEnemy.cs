using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PruebaEnemy : Damager
{
    [SerializeField] private GameObject ClaraMaria;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            print(ClaraMaria.GetComponent<HUD>().Current);
            ApplyDamage(ClaraMaria.GetComponent<HUD>());
        }
    }
}
