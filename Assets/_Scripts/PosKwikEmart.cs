using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosKwikEmart : MonoBehaviour
{
    public CameraKwikEMart cameraKwikEmart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(this.gameObject.name);
        if (other.CompareTag("Player")) {
            if (this.gameObject.name.Equals("Trigger 1")) cameraKwikEmart.MoveToPos2();
            if (this.gameObject.name.Equals("Trigger 2")) cameraKwikEmart.MoveToPos1();
            if (this.gameObject.name.Equals("Trigger 3")) cameraKwikEmart.MoveToPos3();
        }
        
    }
}
