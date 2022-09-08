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

    public Vector3 _centerOfMass;

    private car_light car_Light;

    //public List<Wheel> wheels;

    float moveInput;
    float steerInput;


    
    private Rigidbody carRb;

    //private CarLights carLights;

    WheelFrictionCurve frictionCurveL;
    WheelFrictionCurve frictionCurveR;
    WheelFrictionCurve srictionCurveL;
    WheelFrictionCurve srictionCurveLR;

    [Header("drift")]
    [SerializeField]float slipRate = 1;
    [SerializeField]float handBreakslipRate =0.4f;

    

    void Start()
    {
        car_Light = GetComponent<car_light>();
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        //carLights = GetComponent<CarLights>();
    }

    void Update()
    {
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

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
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
        // foreach(var wheel in wheels)
        // {
        //     wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        // }
        wheelCollider_RR.motorTorque = moveInput * 1600 * maxAcceleration * Time.deltaTime;
        wheelCollider_RL.motorTorque = moveInput * 1600 * maxAcceleration * Time.deltaTime;
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
            wheelCollider_RL.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
            wheelCollider_RR.brakeTorque = 1600 * brakeAcceleration * Time.deltaTime;
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

                wheelEffectObj_FL.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_FL.Emit(1);

                wheelEffectObj_FR.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_FR.Emit(1);

                wheelEffectObj_RL.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_RL.Emit(1);

                wheelEffectObj_RR.GetComponentInChildren<TrailRenderer>().emitting = true;
                smokeParticle_RR.Emit(1);
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
