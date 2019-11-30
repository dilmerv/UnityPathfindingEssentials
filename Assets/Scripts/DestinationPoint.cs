using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class DestinationPoint : MonoBehaviour
{
    [SerializeField]
    private float force = 10.0f;

    [SerializeField]
    private TextMeshPro overlay;

    private string originalText;

    [SerializeField]
    private float blockCollisionForSeconds = 1.5f;

    private float blockCollisionTimer = 1.5f;

    private NavMeshAgent agent;

    private void Update() 
    {
        if(blockCollisionTimer >= blockCollisionForSeconds)
        {
            if(agent != null)
            {
                agent.enabled = true;
                blockCollisionTimer = 0;
                agent = null;
            }
        }
        else if(agent != null)
        {
            blockCollisionTimer += 1.0f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "Player")
        {
            originalText = overlay.text;
            overlay.text = "ACTIVATED";

            agent = other.gameObject.GetComponent<NavMeshAgent>();
            agent.enabled = false;

            other.gameObject.GetComponent<Rigidbody>()
                .AddForce(Vector3.up * force, ForceMode.Force);
            other.gameObject.GetComponent<Rigidbody>()
                .AddForce(other.gameObject.transform.forward * force, ForceMode.Force);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.name == "Player")
        {
            overlay.text = originalText;
            blockCollisionTimer = 0;
        }
    }
}
