using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndDrop : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GunScript gun_Script;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private BoxCollider coll;

    [SerializeField]
    private Transform player, gunContainer, fpsCam;

    [SerializeField]
    private float pickUpRange;

    [SerializeField]
    private float dropForwardForce, dropUpwardForce;

    [SerializeField]
    private bool equipped;

    [SerializeField]
    private static bool slotFull;
    #endregion

    private void Start() {
        //Setup
        if (!equipped) {
            gun_Script.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped) {
            gun_Script.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    void Update() {
        //Check if player is in range and "e" is pressed
        Vector3 DistanceToPlayer = player.position - transform.position;
        if (!equipped && DistanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) {
            PickUp();
        }
        //Drop if equipped and "q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) {
            Drop();
        }
    }

    private void PickUp() {

        //Make sure you can´t pick up more than 1 weapon
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make RigidBody kinematic and collider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Enable weapon script
        gun_Script.enabled = true;
    }

    private void Drop() {

        //Empty slot
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make RigidBody not kinematic and collider back to normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //disnable weapon script
        gun_Script.enabled = false;
                    
    }
}
