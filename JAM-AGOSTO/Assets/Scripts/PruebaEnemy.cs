using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PruebaEnemy : Damager
{
    [SerializeField] private GameObject ClaraMaria;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            print(ClaraMaria.GetComponent<Health>().Current);
            ApplyDamage(ClaraMaria.GetComponent<Health>());
        }

        if (Input.GetKeyDown(KeyCode.C)) 
        {
            print(ClaraMaria.GetComponent<Mana>().Current);
            ApplyDamage(ClaraMaria.GetComponent<Mana>());
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print(ClaraMaria.GetComponent<Resources>().Current);
            ApplyHeal(ClaraMaria.GetComponent<Resources>());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print(ClaraMaria.GetComponent<Resources>().Current);
            ApplyDamage(ClaraMaria.GetComponent<Resources>());
        }
    }
}
