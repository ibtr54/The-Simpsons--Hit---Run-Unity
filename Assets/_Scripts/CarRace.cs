using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRace : CarAI
{
    
    public List<Transform> Checkpoints;
    public Vector3 finishLine;
    private bool isCarAtFinishLine;
    private Transform rayCenterLeft;
    private Transform rayCenterRight;
    private Transform rayLeft;
    private Transform rayRight;
    private bool isRayCLHit;
    private bool isRayCRHit;
    private bool isRayLHit;
    private bool isRayRHit;
    private bool isAnyRayColliding;
    public bool canStart = false;
    public bool raceMode;
    public int totalLaps;
    private int completedLaps;
    private float timeBeforeDestroy = 3.0f;
    private float elapsedTime = 0.0f;
    // Update is called once per frame
    public override void FixedUpdate()
    {
         if( canStart ) {

            isRayCLHit = Physics.Raycast(rayCenterLeft.position, this.transform.forward, 5.0f);
            isRayCRHit = Physics.Raycast(rayCenterRight.position, this.transform.forward, 5.0f);
            isRayLHit = Physics.Raycast(rayLeft.position, this.transform.forward, 5.0f);
            isRayRHit = Physics.Raycast(rayRight.position, this.transform.forward, 5.0f);
            isAnyRayColliding = isRayCLHit || isRayCRHit || isRayLHit || isRayRHit;

            targetDirection = Checkpoints[position].position - this.transform.position;
            targetDirection.y = 0;
            cosAngle = Vector3.Angle(targetDirection, this.transform.forward);
            cosAngleLeft = Vector3.Angle(RotateDegreesYAxis(true, this.transform.forward), targetDirection);
            cosAngleRight = Vector3.Angle(RotateDegreesYAxis(false, this.transform.forward), targetDirection);



            _acceleration =/* cosAngle < cosAngleLeft ? (cosAngle < cosAngleRight ? 1.0f : 1.0f) :*/ 1.0f;

            //Checking Raycast
            if (isAnyRayColliding)
            {

                _steering = isRayLHit ? 1.0f : isRayRHit ? -1.0f : 0.0f;

                multiplierBrakePedal = isRayCLHit || isRayCRHit ? -1.0f : 1.0f;
            }


            if (cosAngle > 0 && targetDirection.sqrMagnitude > 7.0f && !isAnyRayColliding)
            {
                //Debug.Log("Mayor");
                if (cosAngleLeft > cosAngleRight)
                {
                    _steering = Mathf.Clamp((cosAngle / maxSteeringAngle), 0.0f, 1.0f);
                }
                else if (cosAngleLeft < cosAngleRight)
                {
                    _steering = -Mathf.Clamp((cosAngle / maxSteeringAngle), 0.0f, 1.0f);
                }
            }
            else if (!isAnyRayColliding)
            {
                //Debug.Log("Menor");
                _steering = 0.0f;
            }



            if (Physics.Raycast(rayOrigin.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, layerMask))
            {
                //Debug.DrawRay(rayOrigin.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Checkpoint_Car") || hit.collider.CompareTag("Spawn"))
                {
                    isHitSomething = false;
                }
                else
                {
                    isHitSomething = true;
                }

            }
            else if (!isAnyRayColliding)
            {
                //Debug.DrawRay(rayOrigin.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                //Debug.Log("Camino Libre");
                isHitSomething = false;
                multiplierBrakePedal = _rigidbody.velocity.magnitude > 6.0f ? -1.0f : (_rigidbody.velocity.magnitude < 1.0f ? 2.0f : 1.0f);
            }

            //Debug.Log("Accerelation: " + _acceleration);
            //Debug.Log("Sterring: " + _steering);
            //Debug.Log("Velocity: " + _rigidbody.velocity.magnitude);

            float motor = isHitSomething ? 0 : maxMotorTorque */* Input.GetAxis("Vertical")*/_acceleration * multiplierBrakePedal;
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

        VerifyLimits();
    }

    private void VerifyLimits() {
        if (raceMode && position == Checkpoints.Count)
        {
            position = 0; 
            canStart = false;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeBeforeDestroy) Destroy(this.gameObject);
        }
        else if (!raceMode && position == Checkpoints.Count) {
            position = 0;
            completedLaps++;
            if (completedLaps == totalLaps) {
                raceMode = true; 
                canStart = false;
            }
        }
    }

    
}
