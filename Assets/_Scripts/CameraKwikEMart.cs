using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKwikEMart : MonoBehaviour
{

    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public float transitionVelocity;
    public float rotationVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveToPos1() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, pos1.transform.position, transitionVelocity * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, pos1.transform.rotation, rotationVelocity * Time.deltaTime);

    }

    public void MoveToPos2() {
        this.transform.position = Vector3.MoveTowards(this.transform.position, pos2.transform.position, transitionVelocity * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, pos2.transform.rotation, rotationVelocity * Time.deltaTime);
    }
    public void MoveToPos3()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, pos3.transform.position, transitionVelocity * Time.deltaTime);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, pos3.transform.rotation, rotationVelocity * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
