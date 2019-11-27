using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    [SerializeField]
    private Transform goal;

    void Start()
    {
        var playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.destination = goal.position;
    }
}
