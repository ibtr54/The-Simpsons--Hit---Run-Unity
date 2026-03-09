using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHudMap : MonoBehaviour
{
    public GameObject playerObject;
    private Player _player;
    private MeshRenderer _mesh;
    private Vector3 relativePos;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //player = GameObject.FindGameObjectWithTag("Player");
        relativePos = new Vector3(0, 0, 0);
        _mesh = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.isDriverMode)
        {
            _mesh.enabled = true;
            this.transform.position = new Vector3(playerObject.transform.position.x, -179.0f, playerObject.transform.position.z);
            this.transform.rotation = Quaternion.Euler(0, playerObject.transform.eulerAngles.y + 180.0f, 0);
        }
        else {
            _mesh.enabled = false;
            
        }
        //this.transform.position = new Vector3(superCube.transform.position.x, -179.0f, superCube.transform.position.z);
       

    }
}
