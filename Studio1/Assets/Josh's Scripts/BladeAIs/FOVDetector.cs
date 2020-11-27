<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetector : MonoBehaviour
{
    BladeAIIDK bladeAI;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool sighted;

    //Holds a list of currently visible Targets
    public List<Transform> visibleTargets = new List<Transform>();

    void Awake()
    {
        bladeAI = GameObject.Find("Blade").GetComponent<BladeAIIDK>();
        sighted = false;
    }

    void Start()
    {
        StartCoroutine(nameof(FindTargetsWithDelay), 0.2f);    
    }

    void Update()
    {
        if (!bladeAI.playerSighted)
        {
            sighted = false;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            //Finds the direction the object is looking in 
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //Gets 2 directions, finds the angles between the two outgoing lines to check if there's something between
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                //Checks if there're any obstacles in the way
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (!sighted)
                    {
                        visibleTargets.Add(target);
                        bladeAI.playerSighted = true;
                        sighted = true;
                    }
                }
            }
        }
    }

    //Direction of the angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleisGlobal)
    {
        if (!angleisGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        //Trigonometry :D
        //Because Unity starts its angle from 90 rather than 0. We'll need to suppose x is the currentAngle
        //If we minus 90 each time, we get the angle we need for trigonometry

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetector : MonoBehaviour
{
    BladeAIIDK bladeAI;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool sighted;

    //Holds a list of currently visible Targets
    public List<Transform> visibleTargets = new List<Transform>();

    void Awake()
    {
        bladeAI = GameObject.Find("Blade").GetComponent<BladeAIIDK>();
        sighted = false;
    }

    void Start()
    {
        StartCoroutine(nameof(FindTargetsWithDelay), 0.2f);    
    }

    void Update()
    {
        if (!bladeAI.playerSighted)
        {
            sighted = false;
        }
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            //Finds the direction the object is looking in 
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //Gets 2 directions, finds the angles between the two outgoing lines to check if there's something between
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                //Checks if there're any obstacles in the way
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (!sighted)
                    {
                        visibleTargets.Add(target);
                        bladeAI.playerSighted = true;
                        sighted = true;
                    }
                }
            }
        }
    }

    //Direction of the angle
    public Vector3 DirFromAngle(float angleInDegrees, bool angleisGlobal)
    {
        if (!angleisGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        //Trigonometry :D
        //Because Unity starts its angle from 90 rather than 0. We'll need to suppose x is the currentAngle
        //If we minus 90 each time, we get the angle we need for trigonometry

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
>>>>>>> Stashed changes
