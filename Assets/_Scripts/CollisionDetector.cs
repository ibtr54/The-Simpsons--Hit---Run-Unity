using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [HideInInspector]
    public bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }

}
