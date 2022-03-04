using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : ScriptableObject {

    [Header("Bullet")]
    public GameObject Bullet;

    [Header("Bullet force")]
    public float ShootForce, UpwardForce;

    [Header("Gun stats")]
    public float TimeBetweenShooting, ReloadTime, TimeBetweenShots;
    public int MagazineSize, BulletsPerTap;
    public bool AllowButtonHold;

    public int BulletsLeft, BulletsShot;

    public bool Shooting, ReadyToShoot, Reloading;

}
