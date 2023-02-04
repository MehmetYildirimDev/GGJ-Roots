using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Turtleplayer : MonoBehaviour
{

    public GameObject hedefOyuncu;
    public float kovalamaMesafesi;
    NavMeshAgent turtleNavMesh;
    // Start is called before the first frame update
    void Start()
    {
        hedefOyuncu= GameObject.Find("FirstPersonController");
        turtleNavMesh=this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
     float mesafe= Vector3.Distance(this.transform.position, hedefOyuncu.transform.position); 
        if (mesafe< kovalamaMesafesi) 
        {
            turtleNavMesh.SetDestination(hedefOyuncu.transform.position);
        }
    }
}
