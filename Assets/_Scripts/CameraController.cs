using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float horizontalAxis;
    private float verticalAxis;
    public GameObject player;
    public GameObject target;
    public GameObject gameCamera;
    private Vector3 desiredPlayerRotation;
    private Vector3 pos;
    private Quaternion rotation = Quaternion.identity;
    private Rigidbody _rigidbody;
    private bool hasHorizontalInput;
    private bool hasVerticalInput;
    private bool isMoving;
    private bool mouseIsMoving;
    //private float rotationX = 0.0f;
    private bool isCameraColliding;
    private RaycastHit hit;
    int layerMask;
    public float sensitiveX = 2.0f;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float dodgeSpeed;
    public bool isPlayerInDoors;

    public bool CameraColliding
    {
        get {
            return isCameraColliding;
        }
        set {
            isCameraColliding = value;
        }
    }

   

    // Start is called before the first frame update
    void Start()
    {
        dodgeSpeed = 0.02f;
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        isCameraColliding = false;
        turnSpeed = 1;
        pos = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z);
        this.transform.position = pos;
        _rigidbody = GetComponent<Rigidbody>();
        isPlayerInDoors = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z); ;
        this.transform.position = pos;

        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        hasHorizontalInput = !Mathf.Approximately(horizontalAxis, 0.0f);
        hasVerticalInput = !Mathf.Approximately(verticalAxis, 0.0f);
        
        isMoving = hasHorizontalInput || hasVerticalInput;

        mouseIsMoving = !Mathf.Approximately(Input.GetAxis("Mouse X"), 0.0f);

        if (isCameraColliding && !isPlayerInDoors) {
            gameCamera.transform.position = Vector3.MoveTowards(gameCamera.transform.position, target.transform.position+new Vector3(0,1.55f,0), dodgeSpeed);
        }

        if (isMoving && !mouseIsMoving && !isCameraColliding && !isPlayerInDoors) {
            RotateToFowardDirection();
        } else if(isMoving && isPlayerInDoors)
        {
            RotateOnlytoZAxis();
        }

        if (mouseIsMoving && !isPlayerInDoors) {
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitiveX, 0);
        }
        

        //Debug.Log(Input.GetAxis("Mouse X"));

    }

    void RotateToFowardDirection() {
        Vector3 direction = target.transform.position - transform.position;
        desiredPlayerRotation = Vector3.RotateTowards(transform.forward, direction, turnSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(desiredPlayerRotation);
    }

    void RotateOnlytoZAxis() {
        desiredPlayerRotation = new Vector3(0, 0, 1);
        transform.rotation = Quaternion.LookRotation(desiredPlayerRotation);
    }

    private void FixedUpdate()
    {


    }


}
