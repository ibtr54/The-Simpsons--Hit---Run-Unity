using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPerson : MonoBehaviour
{
    public int checkpointPart;
    public int checkpointSubPart;
    public int _position;
    public NPC_Controller _npcController;
    private int random_direction;
    private const int squared_max_distance = 8000;
    private GameObject player;
    public float speed=2.0f;
    private float temp_speed;
    private Vector3 directionVector;
    public Animator _animator;
    private RaycastHit hit;
    private int squared_distance;
    private Quaternion rotation = Quaternion.identity;
    public float turnspeed=2.0f;
    private Vector3 desiredFoward;
    private Rigidbody _rigidbody;
    private Vector3 gap;
    //private int inverseDirection;
    // Start is called before the first frame update
    void Start()
    {
        random_direction = Random.Range(0, 2);
        temp_speed = speed;
        _npcController = GameObject.FindGameObjectWithTag("NPC_Controller_tag").GetComponent<NPC_Controller>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChunkActive())
        {
            //Destroy(this);
            //Debug.LogError("Desaparecete");
            VerifyDistance();
        }

      //Yeah, "if-else" coding sucks
        if (checkpointPart == 1 && _npcController!=null) {
            switch (checkpointSubPart)
            {
                case 1:
                    //Debug.Log("Hola tio");
                    
                    directionVector = _npcController.CheckpointsNPCPart1_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);
                   
                    
                    transform.Translate(directionVector.normalized * speed * Time.deltaTime,Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    //transform.LookAt(new Vector3(_npcController.CheckpointsNPCPart1_1[_position].position.x, this.transform.position.y, _npcController.CheckpointsNPCPart1_1[_position].position.z));
                    break;

                case 2:
                    //              verifyLimits(2);
                    directionVector = _npcController.CheckpointsNPCPart1_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 3:
                    //              verifyLimits(3);
                    directionVector = _npcController.CheckpointsNPCPart1_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                default:
      //              Destroy(this);
                    break;
            }
      //  }else if(checkpointPart == 3){
      //      //TODO:
      //      //Create checkpoint on Kwik Mart Area
      //  }else if (checkpointPart == 4){
      //      //TODO:
      //      //Create checkpoint on church area
      //  }else if (checkpointPart == 5)
      //  {
      //      //TODO:
      //      //Create checkpoints on school area
        }
        else if (checkpointPart == 2 && _npcController != null) {
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart2_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;

                case 2:
                    directionVector = _npcController.CheckpointsNPCPart2_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;

                case 3:
                    directionVector = _npcController.CheckpointsNPCPart2_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                    
            }

        }else if(checkpointPart == 3 && _npcController != null)
        {
            //TODO: Codigo Pendiente
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart3_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart3_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 3:
                    directionVector = _npcController.CheckpointsNPCPart3_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 4:
                    directionVector = _npcController.CheckpointsNPCPart3_4[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }else if(checkpointPart == 4 && _npcController != null)
        {
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart4_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart4_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 3:
                    directionVector = _npcController.CheckpointsNPCPart4_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }else if(checkpointPart == 5 && _npcController != null)
        {
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart5_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart5_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }else if(checkpointPart == 6 && _npcController != null){
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart6_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart6_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 3:
                    directionVector = _npcController.CheckpointsNPCPart6_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 4:
                    directionVector = _npcController.CheckpointsNPCPart6_4[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }else if (checkpointPart == 7 && _npcController != null)
        {
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart7_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart7_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }else if(checkpointPart == 8 && _npcController != null)
        {
            switch (checkpointSubPart)
            {
                case 1:
                    directionVector = _npcController.CheckpointsNPCPart8_1[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 2:
                    directionVector = _npcController.CheckpointsNPCPart8_2[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
                case 3:
                    directionVector = _npcController.CheckpointsNPCPart8_3[_position].position - this.transform.position;
                    directionVector = new Vector3(directionVector.x, 0.0f, directionVector.z);


                    transform.Translate(directionVector.normalized * speed * Time.deltaTime, Space.World);
                    desiredFoward = Vector3.RotateTowards(transform.forward, directionVector, turnspeed * Time.deltaTime, 0f);
                    rotation = Quaternion.LookRotation(desiredFoward);
                    break;
            }
        }






            //VerifyDistance();
        }

        private void OnAnimatorMove()
    {
        _rigidbody.MoveRotation(rotation);
    }

    private void FixedUpdate()
    {
        //TODO: Fix this Raycast
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f))
        {
            if (!hit.collider.gameObject.CompareTag("Checkpoint")){
                Debug.DrawRay(transform.position+transform.forward, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                temp_speed = speed;
                //_animator.SetBool("isWalking", false);
                //speed = 0.0f;
                //Debug.Log("Did Hit");
            }
           
        }
        else
        {
            Debug.DrawRay(transform.position + transform.forward, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            speed = temp_speed;
            _animator.SetBool("isWalking", true);
        }

    }

    void verifyLimits(int part, int subpart)
    {
        if (part == 1)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart1_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart1_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 3:
                    if (_position >= _npcController.CheckpointsNPCPart1_3.Count - 1)
                    {
                        //random_direction = 0;
                        checkpointPart = 2;
                        checkpointSubPart = 2;
                        _position = 0;
                        _npcController.spawnedNPCPart1--;
                        _npcController.spawnedNPCPart2++;
                    }
                    break;
            }
        }else if (part == 2)
        {
            switch (subpart)
            {
                case 1:
                    if(_position >= _npcController.CheckpointsNPCPart2_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;

                case 2:
                    if(_position >= _npcController.CheckpointsNPCPart2_2.Count - 1)
                    {
                        random_direction = 0;
                    }else if (_position == 0)
                    {
                        checkpointPart = 1;
                        checkpointSubPart = 3;
                        _position = _npcController.CheckpointsNPCPart1_3.Count-1;
                        _npcController.spawnedNPCPart1++;
                        _npcController.spawnedNPCPart2--;
                    }
                    break;

                case 3:
                    if(_position>=_npcController.CheckpointsNPCPart2_3.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }else if (part == 3)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart3_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart3_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 3:
                    if (_position >= _npcController.CheckpointsNPCPart3_3.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 4:
                    if (_position >= _npcController.CheckpointsNPCPart3_4.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;

            }
        }else if(part == 4)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart4_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart4_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 3:
                    if (_position >= _npcController.CheckpointsNPCPart4_3.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }else if(part == 5)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart5_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart5_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }else if (part == 6)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart6_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart6_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 3:
                    if (_position >= _npcController.CheckpointsNPCPart6_3.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 4:
                    if (_position >= _npcController.CheckpointsNPCPart6_4.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }else if (part == 7)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart7_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart7_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }else if (part == 8)
        {
            switch (subpart)
            {
                case 1:
                    if (_position >= _npcController.CheckpointsNPCPart8_1.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 2:
                    if (_position >= _npcController.CheckpointsNPCPart8_2.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
                case 3:
                    if (_position >= _npcController.CheckpointsNPCPart8_3.Count - 1)
                    {
                        random_direction = 0;
                    }
                    break;
            }
        }
       

       if (_position == 0)
        {
            //the code below is temporary
            random_direction = 1;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
        verifyLimits(checkpointPart,checkpointSubPart);

            if (random_direction == 1)
            {
               //sumar
              _position++;
            }
            else
            {
            //restar
            _position--;
            }


        }
    }

    void VerifyDistance()
    {

        gap = player.transform.position-this.transform.position;
        squared_distance = (int)((gap.x * gap.x) + (gap.y*gap.y)+(gap.z*gap.z));

        //Debug.Log("Distancia a Player = " + squared_distance);

        if (squared_distance > squared_max_distance)
        {
            switch (checkpointPart)
            {
                case 1:
                    _npcController.spawnedNPCPart1--;
                    break;
                case 2:
                    _npcController.spawnedNPCPart2--;
                    break;
                case 3:
                    _npcController.spawnedNPCPart3--;
                    break;
                case 4:
                    _npcController.spawnedNPCPart4--;
                    break;
                case 5:
                    _npcController.spawnedNPCPart5--;
                    break;
                case 6:
                    _npcController.spawnedNPCPart6--;
                    break;
                case 7:
                    _npcController.spawnedNPCPart7--;
                    break;
                case 8:
                    _npcController.spawnedNPCPart8--;
                    break;
            }
            
            Destroy(this.gameObject);
        }


    }

    bool isChunkActive()
    {
        return _npcController.activeChunk == checkpointPart ? true : false;
    }

}
