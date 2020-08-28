using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HumanPathDebugger : MonoBehaviour
{
    private NavMeshAgent agent;
    private Color c = Color.white;
    public void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        StartCoroutine(DrawPath());
    }

    IEnumerator DrawPath()
    {
        yield return new WaitForEndOfFrame();
        NavMeshPath path = agent.path;
        if (path.corners.Length < 2)
            yield return new WaitForSeconds(0.1f);    
        switch (path.status)
        {
            case NavMeshPathStatus.PathComplete:
                c = Color.white;
                break;
            case NavMeshPathStatus.PathInvalid:
                c = Color.red;
                break;
            case NavMeshPathStatus.PathPartial:
                c = Color.yellow;
                break;
        }

        Vector3 previousCorner = path.corners[0];

        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            Debug.DrawLine(previousCorner, currentCorner, c);
            previousCorner = currentCorner;
            i++;
        }

    }
}
