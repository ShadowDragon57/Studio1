using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        if(Vector3.Distance(transform.position, player.position) >= distance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
