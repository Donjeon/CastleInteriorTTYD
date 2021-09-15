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


    //TransformXYZ functions
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
        


        ///*
        void CameraBounding()
        {
            
            //X axis
            if (cameraCube.transform.position.x >= maxX)
            {
                SetTransformX(maxX + adjustMax);
            }

            if (cameraCube.transform.position.x <= minX)
            {
                SetTransformX(minX + adjustMin);
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

//Camera Bounding Box Faces
//public GameObject top, bottom, left, right, front, back;
//public GameObject interior;

/*
//Camera box bounding
private void OnTriggerStay(Collider boxSide)
{
    if (boxSide.gameObject == top)
    {
        SetTransformY(boxSide.transform.position.y - 1);//top'sY -0.01
    }

    if (boxSide.gameObject == bottom)
    {
        SetTransformY(boxSide.transform.position.y + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == left)
    {
        SetTransformY(boxSide.transform.position.x - 1);//top'sY -0.01
    }

    if (boxSide.gameObject == right)
    {
        SetTransformY(boxSide.transform.position.x + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == front)
    {
        SetTransformY(boxSide.transform.position.z + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == back)
    {
        SetTransformY(boxSide.transform.position.z - 1);//top'sY -0.01
    }

}
//*/
/*
private void OnTriggerExit(Collider col)
{
    if(cameraCube.transform.position.x < 48)
    {
        SetTransformX(48.01f);
    }
}

private void OnTriggerStay(Collider boxSide)
{
    if (boxSide.gameObject == interior)
    {
        transform.position = (transform.position - player.transform.position).normalized + player.transform.position;
        transform.Translate(XfromPlayer, YfromPlayer, ZfromPlayer);
    }

    /*
    if (boxSide.gameObject == top)
    {
        SetTransformY(boxSide.transform.position.y - 1);//top'sY -0.01
    }

    if (boxSide.gameObject == bottom)
    {
        SetTransformY(boxSide.transform.position.y + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == left)
    {
        SetTransformX(boxSide.transform.position.x - 1);//top'sY -0.01
    }

    if (boxSide.gameObject == right)
    {
        SetTransformX(boxSide.transform.position.x + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == front)
    {
        SetTransformZ(boxSide.transform.position.z + 1);//top'sY -0.01
    }

    if (boxSide.gameObject == back)
    {
        SetTransformZ(boxSide.transform.position.z - 1);//top'sY -0.01
    }

}
*/
