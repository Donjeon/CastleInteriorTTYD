using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTrailScriptLeft : MonoBehaviour
{
    public float lifeTime = 0.4f;
    public GameObject player;

    public float adjustX;
    public float adjustY;
    public float adjustZ;

    private Vector3 playerPosition;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
        player = GameObject.Find("Player");

        playerPosition = new Vector3(player.transform.position.x + adjustX, player.transform.position.y + adjustY, player.transform.position.z + adjustZ);
        transform.rotation = new Quaternion(0,0,0,0);
        transform.position = playerPosition;
       
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + -0.06f, transform.position.y, transform.position.z);
    }
    
}
