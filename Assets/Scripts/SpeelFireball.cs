using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeelFireball : MonoBehaviour
{
    public int Damage = 20;
    public GameObject impactVFX;
    private bool collided;



    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "boss" && !collided)
        {
            Debug.Log("boss carpisma");
            collided = true;

            collision.gameObject.transform.root.GetComponent<EnemyBoss>().onDamage(Damage);

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 2f);

            Destroy(gameObject);
        }

        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 2f);

            Destroy(gameObject);
        }

    }
}
