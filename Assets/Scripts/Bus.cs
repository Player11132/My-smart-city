using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Bus : MonoBehaviour
{
    public PathCreator path;
    public float speed; 
   float traveled;

    private void Update()
    {
        if (path != null)
        {
            traveled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(traveled);
            transform.rotation = path.path.GetRotationAtDistance(traveled);
        }
    }

}
