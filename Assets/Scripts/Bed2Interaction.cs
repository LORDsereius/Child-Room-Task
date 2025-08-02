using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Bed2Interaction : MonoBehaviour, IInteractable
{
    [SerializeField]    private Transform interactionPosition;
    [SerializeField]    private float interactionRadius = 0.1f;
    [SerializeField]    private float outlineThickness = 0.005f;
    [SerializeField]    private Material outline;
    [SerializeField]    private GameObject Child;
    private bool isWaitingForArrival;
    private Animator animator;
    private PointAndClickMovement playerMovement;
    private NavMeshAgent agent;
    public void Awake()
    {
        playerMovement = Child.GetComponent<PointAndClickMovement>();
        animator = Child.GetComponent<Animator>();
        agent = Child.GetComponent<NavMeshAgent>();
    }
    public void Interact()
    {
        if (interactionPosition == null)
        {
            Debug.LogWarning("Interaction position is not set for " + gameObject.name);
            return;
        }

        if (playerMovement != null)
        {
            playerMovement.MoveTo(interactionPosition.position);
            isWaitingForArrival = true;
        }
    }

    private void Update()
    {
        if (isWaitingForArrival && playerMovement != null)
        {
            float distance = Vector3.Distance(playerMovement.transform.position, interactionPosition.position);
            if (distance <= interactionRadius)
            {
                isWaitingForArrival = false;
                OnArrivedAtBed();
            }
        }
    }

    private void OnArrivedAtBed()
    {
        agent.enabled = false;
        Child.transform.localRotation = Quaternion.Euler(0, 0, 0);
        agent.enabled = true;
        animator.SetTrigger("Bed2");
    }
    public void CancelInteraction()
    {
        if (!isWaitingForArrival)
        {
            animator.SetTrigger("StopBed2");
            playerMovement.TemporarilyDisableMovement(2f);
        }
    }

    public void OnHoverEnter()
    {
        outline.SetFloat("_Thickness", outlineThickness);
    }

    public void OnHoverExit()
    {
        outline.SetFloat("_Thickness", 0);
    }
}
