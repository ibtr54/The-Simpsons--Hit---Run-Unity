using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalAxis;
    private float verticalAxis;
    private float joy_movement;
    private Animator animatorControl;
    private Vector3 axis;
    private Rigidbody _rigidbody;
    private Vector3 ray;
    private bool canJump;
    private int layerMask;
    private Quaternion rotation = Quaternion.identity;
    private float gravity = -9.8f;
    private float tempSpeed;
    public bool isDriverMode;
    public bool canMove;
    private Vector3 startPos;
    private bool FPressed;
    public GameObject cubeCamera;
    private Collider m_ObjectCollider;
    private Vector3 desiredFoward1;
    private GameObject car;
    private Vector3 g;
    private Vector3 z;
    bool enteringCar;
    private Collider _other;
    private CarDriver _cardriver;
    private Animator _carAnimator;

    [SerializeField]
    private float turnSpeed;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        turnSpeed = 30;
        speed = 3.5f;
        horizontalAxis = 0.0f;
        verticalAxis = 0.0f;
        animatorControl = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        canJump = true;
        tempSpeed = speed;
        FPressed = false;
        m_ObjectCollider = GetComponent<Collider>();
        m_ObjectCollider.isTrigger = false;
        g = new Vector3(0, 0, 0);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        ray = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        if (Physics.Raycast(ray, transform.TransformDirection(Vector3.down), out hit, 0.55f, layerMask))
        {
            //Debug.DrawRay(ray, transform.TransformDirection(Vector3.down) * hit.squared_distance, Color.red);
            canJump = true;

        }
        else
        {
            //Debug.DrawRay(ray, transform.TransformDirection(Vector3.down) * hit.squared_distance, Color.green);
            canJump = false;
            animatorControl.SetBool("Jump", canJump);
            StopCoroutine("Jump");

        }
        if (!isDriverMode && canMove)
        {
            

            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");

            joy_movement = Clmap_Joy(Mathf.Sqrt(Absolute_Value(horizontalAxis) + Absolute_Value(verticalAxis)));
            animatorControl.SetFloat("v_Axis", joy_movement);
            axis.Set(horizontalAxis, 0, verticalAxis);
            axis.Normalize();




            Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, cubeCamera.transform.rotation * axis, turnSpeed * Time.deltaTime, 0f);
            rotation = Quaternion.LookRotation(desiredFoward);

            if (joy_movement > 0.3 && joy_movement < 0.69)
            {
                transform.Translate(Vector3.forward * joy_movement * (speed / 0.4f) /*TODO: Configure the speed, is '*' operator not '/' */* Time.deltaTime);
            }
            else if (joy_movement > 0.7)
            {
                transform.Translate(Vector3.forward * joy_movement * speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {

                //Jump();

                animatorControl.SetBool("Jump", canJump);

                StartCoroutine("Jump");


            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                animatorControl.SetBool("IsKick", true);

            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                animatorControl.SetBool("IsKick", false);

            }




            //if (isPlaying(animatorControl, "Kicking(1)"))
            //{
            //    tempSpeed = speed;
            //    speed = 0;

            //}
            //else {
            //    speed = tempSpeed;
            //}
        }
        else {


            

            if (Input.GetKeyDown(KeyCode.F) && canMove)
            {
                FPressed = true;
                animatorControl.SetBool("charEnterCar", false);
                _carAnimator.SetBool("PlayerEnter", true);



            }
            if (FPressed && canMove)
            {
                transform.Translate(Vector3.left * 0.25f * Time.deltaTime);
                
                

                if (animatorControl.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Happy Idle")
                {
                    _carAnimator.SetBool("PlayerEnter", false);
                    _carAnimator.SetBool("PlayerSit", true);
                    Debug.Log("Ejecunatdo modo a pie");
                    m_ObjectCollider.isTrigger = false;
                    _rigidbody.isKinematic = false;
                    FPressed = false;
                    isDriverMode = false;
                    _cardriver.canDrive = isDriverMode;
                    
                    //_carAnimator.SetBool("PlayerSit", false);
                    transform.SetParent(null);
                }
            }else if(canMove){
                _carAnimator.SetBool("PlayerEnter", false);
                _carAnimator.SetBool("PlayerSit", false);
                transform.SetParent(_other.transform.parent, true);
                transform.position = Vector3.Lerp(transform.position, _other.transform.position, speed * 20 * Time.deltaTime);
                transform.rotation = _other.transform.rotation;
            }
        }
        

        

               
    }
    IEnumerator Jump() {
        yield return new WaitForSeconds (0.6f);
        _rigidbody.AddForce(Vector3.up * 40000);
    }

    IEnumerator tn() {
        
        yield return new WaitForSeconds(2.5f);
        _carAnimator.SetBool("PlayerEnter", false);
        transform.position = Vector3.Lerp(transform.position, z, speed * 20 * Time.deltaTime);
        yield return new WaitForSeconds(1.7f);
       
        _carAnimator.SetBool("PlayerSit", true);
        

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (isDriverMode)
        {
            if (animatorControl.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Driving")
            {
                //Debug.Log(other.transform._position);
                //this.transform._position = other.transform._position;
                //this.transform.rotation = other.transform.rotation;
                //transform._position = Vector3.Lerp(transform._position, other.transform._position, speed * 20 * Time.deltaTime);
                //this.transform.SetParent(other.transform.parent,true);

            }

            

        }
        else if (!isDriverMode) {

            
                if (Input.GetKeyDown(KeyCode.F) && other.tag == "Car")
                {
                    FPressed = true;
                    _cardriver = other.GetComponentInParent<CarDriver>();
                _carAnimator = other.GetComponentInParent<Animator>();
                _carAnimator.SetBool("PlayerEnter", false);
                _carAnimator.SetBool("PlayerSit", false);

            }
            if (FPressed) {
                //car = other.gameObject;

                if (other.tag == "Car") {
                    //startPos = this.transform._position;
                    
                    g = other.transform.position;
                    Vector3 direction = other.transform.position - this.transform.position;
                    direction.Set(direction.x, 0.0f, direction.z);
                    //desiredFoward1 = Vector3.RotateTowards(transform.forward, direction.normalized, turnSpeed * Time.deltaTime, 0f);
                    //rotation = Quaternion.LookRotation(desiredFoward1);
                    transform.rotation = Quaternion.LookRotation(direction);
                    animatorControl.SetFloat("v_Axis", 0.9f);
                    transform.position = Vector3.MoveTowards(transform.position, other.transform.position, speed * Time.deltaTime);


                    Vector3 desiredFoward2 = Vector3.RotateTowards(transform.forward, other.transform.rotation * Vector3.forward, turnSpeed * Time.deltaTime, 0f);
                    transform.rotation = Quaternion.LookRotation(desiredFoward2);
                }
                

                if (other.tag == "DoorLTrigger")
                {
                    
                    transform.rotation = other.transform.rotation;
                    animatorControl.applyRootMotion = true;
                    animatorControl.SetBool("charEnterCar", true);
                    _carAnimator.SetBool("PlayerEnter", true);
                    _carAnimator.SetBool("PlayerSit", false);
                    //Vector3 desiredFoward3 = Vector3.RotateTowards(transform.forward, other.transform.rotation * Vector3.forward, 5.0f * Time.deltaTime, 0f);
                    //rotation = Quaternion.LookRotation(desiredFoward3);



                    m_ObjectCollider.isTrigger = true;
                    _rigidbody.isKinematic = true;
                    //transform._position = Vector3.Lerp(transform._position, g, speed * 10 * Time.deltaTime);

                    z = new Vector3(other.transform.position.x,other.transform.position.y,other.transform.position.z);

                    StartCoroutine(tn());

                    

                    //FPressed = false;
                    if (isPlaying(animatorControl, "Driving")) {
                        
                        Debug.Log("Ejecunatdo modo vehiculo");
                        _carAnimator.SetBool("PlayerEnter", false);
                        _carAnimator.SetBool("PlayerSit", false);
                        //StopCoroutine(Subir());
                        this.transform.SetParent(other.transform.parent, true);
                        isDriverMode = true;
                        _cardriver.canDrive = isDriverMode;
                        FPressed = false;
                        StopCoroutine(tn());
                        _other = other;
                        //this.transform.SetParent(other.transform.parent);
                    }

                    
                }
            }

            

        }



    }

    

    private void OnTriggerEnter(Collider other)
    {
        ////Debug.Log("caracter puede subirse");
        //if (Input.GetKeyDown(KeyCode.F) && other.tag == "Car")
        //{
        //    FPressed = true;

        //}

        //if (FPressed) {
        //    car = other.gameObject;
        //    isDriverMode = true;
        //    //startPos = this.transform._position;

        //    transform._position = Vector3.MoveTowards(transform._position, car.transform._position, speed*Time.deltaTime);
        //    animatorControl.SetFloat("v_Axis", 0.9f);
        //    Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, other.transform.rotation * Vector3.right, turnSpeed * Time.deltaTime, 0f);
        //    rotation = Quaternion.LookRotation(desiredFoward);
        //}
    }
    bool isPlaying(Animator anim, string stateName)
    {
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        //   return true;
        //else
        //    return false;
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f ? true : false;
    }

    private void OnAnimatorMove()
    {
        //_rigidbody.MovePosition(_rigidbody._position + axis * animatorControl.deltaPosition.magnitude);

        _rigidbody.MoveRotation(rotation);


    }

    float Clmap_Joy(float value) {
        return value > 1 ? 1 : value;
    }
   
    float Absolute_Value(float value) {
       return value < 0 ? -value : value;


    }
}
