using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera camera;
    private Vector3 destination;
    public List<GameObject> projectile;
    public Transform FirePoint;
    private float MultiplierfireballSpeed = 1.5f;
    public float FireRate = 4f;
    private float timetoFire;
    public float arcRange = 1;
    private int index;
    private Animator animator;
    public AudioSource audioSource;
    public AudioClip[] ShootClip;
    void Start()
    {
        index = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timetoFire)
        {
            timetoFire = Time.time + 1 / FireRate;
            animator.SetTrigger("Shoot");
            ShootProjectile();
            audioSource.PlayOneShot(ShootClip[UnityEngine.Random.Range(0, ShootClip.Length)]);
        }


        ChangeMagic();

    }

    private void ChangeMagic()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (index >= projectile.Count - 1)
                index = 0;
            else
                index++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (index <= projectile.Count - 1)
                index = projectile.Count - 1;
            else
                index--;
        }
    }

    public void ShootProjectile()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(FirePoint);
    }

    private void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile[index], firePoint.position, Quaternion.identity) as GameObject;
            float projectileSpeed = 30f;

        //if (index == 0) projectileSpeed /= MultiplierfireballSpeed;
        if (index == 1) projectileSpeed /= MultiplierfireballSpeed;

        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(
              UnityEngine.Random.Range(-arcRange, arcRange),
              UnityEngine.Random.Range(-arcRange, arcRange), 0),
              UnityEngine.Random.Range(0.5f, 2f));
    }
}
