using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("carpisma");
        if (collision.gameObject.CompareTag("Bullet") && !gameObject.transform.root.GetComponent<EnemyBoss>().isDead)
        {
            int rand = Random.Range(1, 3);
            gameObject.transform.root.GetComponent<Animator>().Play("Monster_anim|Get_hit" + rand);

        }
    }

}
