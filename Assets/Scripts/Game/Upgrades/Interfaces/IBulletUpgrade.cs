using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBulletUpgrade
{
    // Called when a new bullet is shot
    void OnBulletInit();

    // Called every frame
    void OnBulletUpdate();

    // Called when a bullet hits an enemy
    void OnBulletHit();
}
