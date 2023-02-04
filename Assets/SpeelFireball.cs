using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeelFireball : MonoBehaviour
{
    public float Damage = 20f;
    public GameObject impactVFX;
    private bool collided;



    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 2f);

            Destroy(gameObject);
        }
    }
}
