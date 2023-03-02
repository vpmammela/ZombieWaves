using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBrains : MonoBehaviour
{
    Animator m_zombieAnimation;
    Rigidbody rb;
    //public LayerMask whatIsGround;
    //bool isGrounded;
    SpawnPoint sp;
    bool isDead = false;

    [SerializeField]
    float playerHeight;

    [SerializeField]
    float groundDrag;
    NavMeshAgent m_zombiePathFinder;

    public float health = 50f;

    public void TakeDamage(float amount)
    {
        if (!isDead)
        {
            health -= amount;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        RemoveCollidersRecursively();
        m_zombieAnimation.SetTrigger("DieBackward");
        m_zombiePathFinder.isStopped = true;
        sp.AddKill();
    }

    public void DieByHeadshot()
    {
        if (!isDead)
        {
            RemoveCollidersRecursively();
            m_zombieAnimation.SetTrigger("DieForward");
            m_zombiePathFinder.isStopped = true;
            isDead = true;
            sp.AddKill();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_zombieAnimation = GetComponent<Animator>();
        m_zombiePathFinder = GetComponent<NavMeshAgent>();
        sp = GameObject.FindObjectOfType(typeof(SpawnPoint)) as SpawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        m_zombiePathFinder.destination = target.transform.position;
   
    }
    private void RemoveCollidersRecursively()
    {
        var allColliders = GetComponentsInChildren<Collider>();

        foreach (var childCollider in allColliders) Destroy(childCollider);
    }
}

