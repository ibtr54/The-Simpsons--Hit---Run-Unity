using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHudMapManager : MonoBehaviour
{
    public GameObject playerObject;

    private Vector3 relativePos;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        relativePos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(superCube.transform.position.x, -179.0f, superCube.transform.position.z);
        this.transform.position = new Vector3(playerObject.transform.position.x, -39.0f, playerObject.transform.position.z);
        this.transform.rotation = Quaternion.Euler(90,playerObject.transform.eulerAngles.y,playerObject.transform.eulerAngles.z);

    }
}
