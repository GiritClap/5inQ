using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patrol : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject[] targets;
    public int point = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!nav.pathPending && nav.remainingDistance < 2f) next();
    }

    public void next()
    {
        if (targets.Length == 0) return;

        nav.destination = targets[point].transform.position;
        point = (point + 1) % targets.Length;
    }
}
