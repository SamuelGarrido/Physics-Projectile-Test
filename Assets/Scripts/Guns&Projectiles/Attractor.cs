using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    #region Fields
    private Rigidbody rb;
    [SerializeField]
    private float absorbRadius;
    [SerializeField]
    private float gravityMultiplicator;
    [SerializeField]
    private float massAfterShoot;
    [SerializeField]
    private LayerMask GunLayer;
    #endregion

    void Start() {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {

        //Get an array of objects around the projectile
        Collider[] HitColliders = Physics.OverlapSphere(this.transform.position, absorbRadius);
        
        //Attract objects around the projectile
        foreach (var HitCollider in HitColliders) {
            if (HitCollider != this.GetComponent<Collider>() && HitCollider.tag != "Gun") {
                Attract(HitCollider.gameObject);
            }

        }
    }

    private void Attract(GameObject ObjToAttarct) {
        
        //Get Rigidbody  of the obj we want to attract
        Rigidbody RbToAttract = ObjToAttarct.GetComponent<Rigidbody>();
        
        //Check if go has rb
        if (RbToAttract != null) {

            //Direction of the attraction
            Vector3 Direction = rb.position - RbToAttract.position;

            //Distance between the to objects
            float Distance = Direction.magnitude;

            //Get the force of the magnitude using: F =( m1 * m2 ) / d^2
            float ForceMagnitude = gravityMultiplicator * (rb.mass * RbToAttract.mass) / Mathf.Pow(Distance, 2);

            //Final force Vector
            Vector3 Force = Direction.normalized * ForceMagnitude;

            RbToAttract.AddForce(Force);
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider) {
            //Stop motion
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.mass = massAfterShoot;

            //Destroy projectile
            StartCoroutine(DestroyProjectile());
        }
    }

    private IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }

}
