using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Player object
    public GameObject player;

    //Camera Control
    public Rigidbody cameraCube;

    public float XfromPlayer, YfromPlayer, ZfromPlayer;

    public float maxX;
    public float minX;

    public float maxY;
    public float minY;

    public float maxZ;
    public float minZ;

    private float adjustMax = -0.01f;
    private float adjustMin = 0.01f;


    void SetTransformX(float n)
    {
        cameraCube.transform.position = new Vector3(n, cameraCube.transform.position.y, cameraCube.transform.position.z);
    }

    void SetTransformY(float n)
    {
        cameraCube.transform.position = new Vector3(cameraCube.transform.position.x, n, cameraCube.transform.position.z);
    }

    void SetTransformZ(float n)
    {
        cameraCube.transform.position = new Vector3(cameraCube.transform.position.x, cameraCube.transform.position.y, n);
    }

    void Update()
    {
        transform.position = (transform.position - player.transform.position).normalized + player.transform.position;
        transform.Translate(XfromPlayer, YfromPlayer, ZfromPlayer);


        void CameraBounding()
        {
            //X axis
            if (cameraCube.transform.position.y >= maxX)
            {
                SetTransformY(maxX + adjustMax);
            }

            if (cameraCube.transform.position.y <= minX)
            {
                SetTransformY(minX + adjustMin);
            }

            //Y axis
            if (cameraCube.transform.position.y >= maxY)
            {
                SetTransformY(maxY + adjustMax);
            }

            if (cameraCube.transform.position.y <= minY)
            {
                SetTransformY(minY + adjustMin);
            }

            //Z axis
            if (cameraCube.transform.position.z <= minZ)
            {
                SetTransformZ(minZ + adjustMin);
            }

            if (cameraCube.transform.position.z >= maxZ)
            {
                SetTransformZ(maxZ + adjustMax);
            }

            


        }
        CameraBounding();
        
    }
}
