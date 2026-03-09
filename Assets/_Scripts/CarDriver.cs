using UnityEngine;


using System.Collections;

using System.Collections.Generic;



[System.Serializable]
public class AxleInfo{ 
	public WheelCollider leftWheelCollider;
	public WheelCollider rightWheelCollider;
	public GameObject leftWheelMesh;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	
}

public class CarDriver : MonoBehaviour

{
	public List<AxleInfo> axleInfos;
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float brakeTorque;
	public float decelerationForce;
    public bool canDrive;
	private float minZRot;
	private float maxZRot;

    public void ApplyLocalPositionToVisuals(AxleInfo axleInfo){

		Vector3 position;
		Quaternion rotation;
		axleInfo.leftWheelCollider.GetWorldPose(out position, out rotation);
		axleInfo.leftWheelMesh.transform.position = position;
		axleInfo.leftWheelMesh.transform.rotation = rotation;
		axleInfo.rightWheelCollider.GetWorldPose(out position, out rotation);
		axleInfo.rightWheelMesh.transform.position = position;
		axleInfo.rightWheelMesh.transform.rotation = rotation;

	}

    private void Start()
    {
		minZRot = -45.0f;
		maxZRot = 45.0f;

    }

    private void Update()
    {
		LimitRotation();
    }

    void LimitRotation()
    {
		Vector3 carEulerAngles = this.transform.eulerAngles;

		carEulerAngles.z = (carEulerAngles.z > 180) ? carEulerAngles.z - 360 : carEulerAngles.z;
		carEulerAngles.z = Mathf.Clamp(carEulerAngles.z, minZRot, maxZRot);
		this.transform.rotation = Quaternion.Euler(carEulerAngles);

    }

    public virtual void FixedUpdate()

	{
		if (canDrive) {
			float motor = maxMotorTorque * Input.GetAxis("Vertical");
			float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
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

				if (Input.GetKey(KeyCode.Space))
				{
					Brake(axleInfos[i]);
				}

				ApplyLocalPositionToVisuals(axleInfos[i]);

			}
		}

		

	}

	public void Acceleration(AxleInfo axleInfo, float motor){

		if (motor != 0.0f){
			axleInfo.leftWheelCollider.brakeTorque = 0;
			axleInfo.rightWheelCollider.brakeTorque = 0;
			axleInfo.leftWheelCollider.motorTorque = motor;
			axleInfo.rightWheelCollider.motorTorque = motor;

		}
		else{
			Deceleration(axleInfo);
		}

	}



	public void Deceleration(AxleInfo axleInfo){
		axleInfo.leftWheelCollider.brakeTorque = decelerationForce;
		axleInfo.rightWheelCollider.brakeTorque = decelerationForce;

	}

	public void Steering(AxleInfo axleInfo, float steering){
		axleInfo.leftWheelCollider.steerAngle = steering;
		axleInfo.rightWheelCollider.steerAngle = steering;

	}

	public void Brake(AxleInfo axleInfo){
		axleInfo.leftWheelCollider.brakeTorque = brakeTorque;
		axleInfo.rightWheelCollider.brakeTorque = brakeTorque;

	}

}