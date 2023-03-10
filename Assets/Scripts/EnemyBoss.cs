using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{

    public bool isDead = false;
    public int currentHealt = 250;
    private int Healt;

    public Transform target;

    public bool useFootSteps = true;
    [Header("Foot Steps Parameters")]
    public float baseStepSpeed = 0.5f;
    public float CrawlerStepSpeed = 0.25f;
    public AudioSource FootStepaudioSource = default;
    public AudioClip[] woodClips = default;
    public AudioClip[] NormalClips = default;
    public AudioClip[] MetalClips = default;
    public AudioClip[] grassClips = default;
    public float footStepTimer = 0;

    public bool useSetDistance = true;
    protected float Distance;
    protected NavMeshAgent Agent;
    protected Animator animator;

    public float lookRadius = 10f;

    [Header("Sounds System")]
    public AudioSource MainAudioSource;
    public AudioClip takeDamageSfx;
    public AudioClip[] ZombieSounds;


    public MainSceneManager sceneManager;
    private void Start()
    {
        Healt = currentHealt;
        isDead = false;
        Agent = GetComponent<NavMeshAgent>();
        MainAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        target = FirstPersonController.instance.transform;
    }

    private void Update()
    {
        Debug.Log(Healt.ToString());
        if (!isDead)
        {
            if (useFootSteps)
                HandleFootSteps();
            if (useSetDistance)
                HandlesetDistance();

            //     SoundEffectCheck();
            // }
            // else
            // {
            //     if (MainAudioSource.clip != ZombieSounds[3])
            //     {
            //         MainAudioSource.clip = ZombieSounds[3];
            //         MainAudioSource.Play();
            //     }
        }
    }

    public void SoundEffectCheck()
    {

        if (Distance <= Agent.stoppingDistance)
        {
            if (MainAudioSource.clip != ZombieSounds[2])
            {
                MainAudioSource.clip = ZombieSounds[2];
                MainAudioSource.Play();
            }
            return;
        }

        if (Distance <= lookRadius)
        {
            if (MainAudioSource.clip != ZombieSounds[0])
            {
                MainAudioSource.clip = ZombieSounds[0];
                MainAudioSource.Play();
            }
        }
        else
        {
            if (MainAudioSource.clip != ZombieSounds[1])
            {
                MainAudioSource.clip = ZombieSounds[1];
                MainAudioSource.Play();
            }
        }





    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //normalized: yonunu koruyo ama uzunlugu 1 kaliyo ///mesela burada yon onemli o yuzden yaptik

        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime * 5f);

    }

    public void HandlesetDistance()
    {
        Distance = Vector3.Distance(transform.position, target.position);
        animator.SetFloat("Distance", Distance);

        if ((Distance <= lookRadius || Healt != currentHealt)) //hasar aldiginda da gelmesi gerek
        {

            Agent.SetDestination(target.position);

            FaceTarget();
        }

    }

    protected virtual void HandleFootSteps()
    {

        if (Agent.velocity.magnitude <= 0) return;
        //if (currentInput == Vector2.zero) return;

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 3f))
            {
                switch (hit.collider.tag)
                {
                    case "FootSteps/Grass":
                        FootStepaudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        break;
                    case "FootSteps/Metal":
                        FootStepaudioSource.PlayOneShot(MetalClips[Random.Range(0, MetalClips.Length - 1)]);
                        break;
                    case "FootSteps/Wood":
                        FootStepaudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        break;

                    default:
                        FootStepaudioSource.PlayOneShot(NormalClips[Random.Range(0, NormalClips.Length - 1)]);
                        break;
                }
            }

            footStepTimer = baseStepSpeed;
        }


    }


    public void onDamage(int damageAmount)
    {
        AudioSource.PlayClipAtPoint(takeDamageSfx, transform.position, 1f);
        Healt -= damageAmount;
        if (Healt <= 0)//isdead
        {
            // animator.Play("Monster_anim|Death");
            animator.SetBool("isDead", true);
            isDead = true;
            //animator.enabled = false;
            Agent.speed = 0f;
            //MainAudioSource.Stop();
            Destroy(this.gameObject,3f);
            StartCoroutine(WinSCene());  
        }
        Agent.isStopped = false;
    }

    IEnumerator WinSCene()
    {
        yield return new WaitForSeconds(2f);
        sceneManager.Win();
    }

    //animasyonda vuruyorsa burayi cagiriyo ve eger hasar al dogruysa(o da kolu degdiyse true oluyo) hasar verito ve surekli hasar alma olayini cozyuoruz
    /*
     
    public void onDamagePlayer()
    {
        if (FirstPersonController.instance.ZombieTakeDamage)
        {
            FirstPersonController.instance.ApplyDamage(10);
        }
        FirstPersonController.instance.ZombieTakeDamage = false;
    }
    */

    public void Freeze(int second)
    {
        StartCoroutine(IFreeze(second));
    }

    IEnumerator IFreeze(int second)
    {
        animator.enabled = false;
        Agent.isStopped = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(second);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        animator.enabled = true;
        Agent.isStopped = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("carpisma");
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int rand = Random.Range(1, 3);
            animator.Play("Monster_anim|Get_hit" + rand);
        }
    }
}

