using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTrigger : MonoBehaviour
{

    private bool isChunckActive;
    //public GameObject player;
    //private Vector3 vectorDistance;
    //public int squaredDistance;

    public bool getTriggerEnter
    {
        get
        {
            return isChunckActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isChunckActive = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log(this.gameObject.name+" - Distancia al cuadrado: " + vectorDistance.sqrMagnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChunckActive =true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChunckActive = false;
        }
    }
}
