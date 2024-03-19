using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 position;

    public void updatePosition()
    {
        position = transform.position;  
    }

    public Vector3 getCheckpointPostion()
    {
        return position;
    }
}
