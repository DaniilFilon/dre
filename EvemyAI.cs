using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed; 
    private void Start()
    {
        InitComponentLinks();
        PickNewPatolPoint();
    }
    private void InitComponentLinks()
    { 
      _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        NoticePlayerUpdate();
        ChaseUpdate();
        PatroUpdate();
    }
    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }
    private void PatroUpdate()
    {
        if (!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance == 0)
            {
                PickNewPatolPoint();
            }
        }
    }
     private void PickNewPatolPoint()
     {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
     }
    private void ChaseUpdate()
    {
        if(_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }
}
