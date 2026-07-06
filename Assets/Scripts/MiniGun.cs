using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGun : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        //OpóŸnienie miêdzy strza³ami (mo¿esz ustawiæ dowoln¹ wartoœæ, któr¹ lubisz)
        cooldown = 0.1f;
        //Ta broñ strzela w trybie pe³nego automatycznego ognia; bêdzie strzelaæ tak d³ugo, jak trzymamy przycisk myszy (nie martw siê: opóŸnienie, które zdefiniowa³eœ powy¿ej, zostanie uwzglêdnione!
        auto = true;
        ammoMax = 40;
        ammoCurrent = ammoMax;
        ammoBackPack = 120;
    }
    protected override void OnShoot()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector3 drift = new Vector3(Random.Range(-15, 15), Random.Range(-15, 15));
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition + drift);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            if (hit.collider.CompareTag("enemy"))
            {
                // Mo¿esz zmieniæ liczbê 10 na dowoln¹, któr¹ chcesz. To jest iloœæ obra¿eñ zadawanych przez 1 pocisk
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(20);
            }
            Destroy(gameBullet, 1);
        }
    }

}
