/* NavMeshPart7.unity 
 * LevelGenerator.cs
 * LevelBaker.cs
 * PatrolAgentProcedural.cs
 */


using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    [SerializeField]
    private List<Transform> goals = new List<Transform>();

    private int goalTracker = 0;

    [SerializeField]
    [Range(2, 200)]
    private int howManyFloors = 10;

    private int floorTracker = 0;
    
    [SerializeField]
    [Range(0, 5.0f)]
    private float buildFloorInSeconds = 2;

    private float buildTimer = 0;

    private Vector3 trackedPosition = Vector3.zero;

    private PatrolAgentProcedural patrolAgent;


    void Awake()
    {
        buildTimer = buildFloorInSeconds;
    }

    void Update()
    {
        if(floorTracker >= howManyFloors)
        {
            if(patrolAgent != null)
            {
                if(patrolAgent.PointCount == 0)
                {
                    patrolAgent.AddPoints(goals.ToArray());
                    LevelBaker.Instance.BakeLevel();
                    patrolAgent.enabled = true;
                }
            }
            return;
        }

        if(buildTimer >= buildFloorInSeconds)
        {
            GameObject newFloor = Instantiate(floorPrefab, trackedPosition, Quaternion.identity);
            newFloor.transform.parent = transform;

            // add agent
            if(floorTracker == 0)
            {
                patrolAgent = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity)
                    .GetComponent<PatrolAgentProcedural>();
                patrolAgent.enabled = false;
                patrolAgent.transform.parent = transform;
            }

            // add goal
            if(floorTracker % 2 == 0)
            {
                GameObject goal = Instantiate(goalPrefab, 
                    new Vector3(trackedPosition.x, trackedPosition.y + 0.1f, trackedPosition.z), 
                    Quaternion.identity);
                TextMeshPro goalText = goal.GetComponentInChildren<TextMeshPro>();
                goalText.text = $"PATROL POINT {++goalTracker}";
                goal.transform.parent = transform;

                goals.Add(goal.transform);
            }

            trackedPosition = new Vector3(trackedPosition.x, trackedPosition.y, 
                trackedPosition.z - newFloor.transform.localScale.z);

            floorTracker++;
            buildTimer = 0;
        }
        else
        {
            buildTimer += Time.deltaTime * 1.0f;
        }

    }
}
