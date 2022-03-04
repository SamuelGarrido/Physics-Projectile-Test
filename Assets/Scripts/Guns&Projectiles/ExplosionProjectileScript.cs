using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectileScript : MonoBehaviour {

    #region Fields
    [Header("Assignables")]
    [SerializeField]
    private Rigidbody rb;

    [Header("Stats")]
    [Range(0f, 1f)]
    [SerializeField]
    private float bounciness;
    [SerializeField]
    private bool useGravity;

    [Header("Damage")]
    [SerializeField]
    private float explosionRange;
    [SerializeField]
    private float explosionForce;


    [Header("Lifetime")]
    [SerializeField]
    private int maxCollisions;
    [SerializeField]
    private float maxLifetime;
    [SerializeField]
    private bool explodeOnTouch = true;

    private int collisions;
    private PhysicMaterial physics_mat;
    #endregion
    void Start() {
        SetUp();
    }


    void Update()  {
        if (collisions > maxCollisions) {
            Explode();
        }
    }

    private void Explode() {
        Collider[] HitColliders = Physics.OverlapSphere(transform.position, explosionRange);
        for (int i = 0; i < HitColliders.Length; i++) {
            if (HitColliders[i].GetComponent<Rigidbody>()) {
                Debug.Log(HitColliders[i]);
                //Use Rigidbody.AddExplosionForce and add => power of the explosion, the position of the explosion and the radius
                HitColliders[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            }
            StartCoroutine(DestroyProjectile());
        }
    }

    private IEnumerator DestroyProjectile() {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        //Count up collisions 
        collisions++;
        if (explodeOnTouch) {
            Explode(); 
        }
    }

    private void SetUp() {
        //Create a new Physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        //For a good bounciness set frictionCombine and bounceCombine
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assing materail to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //Set garavity
        rb.useGravity = useGravity;
    }
}
