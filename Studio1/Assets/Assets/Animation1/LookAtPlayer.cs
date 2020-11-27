using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public float triggerRange = 20;
    private float angle = 110;
    public Transform playerPosition;
    public Animator anim;
    public GameObject lookAt;
    public bool detectedPlayer;

    void Update()
    {
        float distance = Vector3.Distance(playerPosition.position, transform.position);
        Vector3 direction = playerPosition.position - transform.position;
        float sightAngle = Vector3.Angle(direction, transform.position);

        //if (sightAngle <= angle / 2)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(transform.position + (transform.up * 4.5f), direction.normalized, out hit, triggerRange))
        //    {
        //        if(hit.collider.gameObject == lookAt)
        //        {
        //            detectedPlayer = true;
        //        }
        //    }
        //}

        if (distance <= triggerRange)
        {
            Idle();
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }

        if(distance > triggerRange)
        {
            Alert();
        }
    }

    IEnumerator idleRoutine()
    {
        anim.SetBool("IdleNPC", true);
        yield return new WaitForSeconds(2);
        anim.enabled = false;
    }

    IEnumerator alertRoutine()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(2);
        anim.SetBool("IdleNPC", false);
    }
    void Idle()
    {
        StartCoroutine(nameof(idleRoutine));
    }

    void Alert()
    {
        StartCoroutine(nameof(alertRoutine));
    }
}
