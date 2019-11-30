using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField]
    private float reachGoalInSeconds = 5.0f;

    [SerializeField]
    private Vector3 maxGoal = Vector3.zero;

    private Vector3 originalPosition;

    private MoveDirection startDirection = MoveDirection.Up;

    private bool moving = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if(moving)
        {
            return;
        }

        if(transform.localPosition != maxGoal)
        {
            StartCoroutine(MoveOverSeconds(gameObject, maxGoal, reachGoalInSeconds));
            moving = true;
        }
        else if(transform.localPosition == maxGoal)
        {
            StartCoroutine(MoveOverSeconds(gameObject, originalPosition, reachGoalInSeconds));
            moving = true;
        }
    }

    public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.localPosition;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.localPosition = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.localPosition = end;
        moving = false;
    }

    public enum MoveDirection
    {
        Up,
        Down
    }
}
