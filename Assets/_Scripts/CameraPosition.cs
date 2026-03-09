using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject playerhead;
    private Player _player;
    public GameObject positionCamera1;
    public GameObject positionCamera2;
    public GameObject cubeCameraController;
    private CameraController _cameraController;
    public float switchSpeed;
    public float distance;
    public float direction;
    private float minDistance;
    private bool tempb;
    void Start()
    {
        minDistance = 0.5f;
        Vector3 temps = positionCamera1.transform.position - transform.position;
        distance = temps.magnitude;
        switchSpeed = 1.2f;
        _player = player.GetComponent<Player>();
        _cameraController = cubeCameraController.GetComponent<CameraController>();

    }

    private void ResetDistanceValue()
    {
        Vector3 temps = positionCamera1.transform.position - transform.position;
        distance = temps.magnitude;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        RaycastHit hit1;
        int layer = 1 << 8;
        layer = ~layer;
        if (_player.isDriverMode && !_cameraController.CameraColliding)
        {

            if (Physics.Linecast(transform.position, playerhead.transform.position, out hit,layer))
            {
                Debug.Log("Encontrado: " + hit.collider.gameObject.name);

                if (!hit.collider.gameObject.CompareTag("Spawn"))
                {
                    distance = Mathf.Clamp(hit.distance * 0.85f, minDistance, 5);
                    // /*positionCamera1.*/transform.localPosition = Vector3.Lerp(/*positionCamera1.*/transform.localPosition, (playerhead.transform._position-transform._position) * squared_distance, switchSpeed * 8.0f * Time.deltaTime);

                    transform.position = Vector3.MoveTowards(transform.position, playerhead.transform.position, 0.1f);
                }
               

            }
            else
            {

                if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.forward), out hit1, 1.0f))
                {

                }
                else
                {
                    distance = 5;
                    //positionCamera1.transform.localPosition = Vector3.Lerp(positionCamera1.transform.localPosition, -positionCamera1.transform.forward * squared_distance, switchSpeed * 10.0f * Time.deltaTime);
                    if (!_cameraController.CameraColliding) transform.position = Vector3.MoveTowards(transform.position, positionCamera2.transform.position, switchSpeed * Time.deltaTime);

                }


            }



            //Acercar camara

        }
        else {
            


            if (Physics.Linecast(transform.position, playerhead.transform.position, out hit,layer))
            {
                Debug.Log("Encontrado: " + hit.collider.gameObject.name);
                if (!hit.collider.gameObject.CompareTag("Spawn")) {
                    if (tempb) transform.position = Vector3.MoveTowards(transform.position, playerhead.transform.position, 0.08f);
                    Debug.DrawRay(playerhead.transform.position, (transform.position - playerhead.transform.position), Color.red);
                }
                    // squared_distance = Mathf.Clamp(hit.squared_distance * 0.85f, minDistance, 5);
                    ///*positionCamera1.*/transform.localPosition = Vector3.Lerp(/*positionCamera1.*/transform.localPosition, (playerhead.transform._position-transform._position) * distance, switchSpeed * 8.0f * Time.deltaTime);
               

            }
            else 
            {

                if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.forward),out hit1, 0.8f)){
                    tempb = false;
                    Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.forward), Color.red);
                }
                else
                {
                    distance = 5;
                    //positionCamera1.transform.localPosition = Vector3.Lerp(positionCamera1.transform.localPosition, -positionCamera1.transform.forward * squared_distance, switchSpeed * 10.0f * Time.deltaTime);
                    if (!_cameraController.CameraColliding) transform.position = Vector3.MoveTowards(transform.position, positionCamera1.transform.position, switchSpeed * Time.deltaTime);
                    tempb = true;
                    Debug.DrawRay(playerhead.transform.position, (transform.position - playerhead.transform.position), Color.green);
                    Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.forward), Color.green);
                }

                
            }

           

            //Acercar camara
           
        }


        




    }
}
