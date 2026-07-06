using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Pistol
{
    void Start()
    {
        //OpóŸnienie miêdzy strza³ami (mo¿esz ustawiæ dowoln¹ wartoœæ)
        cooldown = 0.2f;
        //Ta broñ strzela w pe³nym automacie; bêdzie kontynuowa³a strzelanie, dopóki trzymamy przycisk myszy (nie martw siê: powy¿ej zdefiniowane opóŸnienie zostanie uwzglêdnione!
        auto = true;
        ammoMax = 24;
        ammoCurrent = ammoMax;
        ammoBackPack = 80;
    }
}
