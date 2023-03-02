using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    float damage = 10f;

    [SerializeField]
    float range = 100f;

    [SerializeField]
    float impactForce = 30f;

    [SerializeField]
    float fireRate = 15f;

    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        //muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.collider.tag);
            ZombieBrains target = hit.transform.GetComponent<ZombieBrains>();
            if (target != null)
            {
                if (hit.collider.tag == "Head")
                    target.DieByHeadshot();
                else
                    target.TakeDamage(damage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
