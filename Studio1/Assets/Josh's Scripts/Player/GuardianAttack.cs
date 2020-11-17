using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAttack : MonoBehaviour
{
    //References
    public GameObject rockPrefab;
    GuardianInventory inventory;
    PlayerMovement player;
    GameObject thrownRocks;

    //Private Vars
    Vector3 mouseLoc;
    Vector3 mouseWorldPos;

    //Public Vars
    public Vector3 hitPosition;
    public Quaternion playerRotation;

    void Awake()
    {
        inventory = GetComponent<GuardianInventory>();
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseLoc = Input.mousePosition;

        //Ensures that the spawn location of the object is away from the camera.
        mouseLoc.z = 10;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        #region Collect and Throw Mechanic
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("grabbableRock") && 
                    inventory.rocksCollected < inventory.rockLimit)
                {
                    inventory.rocksCollected += 1;
                }

                if (!hit.transform.gameObject.CompareTag("grabbableRock"))
                {
                    if (inventory.rocksCollected > 0)
                    {
                        inventory.rocksCollected -= 1;
                        Instantiate(rockPrefab, mouseWorldPos, Quaternion.Euler(0,
                            player.GetComponent<Transform>().rotation.y, 0));
                        hitPosition = hit.point;
                    }

                    if (inventory.rocksCollected <= 0)
                    {
                        //Insert Message here
                    }

                    if (inventory.rocksCollected == inventory.rockLimit)
                    {
                        //Insert Message Here
                    }
                }
            }
        }
        #endregion

        #region Finding Spawned Rocks
        if (GameObject.Find("rockPrefab(Clone)") != null)
        {
            thrownRocks = GameObject.Find("rockPrefab(Clone)");
            thrownRocks.name = "Flying Rock";
        }
        #endregion

    }
}
