using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform playerSpawn;

    public float speed = 10f;
    private int currentWaypoint = 0;
    public float targetDistance = 0.1f;
    public float rotationSpeed = 1;
    
    

    private void Awake()
    {
        currentWaypoint = 1;
    }

    private void Update()
    {
        transform.parent.position += (waypoints[currentWaypoint].position - transform.parent.position).normalized  * Time.deltaTime * speed;
        var lookPos = waypoints[currentWaypoint].position - transform.parent.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, rotation, Time.deltaTime * rotationSpeed);


        if (AmIClose())
            ChangeWaypoint();
    }

    private bool AmIClose()
    {
        float distance = Vector3.Distance(transform.parent.position, waypoints[currentWaypoint].position);

        if (distance <= targetDistance)
        {
            transform.parent.position = waypoints[currentWaypoint].position;
            return true;
        }

        return false;
    }

    private void ChangeWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        //transform.parent.LookAt(waypoints[currentWaypoint]);
        //transform.parent.Rotate(0, 180, 0);
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Im triggering: " + other.gameObject);

        if (other.gameObject.layer == Layers.Player)
        {
            CharacterController cc = other.transform.GetComponent<CharacterController>();
            cc.enabled = false;
            other.transform.position = playerSpawn.position;
            cc.enabled = true;
        }
        else if (other.gameObject.layer == Layers.Spawn)
        {
            ItemSpawn spawn = other.gameObject.GetComponent<ItemSpawn>();
            spawn.isInStorm = true;

            if (!spawn.isUnique)
                InventoryItemSpawner.Instance.AddSpawnToStorm(spawn);
        }     
        else if (other.gameObject.layer == Layers.InteractableObject)
        {
            if (other.gameObject.CompareTag(Tags.PickableItem))
            {
                InteractableInvetoryItem item = other.gameObject.GetComponent<InteractableInvetoryItem>();

                if (item.canBeDestroyed)
                    InventoryItemSpawner.Instance.RemoveItemDueStorm(item);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.Spawn)
        {
            Debug.Log("OnTriggerExit spawn");
            ItemSpawn spawn = other.gameObject.GetComponent<ItemSpawn>();

            if (!spawn.isUnique)
            {
                spawn.isInStorm = false;
                InventoryItemSpawner.Instance.RemoveSpawnFromStorm(spawn);
            }
        }
    }

}
