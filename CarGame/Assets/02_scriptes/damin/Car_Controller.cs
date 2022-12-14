using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Car_Controller : MonoBehaviour
{
    // public enum ControlMode
    // {
    //     Keyboard,
    //     Buttons
    // };

<<<<<<< Updated upstream
    public enum Axel
    {
        Front,
        Rear
    }
=======
    // public enum Axel
    // {
    //     Front,
    //     Rear,
    //     All
    // }
>>>>>>> Stashed changes

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public ParticleSystem smokeParticle;
        public GameObject wheelEffectObj;
        public Axel axel;
    }

<<<<<<< Updated upstream
    public ControlMode control;
=======
    // public Axel axel;
    // public ControlMode control;
>>>>>>> Stashed changes

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

<<<<<<< Updated upstream
=======
    // [SerializeField]private float maxSpeed;
    // [SerializeField]private float maxRSpeed;
    // [SerializeField]private float maxTorque ;
    public  float currentSpeed;
    [SerializeField]private Text GearTXT;


>>>>>>> Stashed changes
    public Vector3 _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;
    private bool HandBreak=false;

    //private CarLights carLights;

<<<<<<< Updated upstream
=======
    WheelFrictionCurve frictionCurveL;
    WheelFrictionCurve frictionCurveR;
    WheelFrictionCurve srictionCurveL;
    WheelFrictionCurve srictionCurveR;

    [Header("drift")]
    [SerializeField]float slipRate = 1;
    [SerializeField]float handBreakslipRate =0.4f;
    public float wheelSteeringAngle;
    public float wheelRotateSpeed;
    public float Horizontal;
    public float Vertical;
    public float wheelMaxSpeed;
    [SerializeField] private float HandBreakTime;
    [SerializeField]public int gear = 0;
    private float wheelcurrentspeed;
    [SerializeField]private float maxSpeed;
    private bool isMaxSpeed=false;


    private bool ishandbrake=false;
    private float horizontal;
    public float handBrakeFrictionMultiplier = 2f;
    [SerializeField]private Text RPMTXT;
    private float engineRPM;
    private float wheelsRPM;
    [SerializeField]private float downForceValue;
    private bool isBreake=false;
    

>>>>>>> Stashed changes
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        //carLights = GetComponent<CarLights>();
    }

    void Update()
    {
<<<<<<< Updated upstream
        GetInputs();
        AnimateWheels();
=======
        //float sum;
        //wheelsRPM = (4 != 0) ? sum / R : 0;
        //engineRPM = Mathf.SmoothDamp(engineRPM,1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity , smoothTime);
        RPMTXT.text="RPM " + KPH.ToString();
        //currentSpeed = 2 * 3.14f * wheelCollider_RL.radius * wheelCollider_RL.rpm * 60 / 1000;
        //currentSpeed = Mathf.Round (currentSpeed);

        //currentSpeed = Mathf.Round (currentSpeed);
//        currentSpeed = carRb.velocity.magnitude;
    if(currentSpeed > maxSpeed)
    {
        isMaxSpeed=true;
        wheelCollider_RL.motorTorque = -10 * Input.GetAxis("Vertical");
        wheelCollider_RR.motorTorque = -10 * Input.GetAxis("Vertical");
        wheelCollider_FL.motorTorque = -10 * Input.GetAxis("Vertical");
        wheelCollider_FR.motorTorque = -10 * Input.GetAxis("Vertical");
    }
    else
    {
        isMaxSpeed=false;
    }
        GearChange();
        //GetInputs();
        Brake();
>>>>>>> Stashed changes
        WheelEffects();
    }

    void LateUpdate()
    {
<<<<<<< Updated upstream
        Move();
        Steer();
        Brake();
=======
        AddDownForce();
        adjustTraction();
        AnimateWheels();
        wheelControl();
        // Move();
        // Steer();
        
        Vector3 vel = carRb.velocity;
        //currentSpeed = carRb.velocity.magnitude * 2.23693629f;
        currentSpeed=carRb.velocity.sqrMagnitude;
>>>>>>> Stashed changes
    }

    public void MoveInput(float input)
    {
        moveInput = input;
    }

    public void SteerInput(float input)
    {
        steerInput = input;
    }

    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    void Move()
    {
        foreach(var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
<<<<<<< Updated upstream
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
=======
        // foreach(var wheel in wheels)
        // {
        //     if (wheel.axel == Axel.Front)
        //     {
        //         var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
        //         wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
        //     }
        // }
        var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
        wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
        wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);

        // wheelCollider_FL.steerAngle = Mathf.LerpAngle(wheelCollider_FL.steerAngle, 0, Time.deltaTime * wheelRotateSpeed);
        // wheelCollider_FR.steerAngle = Mathf.LerpAngle(wheelCollider_FR.steerAngle, 0, Time.deltaTime * wheelRotateSpeed);
        // if (steerInput > 0.1)
        // {
        // wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
        // wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
        // }

        // if(steerInput < -0.1)
        // {
        //     wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
        //     wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
        // }
        // else
        // {
        //     var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
        //     wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
        //     wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);
        // }
    }
    void wheelControl()
    {
//Sets default steering angle
            wheelCollider_FL.steerAngle = Mathf.LerpAngle(wheelCollider_FL.steerAngle, 0, Time.deltaTime * wheelRotateSpeed);
             wheelCollider_FR.steerAngle = Mathf.LerpAngle(wheelCollider_FR.steerAngle, 0, Time.deltaTime * wheelRotateSpeed);
            //Sets default motor speed
            if(!isMaxSpeed)
            {
                wheelCollider_RL.motorTorque = -Mathf.Lerp(wheelCollider_RL.motorTorque, 0, Time.deltaTime * maxAcceleration);
                wheelCollider_RR.motorTorque = -Mathf.Lerp(wheelCollider_RR.motorTorque, 0, Time.deltaTime * maxAcceleration);
            }
            
            //Motor controls

            horizontal = Input.GetAxis("Horizontal");
            if(!isMaxSpeed)
            {
                Vertical = Input.GetAxis("Vertical");
            }
            
            
            if (Vertical > 0.1 && !isMaxSpeed)
            {
                wheelCollider_RL.motorTorque = Mathf.Lerp(wheelCollider_RL.motorTorque, wheelcurrentspeed, Time.deltaTime * maxAcceleration);
                wheelCollider_RR.motorTorque = Mathf.Lerp(wheelCollider_RR.motorTorque, wheelcurrentspeed, Time.deltaTime * maxAcceleration);
            }

            if(Input.GetKey(KeyCode.S))
            {
                ishandbrake=true;
            }
            else
            {
                ishandbrake=false;
            }

            if (Vertical < 0)
            {
                
                // wheelCollider_RL.motorTorque = -Mathf.Lerp(wheelCollider_RL.motorTorque, wheelcurrentspeed, Time.deltaTime * maxAcceleration * brakeAcceleration);
                // wheelCollider_RR.motorTorque = -Mathf.Lerp(wheelCollider_RR.motorTorque, wheelcurrentspeed, Time.deltaTime * maxAcceleration * brakeAcceleration);
                // carRb.drag = 0.3f;
            }
            else
            {
                carRb.drag = 0;
            }

            if (horizontal > 0 ) {
				//rear tracks size is set to 1.5f       wheel base has been set to 2.55f
            wheelCollider_FL.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (6 + (1.5f / 2))) * horizontal;
            wheelCollider_FR.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (6 - (1.5f / 2))) * horizontal;
        } else if (horizontal < 0 ) {
            wheelCollider_FL.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (6 - (1.5f / 2))) * horizontal;
            wheelCollider_FR.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (6 + (1.5f / 2))) * horizontal;
			//transform.Rotate(Vector3.up * steerHelping);

        } else {
            wheelCollider_FL.steerAngle =0;
            wheelCollider_FR.steerAngle =0;
        }
            // if (horizontal > 0.1)
            // {
            //     if(Input.GetKey(KeyCode.Space))
            //     {
            //         wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            //      wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            //     }
            //     else
            //         {
            //             var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
            //             wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
            //             wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);
            //             // wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, currentSpeed / 60 * 360 *Time.deltaTime, 0.6f);
            //             // wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, currentSpeed / 60 * 360 *Time.deltaTime, 0.6f);
            //         }
            // }
            

            // if (horizontal < -0.1)
            // {
            //     if(Input.GetKey(KeyCode.Space))
            //     {
            //         wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            //         wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            //     }
            //     else
            //         {
            //             var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
            //             wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
            //             wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);
            //             // wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, currentSpeed / 60 * 360 *Time.deltaTime, 0.6f);
            //             // wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, currentSpeed / _steerAngle *Time.deltaTime, 0.6f);
                        
            //         }
            // }
            
        
        // if((horizontal < -0.55 || horizontal > 0.55) && currentSpeed > 39)
        // {
        //     frictionCurveL.stiffness = slipRate;
        //     wheelCollider_RL.sidewaysFriction = frictionCurveL;
        //     wheelCollider_RR.sidewaysFriction = frictionCurveL;
        //     wheelCollider_FL.sidewaysFriction = frictionCurveL;
        //     wheelCollider_FR.sidewaysFriction = frictionCurveL;
        // }
        // else
        // {
        //     frictionCurveL.stiffness = handBreakslipRate;
        //     wheelCollider_RL.sidewaysFriction = frictionCurveL;
        //     wheelCollider_RR.sidewaysFriction = frictionCurveL;
        //     wheelCollider_FL.sidewaysFriction = frictionCurveL;
        //     wheelCollider_FR.sidewaysFriction = frictionCurveL;
        // }
>>>>>>> Stashed changes
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
<<<<<<< Updated upstream
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
=======
            // foreach (var wheel in wheels)
            // {
            //     if(wheel.axel == Axel.Rear)
            //     {
            //         wheel.wheelCollider.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            //     }
            // }
            // wheelCollider_RL.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            // wheelCollider_RR.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            isBreake=true;
            wheelCollider_RL.brakeTorque = 3000;
            wheelCollider_RR.brakeTorque = 3000;
            
            //wheelCollider_RL.motorTorque = 0;
            //wheelCollider_RR.motorTorque = 0;

            // if(currentSpeed>49)
            // {
            //     StartCoroutine(HandBreak());
            // }

            // frictionCurveL.stiffness = handBreakslipRate;
            // wheelCollider_RL.forwardFriction =frictionCurveL;

            // srictionCurveL.stiffness = handBreakslipRate;
            // wheelCollider_RL.sidewaysFriction = srictionCurveL;

            // frictionCurveR.stiffness = handBreakslipRate;
            // wheelCollider_RR.forwardFriction =frictionCurveR;
>>>>>>> Stashed changes

            // carLights.isBackLightOn = true;
            // carLights.OperateBackLights();
<<<<<<< Updated upstream
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
=======

        }
        else
        {
            // foreach (var wheel in wheels)
            // {
            //     wheel.wheelCollider.brakeTorque = 0;
            // }
            isBreake=false;
            wheelCollider_RL.brakeTorque =0;
            wheelCollider_RR.brakeTorque =0;

            // WheelFrictionCurve curve = new WheelFrictionCurve();
            // curve.stiffness=0.9f;
            // wheelCollider_RL.forwardFriction=curve;
            // wheelCollider_RL.sidewaysFriction=curve;
            // wheelCollider_RR.forwardFriction=curve;
            // wheelCollider_RR.sidewaysFriction=curve;



            // frictionCurveL.stiffness = slipRate;
            // wheelCollider_RL.forwardFriction =frictionCurveL;

            // srictionCurveL.stiffness = slipRate;
            // wheelCollider_RL.sidewaysFriction = srictionCurveL;

            // frictionCurveR.stiffness = slipRate;
            // wheelCollider_RR.forwardFriction =frictionCurveR;

            // srictionCurveR.stiffness = slipRate;
            // wheelCollider_RR.sidewaysFriction = srictionCurveR;


>>>>>>> Stashed changes

            // carLights.isBackLightOn = false;
            // carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
<<<<<<< Updated upstream
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
=======
        // foreach(var wheel in wheels)
        // {
        //     Quaternion rot;
        //     Vector3 pos;
        //     wheel.wheelCollider.GetWorldPose(out pos, out rot);
        //     wheel.wheelModel.transform.position = pos;
        //     wheel.wheelModel.transform.rotation = rot;
        // }
        // Quaternion rot;
        //     Vector3 pos;
        // wheelCollider_FL.GetWorldPose(out pos, out rot);
        // wheelModel_FL.transform.position = pos;
        // wheelModel_FL.transform.rotation = rot;

        // wheelCollider_FR.GetWorldPose(out pos, out rot);
        // wheelModel_FR.transform.position = pos;
        // wheelModel_FR.transform.rotation = rot;

        // wheelCollider_RL.GetWorldPose(out pos, out rot);
        // wheelModel_RL.transform.position = pos;
        // wheelModel_RL.transform.rotation = rot;

        // wheelCollider_RR.GetWorldPose(out pos, out rot);
        // wheelModel_RR.transform.position = pos;
        // wheelModel_RR.transform.rotation = rot;

        Vector3 wheelPosition = Vector3.zero;
		Quaternion wheelRotation = Quaternion.identity;

		
			wheelCollider_FL .GetWorldPose (out wheelPosition, out wheelRotation);
			wheelModel_FL.transform.position = wheelPosition;
			wheelModel_FL.transform.rotation = wheelRotation;

            wheelCollider_FR .GetWorldPose (out wheelPosition, out wheelRotation);
			wheelModel_FR.transform.position = wheelPosition;
			wheelModel_FR.transform.rotation = wheelRotation;

            wheelCollider_RL .GetWorldPose (out wheelPosition, out wheelRotation);
			wheelModel_RL.transform.position = wheelPosition;
			wheelModel_RL.transform.rotation = wheelRotation;

            wheelCollider_RR .GetWorldPose (out wheelPosition, out wheelRotation);
			wheelModel_RR.transform.position = wheelPosition;
			wheelModel_RR.transform.rotation = wheelRotation;
		
>>>>>>> Stashed changes
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            //var dirtParticleMainSettings = wheel.smokeParticle.main;

<<<<<<< Updated upstream
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
=======
           // if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear &&wheelCollider_FL.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
            if ((currentSpeed>39 && ishandbrake) || Input.GetKey(KeyCode.Space)  && wheelCollider_RL.isGrounded == true && carRb.velocity.magnitude >= 10.0f && wheelCollider_RR.isGrounded == true || (currentSpeed>49 && (Horizontal > 0.6 || Horizontal < -0.6)))
>>>>>>> Stashed changes
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }

    IEnumerator handBreak()
    {
        frictionCurveL.stiffness = slipRate;
        wheelCollider_RL.sidewaysFriction = frictionCurveL;
        wheelCollider_RR.sidewaysFriction = frictionCurveL;
        wheelCollider_FL.sidewaysFriction = frictionCurveL;
        wheelCollider_FR.sidewaysFriction = frictionCurveL;
        // WheelFrictionCurve curve = new WheelFrictionCurve();
        // curve.stiffness=0.5f;
        // wheelCollider_RL.sidewaysFriction=curve;
        // wheelCollider_RR.sidewaysFriction=curve;
        // wheelCollider_FL.sidewaysFriction=curve;
        // wheelCollider_FR.sidewaysFriction=curve;
        yield return new WaitForSeconds(HandBreakTime);
        frictionCurveL.stiffness = handBreakslipRate;
        wheelCollider_RL.sidewaysFriction = frictionCurveL;
        wheelCollider_RR.sidewaysFriction = frictionCurveL;
        wheelCollider_FL.sidewaysFriction = frictionCurveL;
        wheelCollider_FR.sidewaysFriction = frictionCurveL;
        // WheelFrictionCurve curvered = new WheelFrictionCurve();
        // curvered.stiffness=1f;
        // wheelCollider_RL.sidewaysFriction=curvered;
        // wheelCollider_RR.sidewaysFriction=curvered;
        // wheelCollider_FL.sidewaysFriction=curvered;
        // wheelCollider_FR.sidewaysFriction=curvered;
    }

    private void GearChange()
    {
        switch(gear)
        {
            case -1:
                GearTXT.text="R";
                wheelcurrentspeed = -700;
                maxSpeed=100;
                break;
            case 0:
                maxSpeed = 0;
                wheelcurrentspeed = 0;
                GearTXT.text="N";
                break;  
            case 1:
                wheelcurrentspeed=2000;
                maxSpeed=230;
                GearTXT.text=gear.ToString();
                break;
            case 2:
                if(!isBreake)
                {
                    wheelcurrentspeed= currentSpeed > 230 ? 2000 : 1000;
                }
                else
                {
                    wheelcurrentspeed=0;
                }
                maxSpeed=680;
                GearTXT.text=gear.ToString();
                break;
            case 3:
                if(!isBreake)
                {
                    wheelcurrentspeed= currentSpeed > 680 ? 2000 : 1000;
                }
                else
                {
                    wheelcurrentspeed=0;
                }
                
                maxSpeed=1160;
                GearTXT.text=gear.ToString();
                break;
            case 4:
                if(!isBreake)
                {
                    wheelcurrentspeed= currentSpeed > 1160 ? 2000 : 1000;
                }
                else
                {
                    wheelcurrentspeed=0;
                }
                maxSpeed=1720;
                GearTXT.text=gear.ToString();
                break;
            case 5:
                if(isBreake)
                {
                    wheelcurrentspeed= currentSpeed > 1720 ? 2000 : 1000;
                }
                else
                {
                    wheelcurrentspeed=0;
                }
                maxSpeed=2680;
                GearTXT.text=gear.ToString();
                break;
            case 6:
                if(!isBreake)
                {
                    wheelcurrentspeed= currentSpeed > 2680 ? 2000 : 1000;
                }
                else
                {
                    wheelcurrentspeed=0;
                }
                maxSpeed=4340;
                GearTXT.text=gear.ToString();
                break;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow) && gear >-1)
        {
            gear--;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && gear < 6)
        {
            gear++;
        }
    }
    private WheelFrictionCurve  forwardFriction,sidewaysFriction;
    private float driftFactor;
    float KPH;
    private void adjustTraction(){
         KPH = carRb.velocity.magnitude * 3.6f;
            //tine it takes to go from normal drive to drift 
        float driftSmothFactor = .7f * Time.deltaTime;

		if(ishandbrake)
        {
            sidewaysFriction = wheelCollider_FL.sidewaysFriction;
            forwardFriction = wheelCollider_FL.forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue =sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =
                Mathf.SmoothDamp(forwardFriction.asymptoteValue,driftFactor * handBrakeFrictionMultiplier,ref velocity ,driftSmothFactor );

                sidewaysFriction.stiffness=0.75f;
            
                wheelCollider_FL.sidewaysFriction = sidewaysFriction;
                wheelCollider_FL.forwardFriction = forwardFriction;
                wheelCollider_FR.sidewaysFriction = sidewaysFriction;
                wheelCollider_FR.forwardFriction = forwardFriction;
                wheelCollider_RL.sidewaysFriction = sidewaysFriction;
                wheelCollider_RL.forwardFriction = forwardFriction;
                wheelCollider_RR.sidewaysFriction = sidewaysFriction;
                wheelCollider_RR.forwardFriction = forwardFriction;
            
                frictionCurveL.stiffness = handBreakslipRate;


            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =  1.1f;
                //extra grip for the front wheels

                wheelCollider_FL.sidewaysFriction = sidewaysFriction;
                wheelCollider_FL.forwardFriction = forwardFriction;
                wheelCollider_FR.sidewaysFriction = sidewaysFriction;
                wheelCollider_FR.forwardFriction = forwardFriction;
            
            GetComponent<Rigidbody>().AddForce(transform.forward * (KPH / 600) * 20000 );   

            wheelCollider_RL.brakeTorque = 3000;
            wheelCollider_RR.brakeTorque = 3000;
		}
            //executed when handbrake is being held
        else
        {

			forwardFriction = wheelCollider_FL.forwardFriction;
			sidewaysFriction = wheelCollider_FL.sidewaysFriction;

			forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = 
                ((KPH * handBrakeFrictionMultiplier) / 300) + 1;

                sidewaysFriction.stiffness=1f;

				wheelCollider_FL.forwardFriction = forwardFriction;
				wheelCollider_FL.sidewaysFriction = sidewaysFriction;
                wheelCollider_FR.forwardFriction = forwardFriction;
				wheelCollider_FR.sidewaysFriction = sidewaysFriction;
                wheelCollider_RL.forwardFriction = forwardFriction;
				wheelCollider_RL.sidewaysFriction = sidewaysFriction;
                wheelCollider_RR.forwardFriction = forwardFriction;
				wheelCollider_RR.sidewaysFriction = sidewaysFriction;
			
        }

            //checks the amount of slip to control the drift
		

            WheelHit wheelHit;

            wheelCollider_RL.GetGroundHit(out wheelHit);            
            wheelCollider_RR.GetGroundHit(out wheelHit);            

			if(wheelHit.sidewaysSlip < 0 )	driftFactor = (1 + horizontal) * Mathf.Abs(wheelHit.sidewaysSlip) ;

			if(wheelHit.sidewaysSlip > 0 )	driftFactor = (1 + horizontal )* Mathf.Abs(wheelHit.sidewaysSlip );
		
		
            wheelCollider_RL.brakeTorque =0;
            wheelCollider_RR.brakeTorque =0;

	}

    void AddDownForce()
    {
        carRb.AddForce(-transform.up * downForceValue * carRb.velocity.magnitude);
    }
}
