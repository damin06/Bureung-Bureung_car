using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car_Controller : MonoBehaviour
{
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear,
        All
    }

    //[Serializable]
    // public struct Wheel
    // {
    //     public GameObject wheelModel;
    //     public WheelCollider wheelCollider;
    //     public ParticleSystem smokeParticle;
    //     public GameObject wheelEffectObj;
    //     public Axel axel;
    // }




    [Header("wheel_FL")]
    public GameObject wheelModel_FL;
        public WheelCollider wheelCollider_FL;
        public ParticleSystem smokeParticle_FL;
        public GameObject wheelEffectObj_FL;

        [Header("wheel_FR")]
        public GameObject wheelModel_FR;
        public WheelCollider wheelCollider_FR;
        public ParticleSystem smokeParticle_FR;
        public GameObject wheelEffectObj_FR;

        [Header("wheel_RL")]
        public GameObject wheelModel_RL;
        public WheelCollider wheelCollider_RL;
        public ParticleSystem smokeParticle_RL;
        public GameObject wheelEffectObj_RL;

        [Header("wheel_RR")]
        public GameObject wheelModel_RR;
        public WheelCollider wheelCollider_RR;
        public ParticleSystem smokeParticle_RR;
        public GameObject wheelEffectObj_RR;

    public Axel axel;
    public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    // [SerializeField]private float maxSpeed;
    // [SerializeField]private float maxRSpeed;
    // [SerializeField]private float maxTorque ;
    public  float currentSpeed;


    public Vector3 _centerOfMass;

    private car_light car_Light;

    //public List<Wheel> wheels;

    float moveInput;
    float steerInput;

   // public float currentSpeed;

    
    private Rigidbody carRb;

    //private CarLights carLights;

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



    void Start()
    {
        //wheelCollider_RL=GetComponent<WheelCollider>();
        //wheelCollider_RR=GetComponent<WheelCollider>();

        frictionCurveL = wheelCollider_RL.forwardFriction;
        srictionCurveL = wheelCollider_RL.sidewaysFriction;
        frictionCurveR = wheelCollider_RR.forwardFriction;
        srictionCurveR = wheelCollider_RR.sidewaysFriction;

        car_Light = GetComponent<car_light>();
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        //carLights = GetComponent<CarLights>();
    }

    void Update()
    {
        //currentSpeed = 2 * 3.14f * wheelCollider_RL.radius * wheelCollider_RL.rpm * 60 / 1000;
        //currentSpeed = Mathf.Round (currentSpeed);

        //currentSpeed = Mathf.Round (currentSpeed);
//        currentSpeed = carRb.velocity.magnitude;
        GetInputs();
        AnimateWheels();
        WheelEffects();

        if(Input.GetKey(KeyCode.S))
        {
            car_Light.isBackLightOn = true;
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            car_Light.isBackLightOn = false;
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            car_Light.isBackLightOn = true;
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            car_Light.isBackLightOn = false;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            car_Light.isBackLightOn = true;
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            car_Light.isBackLightOn = false;
        }
        
    }

    void FixedUpdate()
    {
        wheelControl();
        // Move();
        // Steer();
        Brake();
        Vector3 vel = carRb.velocity;
        currentSpeed = carRb.velocity.magnitude * 2.23693629f;
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
        // if (currentSpeed <= 0 && currentSpeed > -maxSpeed) 
        // {
        //     wheelCollider_RL.motorTorque = -10  * Input.GetAxis("Vertical");	
        //     wheelCollider_RR.motorTorque = -10  * Input.GetAxis("Vertical");
        // }
        // else if (currentSpeed >= 0 && currentSpeed < maxRSpeed)
        // {
        //     wheelCollider_RR.motorTorque = -maxTorque  * Input.GetAxis("Vertical");		
        //     wheelCollider_RL.motorTorque = -maxTorque  * Input.GetAxis("Vertical");
        // }
        
        // if(currentSpeed>10)
        // {
        //     return;
        // }
        // else
        // {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
       // }
            
            //wheelCollider_RR.motorTorque = 0;		
            //wheelCollider_RL.motorTorque = 0;
        
    }

    void Move()
    {
        // foreach(var wheel in wheels)
        // {
        //     wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        // }
        // if(currentSpeed <= 0 && currentSpeed > maxSpeed)
        // {
        //     return;
        // }
        // else if (currentSpeed >= 0 && currentSpeed < maxRSpeed)
        // {
        //     return;
        // }
        // else
        // {
            // if(currentSpeed>10)
            // {
            //     return;
            // }
            // else
            // {
            wheelCollider_RR.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            wheelCollider_RL.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
          //  }
        //}
    }

    void Steer()
    {
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
            wheelCollider_RL.motorTorque = -Mathf.Lerp(wheelCollider_RL.motorTorque, 0, Time.deltaTime * maxAcceleration);
            wheelCollider_RR.motorTorque = -Mathf.Lerp(wheelCollider_RR.motorTorque, 0, Time.deltaTime * maxAcceleration);



            //Motor controls

            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            if (Vertical > 0.1)
            {
                wheelCollider_RL.motorTorque = Mathf.Lerp(wheelCollider_RL.motorTorque, wheelMaxSpeed, Time.deltaTime * maxAcceleration);
                wheelCollider_RR.motorTorque = Mathf.Lerp(wheelCollider_RR.motorTorque, wheelMaxSpeed, Time.deltaTime * maxAcceleration);
            }


            if (Vertical < -0.1)
            {
                wheelCollider_RL.motorTorque = -Mathf.Lerp(wheelCollider_RL.motorTorque, wheelMaxSpeed, Time.deltaTime * maxAcceleration * brakeAcceleration);
                wheelCollider_RR.motorTorque = -Mathf.Lerp(wheelCollider_RR.motorTorque, wheelMaxSpeed, Time.deltaTime * maxAcceleration * brakeAcceleration);
                carRb.drag = 0.3f;
            }
            else
            {
                carRb.drag = 0;
            }


            if (Horizontal > 0.1)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
                 wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
                }
                else
                    {
                        var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                        wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
                        wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);
                    }
            }
            

            if (Horizontal < -0.1)
            {
                if(Input.GetKey(KeyCode.Space))
                {
                    wheelCollider_FL.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
                    wheelCollider_FR.steerAngle= -Mathf.LerpAngle(wheelCollider_FL.steerAngle, wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
                }
                else
                    {
                        var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                        wheelCollider_FL.steerAngle =Mathf.Lerp(wheelCollider_FL.steerAngle, _steerAngle, 0.6f);
                        wheelCollider_FR.steerAngle =Mathf.Lerp(wheelCollider_FR.steerAngle, _steerAngle, 0.6f);
                    }
            }
            
        
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            // foreach (var wheel in wheels)
            // {
            //     if(wheel.axel == Axel.Rear)
            //     {
            //         wheel.wheelCollider.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            //     }
            // }
            // wheelCollider_RL.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            // wheelCollider_RR.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            wheelCollider_RL.brakeTorque = 3000;
            wheelCollider_RR.brakeTorque = 3000;


            // WheelFrictionCurve curve = new WheelFrictionCurve();
            // curve.stiffness=0.4f;
            // wheelCollider_RL.forwardFriction=curve;
            // wheelCollider_RL.sidewaysFriction=curve;
            // wheelCollider_RR.forwardFriction=curve;
            // wheelCollider_RR.sidewaysFriction=curve;



            // frictionCurveL.stiffness = handBreakslipRate;
            // wheelCollider_RL.forwardFriction =frictionCurveL;

            // srictionCurveL.stiffness = handBreakslipRate;
            // wheelCollider_RL.sidewaysFriction = srictionCurveL;

            // frictionCurveR.stiffness = handBreakslipRate;
            // wheelCollider_RR.forwardFriction =frictionCurveR;

            // srictionCurveR.stiffness = handBreakslipRate;
            // wheelCollider_RR.sidewaysFriction = srictionCurveR;
            //car_Light.isBackLightOn = true;
            // carLights.OperateBackLights();


        }
        else
        {
            // foreach (var wheel in wheels)
            // {
            //     wheel.wheelCollider.brakeTorque = 0;
            // }
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



            //car_Light.isBackLightOn = false;
            // carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
        // foreach(var wheel in wheels)
        // {
        //     Quaternion rot;
        //     Vector3 pos;
        //     wheel.wheelCollider.GetWorldPose(out pos, out rot);
        //     wheel.wheelModel.transform.position = pos;
        //     wheel.wheelModel.transform.rotation = rot;
        // }
        Quaternion rot;
            Vector3 pos;
        wheelCollider_FL.GetWorldPose(out pos, out rot);
        wheelModel_FL.transform.position = pos;
        wheelModel_FL.transform.rotation = rot;

        wheelCollider_FR.GetWorldPose(out pos, out rot);
        wheelModel_FR.transform.position = pos;
        wheelModel_FR.transform.rotation = rot;

        wheelCollider_RL.GetWorldPose(out pos, out rot);
        wheelModel_RL.transform.position = pos;
        wheelModel_RL.transform.rotation = rot;

        wheelCollider_RR.GetWorldPose(out pos, out rot);
        wheelModel_RR.transform.position = pos;
        wheelModel_RR.transform.rotation = rot;
    }

    void WheelEffects()
    {
        //foreach (var wheel in wheels)
        //{
            //var dirtParticleMainSettings = wheel.smokeParticle.main;

           // if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear &&wheelCollider_FL.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
            if (Input.GetKey(KeyCode.Space)  && wheelCollider_RL.isGrounded == true && carRb.velocity.magnitude >= 10.0f && wheelCollider_RR.isGrounded == true)
            {
            
                // wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                // wheel.smokeParticle.Emit(1);
                int rad = UnityEngine.Random.Range(1,4);
                wheelEffectObj_FL.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_FL.Emit(rad);

                wheelEffectObj_FR.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_FR.Emit(rad);

                wheelEffectObj_RL.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_RL.Emit(rad);

                wheelEffectObj_RR.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_RR.Emit(rad);
            }
            else
            {
                //wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
                wheelEffectObj_FL.GetComponentInChildren<TrailRenderer>().emitting = false;
                wheelEffectObj_FR.GetComponentInChildren<TrailRenderer>().emitting = false;
                wheelEffectObj_RL.GetComponentInChildren<TrailRenderer>().emitting = false;
                wheelEffectObj_RR.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
       // }
    }

}
