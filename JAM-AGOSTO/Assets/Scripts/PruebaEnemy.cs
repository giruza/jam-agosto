using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PruebaEnemy : Damager
{
    [SerializeField] private GameObject ClaraMaria;

    //prueba objetos usables
    [SerializeField] private Sprite _object01;
    [SerializeField] private Sprite _object02;
    [SerializeField] private Sprite _object03;
    [SerializeField] private Sprite _object04;

    [SerializeField] private HUD _hud;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            print(ClaraMaria.GetComponent<Health>().Current);
            ApplyDamage(ClaraMaria.GetComponent<Health>());
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            print(ClaraMaria.GetComponent<Health>().Current);
            ApplyHeal(ClaraMaria.GetComponent<Health>());
        }

            if (Input.GetKeyDown(KeyCode.C)) 
        {
            print(ClaraMaria.GetComponent<Mana>().Current);
            ApplyDamage(ClaraMaria.GetComponent<Mana>());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            print(ClaraMaria.GetComponent<Mana>().Current);
            ApplyHeal(ClaraMaria.GetComponent<Mana>());
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

        if (Input.anyKeyDown) 
        {
            var input = Input.inputString;
            //print(input);
            switch (input) 
            {
                case "1":
                    //Usar objeto 01
                    print(input);
                    _hud.UseObject(_object01, 0);
                    break;
                case "2":
                    //Usar objeto 02
                    _hud.UseObject(_object02, 1);
                    break;
                case "3":
                    //Usar objeto 03
                    _hud.UseObject(_object03, 2);
                    break;
                case "4":
                    //Usar objeto 04
                    _hud.UseObject(_object04, 3);
                    break;
            }
        }
    }
}
