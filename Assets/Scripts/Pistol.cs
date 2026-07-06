using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    void Start()
    {
        //Nie ma opóŸnienia miêdzy strza³ami
        cooldown = 0;
        //To nie jest broñ automatyczna, co oznacza, ¿e musimy klikn¹æ przycisk ognia za ka¿dym razem, gdy chcemy z niej strzelaæ
        auto = false;
        ammoMax = 12;
        ammoCurrent = ammoMax;
        ammoBackPack = 60;
    }
    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            if (hit.collider.CompareTag("enemy"))
            {
                // Mo¿esz zmieniæ liczbê 10 na dowoln¹, któr¹ chcesz. To jest iloœæ obra¿eñ zadawanych przez 1 pocisk
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
            }
            Destroy(gameBullet, 1);
        }
    }

}
