using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaProjectileScript : MonoBehaviour {

    //Handle projectile lifetime
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider) {
            StartCoroutine(DestroyProjectile());
        }   
    }

    //After 5 seconds the projectile is destroyed
    private IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
