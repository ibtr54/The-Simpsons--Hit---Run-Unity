using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarAI : CarDriver
{
	private NPC_Controller _npcController;
	public int position;
	public Transform rayOrigin;
    public Vector3 targetDirection;
    public float cosAngle;
    public float cosAngleLeft;
    public float cosAngleRight;
    public float _acceleration;
    public float _steering;
	public float max_distance;
    public Rigidbody _rigidbody;
	public float multiplierBrakePedal;
	private int laneSelector;
	private float currentSteering;
	public RaycastHit hit;
	public bool isHitSomething;
	public int layerMask = 1 << 2;
	public GameObject player;
	public const int maxDistanceToPlayer=100000;
	public int squaredDistance;

	// Start is called before the first frame update
	private void Start()
    {
		//maxMotorTorque = 150;
		layerMask = ~layerMask;
		_npcController = GameObject.FindGameObjectWithTag("NPC_Controller_tag").GetComponent<NPC_Controller>();
		_rigidbody = this.GetComponent<Rigidbody>();
		player = GameObject.FindGameObjectWithTag("Player");
		currentSteering = 0;
    }

    public virtual void Update()
    {
		squaredDistance = (int) (player.transform.position - this.transform.position).sqrMagnitude;
		if (squaredDistance > maxDistanceToPlayer) {
			_npcController.spawnedCarWorld--;
			Destroy(this.gameObject);
		}

		VerifyRotation(-80.0f, 80.0f, -80.0f, 80.0f);
    }

	void VerifyRotation(float _minZRot, float _maxZRot, float _minXRot, float _maxXRot) {
		Vector3 carEulerAngles = this.transform.eulerAngles;

		carEulerAngles.z = (carEulerAngles.z > 180) ? carEulerAngles.z - 360 : carEulerAngles.z;
		carEulerAngles.x = (carEulerAngles.x > 180) ? carEulerAngles.x - 360 : carEulerAngles.x;
		if (carEulerAngles.x > _maxXRot ||carEulerAngles.x<_minXRot ||carEulerAngles.z<_minZRot|| carEulerAngles.z > _maxZRot) {
			_npcController.spawnedCarWorld--;
			Destroy(this.gameObject);
		}
		//this.transform.rotation = Quaternion.Euler(carEulerAngles);
	}

    // Update is called once per frame
    public override void FixedUpdate()
    {
		
		targetDirection = (_npcController.CheckpointsCar[position].position - this.transform.position);
		cosAngle = Vector3.Angle(targetDirection, this.transform.forward);
		cosAngleLeft = Vector3.Angle(RotateDegreesYAxis(true, this.transform.forward), targetDirection);
		cosAngleRight = Vector3.Angle(RotateDegreesYAxis(false, this.transform.forward), targetDirection);

	

		_acceleration = cosAngle < cosAngleLeft ? (cosAngle < cosAngleRight ? 1.0f : 1.0f) : 1.0f;
        

        if (cosAngle > 0 && targetDirection.sqrMagnitude>7)
        {
			//Debug.Log("Mayor");
            if (cosAngleLeft > cosAngleRight)
            {
				_steering = Mathf.Clamp((cosAngle / maxSteeringAngle), 0.0f, 1.0f);
			}
            else if(cosAngleLeft<cosAngleRight)
            {
				_steering = -Mathf.Clamp((cosAngle / maxSteeringAngle), 0.0f, 1.0f);
			}
        }
        else
        {
			//Debug.Log("Menor");
			_steering = 0.0f;
        }


		
		if (Physics.Raycast(rayOrigin.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f,layerMask))
		{
			//Debug.DrawRay(rayOrigin.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
			//Debug.Log(hit.collider.gameObject.name);
			if (hit.collider.CompareTag("Checkpoint_Car") || hit.collider.CompareTag("Spawn")) {
				isHitSomething = false;
            }
            else
            {
				isHitSomething = true;
			}
			
		}
		else
		{
			//Debug.DrawRay(rayOrigin.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
			//Debug.Log("Camino Libre");
			isHitSomething = false;
			multiplierBrakePedal = _rigidbody.velocity.magnitude > 6.0f ? -1.0f : (_rigidbody.velocity.magnitude < 1.0f ? 2.0f : 1.0f);
		}

		//Debug.Log("Accerelation: " + _acceleration);
		//Debug.Log("Sterring: " + _steering);
		//Debug.Log("Velocity: " + _rigidbody.velocity.magnitude);

		float motor = isHitSomething? 0:maxMotorTorque */* Input.GetAxis("Vertical")*/_acceleration * multiplierBrakePedal;
		float steering = maxSteeringAngle * /*Input.GetAxis("Horizontal")*/_steering;
		for (int i = 0; i < axleInfos.Count; i++)
		{
			if (axleInfos[i].steering)
			{
				Steering(axleInfos[i], steering);
			}
			if (axleInfos[i].motor)
			{
				Acceleration(axleInfos[i], motor);

			}

			if (isHitSomething)
			{
				Brake(axleInfos[i]);
			}

			ApplyLocalPositionToVisuals(axleInfos[i]);

		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint_Car"))
        {
			//TODO: Program implementation for make car able to change row.
			if(position == 54)
            {
				//laneSelector = Random.Range(0, 2);
				laneSelector = 1;
                if (laneSelector == 0)
                {
					position++;
                }
                else
                {
					position += 4;
                }
            }else if(position == 57)
            {
				position += 4;
            }else if(position == 119)
            {
				laneSelector = Random.Range(0, 2);
				if(laneSelector == 0)
                {
					position++;

                }
                else
                {
					position += 40;
                }
            }else if (position == 158)
            {
				position += 43;
            }else if(position == 226)
            {
				laneSelector = Random.Range(0, 2);
                if (laneSelector == 0)
                {
					position += 42;
                }
                else
                {
					position++;
                }
            }else if(position==267){
				position += 40;
            }else if (position == 362)
            {
				laneSelector = Random.Range(0, 2);
                if (laneSelector == 0)
                {
					position += 5;
                }
                else
                {
					position++;
                }
                
            }else if (position == 366)
            {
				position += 5;
            }
				
            else
            {
				position++;
			}
			
			VerifyLimits();
        }
    }


	private void VerifyLimits()
    {
		if(position > _npcController.CheckpointsCar.Count - 1)
        {
			position = 0;
        }
    }

	public Vector3 RotateDegreesYAxis(bool isNegative, Vector3 vector)
    {
        //cos(5)=0.9962 aprox
        //sin(5)=0.0871 aprox
        //cos(15)=0.9659
        //sin(15)=0.2588
        
			return isNegative ? new Vector3(0.9659f * vector.x - 0.2588f * vector.z, vector.y, 0.2588f * vector.x + 0.9659f * vector.z) : new Vector3(0.9659f * vector.x + 0.2588f * vector.z, vector.y, 0.9659f * vector.z - 0.2588f * vector.x);
		
    }


	float GetRandianAngleVector3(Vector3 A, Vector3 B)
    {
		


		//TODO: Mathf functions are too slow, change them for a quick algorithm 
		return Mathf.Acos(cosAngleVector3(A,B));
    }

	float cosAngleVector3(Vector3 A, Vector3 B)
    {
		return (DotProductVector3(A, B)) / (A.magnitude * B.magnitude);

	}

	float DotProductVector3(Vector3 A, Vector3 B)
    {
		return (A.x * B.x)+(A.y*B.y)+(A.z * A.z);
    }

	float q_Acos(float x)
    {
		return 0;
    }


}
