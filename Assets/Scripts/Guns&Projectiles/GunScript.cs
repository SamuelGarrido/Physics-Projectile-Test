using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunScript : MonoBehaviour {
    #region Fields

    [Header("Bullet")]
    [SerializeField]
    private GameObject Bullet;

    [Header("Bullet force")]
    [SerializeField]
    private float ShootForce, UpwardForce;

    [Header("Gun stats")]
    [SerializeField]
    private float TimeBetweenShooting, ReloadTime, TimeBetweenShots;
    [SerializeField]
    private int MagazineSize, BulletsPerTap;
    [SerializeField]
    private bool AllowButtonHold;

    //Handle bullets
    private int BulletsLeft, BulletsShot;

    //Handle shooting
    private bool Shooting, ReadyToShoot, Reloading;

    [Header("Reference")]
    [SerializeField]
    private Camera FpsCam;
    [SerializeField]
    private Transform AttackPoint;

    //Graphics
    //public TextMeshProUGUI AmmunitionDisplay;

    //Wait for reset Shot
    public bool AllowInvoke = true;

    #endregion
    void Awake() {
        //Magazine is full
        BulletsLeft = MagazineSize;
        ReadyToShoot = true;
    }

    void Update() {
        GunInput();
        
        /*
        //Set ammo display
        if (AmmunitionDisplay != null) {
            AmmunitionDisplay.SetText(BulletsLeft / BulletsPerTap + " / " + MagazineSize / BulletsPerTap);
        }
        */
    }

    private void GunInput() {

        //Check if allowed to hold fire (hold button) and take input 
        if (AllowButtonHold) {
            Shooting = Input.GetKey(KeyCode.Mouse0);
        } else {
            Shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < MagazineSize && !Reloading) {
            Reload();
        }
        //Auomatic reload when there is no ammo
        if (ReadyToShoot && Shooting && !Reloading && BulletsLeft <= 0) {
            Reload();
        }

        //Shooting 
        if (ReadyToShoot && Shooting && !Reloading && BulletsLeft > 0) {

            //Set bullets shot to 0
            BulletsShot = 0;
            Shoot();
        }

    }

    private void Shoot() {

        ReadyToShoot = false;

        //Find Hit position
        Ray Ray = FpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit Hit;

        //Check if the ray hits smthg
        Vector3 TargetPoint;
        if (Physics.Raycast(Ray, out Hit)) {
            TargetPoint = Hit.point;
        } else {
            //if it not just cast a ray away of the player
            TargetPoint = Ray.GetPoint(75);
        }
        
        //Calculate direction from attackPoint to targetPoint (B - A)
        Vector3 DirectionWithoutSpread = TargetPoint - AttackPoint.position;

        //Instatiate bullet
        GameObject CurrentBullet = Instantiate(Bullet, AttackPoint.position, Quaternion.identity); //Store instantiated bullet in CurrentBullet
        //Rotate bullet to shoot direction 
        CurrentBullet.transform.forward = DirectionWithoutSpread.normalized;

        //Add force to the bullet 
        CurrentBullet.GetComponent<Rigidbody>().AddForce(DirectionWithoutSpread.normalized * ShootForce, ForceMode.Impulse);

        //

        if (AllowInvoke) {
            StartCoroutine(ResetShot());
            AllowInvoke = false;
        }


        BulletsLeft--;
        BulletsShot++;
    }

    private IEnumerator ResetShot() {
        
        yield return new WaitForSeconds(TimeBetweenShooting);
        ReadyToShoot = true;
        AllowInvoke = true;

    }

    private void Reload() {
        Reloading = true;
        StartCoroutine(ReloadFinished());
    }

    private IEnumerator ReloadFinished() {
        yield return new WaitForSeconds(ReloadTime);
        BulletsLeft = MagazineSize;
        Reloading = false;
    }

}
