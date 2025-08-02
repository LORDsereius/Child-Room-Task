using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.InputSystem;

public class PointAndClickMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField]
    private ParticleSystem ClickEffect;
    private bool canMove = true;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (animator != null && agent != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
    }
    public void MoveTo(Vector3 destination)
    {
        if (!canMove) return;
        agent.SetDestination(destination);
        if (ClickEffect != null)
        {
            Instantiate(ClickEffect, destination, ClickEffect.transform.rotation);
        }
    }
        public void TemporarilyDisableMovement(float duration)
    {
        StartCoroutine(DisableMovementForSeconds(duration));
    }

    private IEnumerator DisableMovementForSeconds(float seconds)
    {
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }
}