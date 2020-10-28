using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiMovement : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    private float moveSpeed, distance;
    private bool playerContact = false;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
        distance = 0;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerContact = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        //Looks at player and begins to move towards them
        if(Vector3.Distance(transform.position, player.position) >= distance && playerContact != true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        //Begin Attack Animations based what their tag is. Such as spears, swords etc
        if (gameObject.tag == "Sword")
        {
            //SwordAI
        }
    }
}
