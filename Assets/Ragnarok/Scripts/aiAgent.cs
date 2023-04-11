using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class aiAgent : MonoBehaviour
{
    /// <summary>
    /// A.I. bots to help fill out the scene
    /// </summary>

    [SerializeField]
    private int waitTime = 4;

    public enum State { MoveToPointOne, TravellingToPointTwo, HangAround };
    public State currentState = State.MoveToPointOne;


    public Transform pointOne;
    public BoxCollider fieldCollider;
    

    //navemesh component goes on the A.I bot
    //navmesh must also be created
    private NavMeshAgent agent;
    Animator anim;
    bool isWalking = false;
    bool canHangAround;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent.SetDestination(pointOne.position);
        isWalking = false;
        canHangAround = true;
    }

    void Update()
    {
        AiLogic();
    }

    private void AiLogic()
    {
        if (currentState == State.MoveToPointOne)
        {
            isWalking = true;
            if (Vector3.Distance(transform.position, pointOne.position) < 1)
            {
                currentState = State.TravellingToPointTwo;
                Vector3 targetPosition = RandomPointInBounds(fieldCollider.bounds);
                agent.SetDestination(targetPosition);
                canHangAround = true;
            }
        }

        if (currentState == State.TravellingToPointTwo)
        {
            isWalking = true;
            if (!agent.pathPending)
            {
                //Is the remaining distance very small, implying we've arrived? **Keeping these comments for future reference**
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Check that we have come to a halt or no longer have a path.
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        currentState = State.HangAround;
                    }
                }
            }
        }

        if (currentState == State.HangAround && canHangAround == true)
        {
            isWalking = false;
            StartCoroutine(RelaxingInPlace());

        }

        //anim.SetBool("Walking", isWalking);
        //SETUP ANIMATION LATER
       
    }

    IEnumerator RelaxingInPlace()
    {
        yield return new WaitForSeconds(waitTime);

        currentState = State.MoveToPointOne;
        agent.SetDestination(pointOne.position);
    }



    Vector3 RandomPointInBounds(Bounds bounds)
    {
        Vector3 randomvector3 = new Vector3
                            (Random.Range(bounds.min.x, bounds.max.x),
                             Random.Range(bounds.min.y, bounds.max.y),
                             Random.Range(bounds.min.z, bounds.max.z));
        return randomvector3;
    }
}
