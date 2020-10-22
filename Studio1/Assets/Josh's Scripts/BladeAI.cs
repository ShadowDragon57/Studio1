using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeAI : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;
    public bool playerSighted = false;
    public Transform bladeTransform;

    private SphereCollider col;
    private GameObject player;
    private Vector3 playerLoc;

    // Start is called before the first frame update
    public void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerLoc = player.GetComponent<Transform>().position;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    //Used to detect if the player is within the enemies field of view
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Grabs the direction the enemy is currently facing
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            //If it finds that the player is within half the angle for the field of view, it has seen the player
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;


                /*As most object will begin raycasting from the bottom of their feet, there's potential that it might hit the ground
                So transform.position + transform.up brings it approciamately to the middle
                transform.up is essentially 1 unit. Coat is 4.5 units tall. So 2.25 is the middle point of Coat*/
                if (Physics.Raycast(transform.position + (transform.up * 2.25f), direction.normalized, out hit, col.radius))
                {
                    //When the player gets detected
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        playerSighted = true;
                        bladeTransform.position = Vector3.MoveTowards(bladeTransform.position, playerLoc, Time.deltaTime);
                    }
                }
            }
        }
    }
}
