using System;
using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using TMPro;




public class MotorbikeController : MonoBehaviour
{
    [SerializeField] private MotorSettingsSO _settings;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private Rigidbody SphereRB, BikeRB;
    [SerializeField] private LayerMask derivableSurface;
    [SerializeField] private GameObject handle;
    [SerializeField] private TrailRenderer skidMarks;
    [SerializeField] private AudioSource EngineSource;
    [SerializeField] private AudioSource SkidSource;
    [SerializeField] private GameObject FrontTyre, BackTyre;
    [SerializeField] private ParticleSystem _smoke;
    
    
    private float moveInput, steerInput, rayLenght, currentVelocityOffset;
    
    private RaycastHit hitInfo;

    void Start()
    {
        SphereRB.transform.parent = null;
        BikeRB.transform.parent = null;

        rayLenght = SphereRB.GetComponent<SphereCollider>().radius + 0.2f;
        skidMarks.startWidth = _settings.skidWidth;
        skidMarks.emitting = false;
        SkidSource.mute = true;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        transform.position = SphereRB.transform.position;
        velocity = BikeRB.transform.InverseTransformDirection(BikeRB.linearVelocity);
        currentVelocityOffset = velocity.z / _settings.maxSpeed;


    }
    private void FixedUpdate()
    {
        Movement();
        SkidMarks();
        EngineSound();
        Smoke();
        FrontTyre.transform.Rotate(Vector3.right, Time.fixedDeltaTime * _settings.tyreRotSpeed * currentVelocityOffset);
        BackTyre.transform.Rotate(Vector3.right, Time.fixedDeltaTime * _settings.tyreRotSpeed *moveInput);


    }
    private void Movement()
    {


        if (Grounded())
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                Acceleration();
                Rotation();
            }
            Break();
        }
        else
        {
            Gravity();
        }
        BikeTilt();

    }
    private void Acceleration()
    {
        SphereRB.linearVelocity = Vector3.Lerp(SphereRB.linearVelocity,
        moveInput * _settings.maxSpeed * transform.forward
        , Time.fixedDeltaTime * _settings.acceleration);
    }
    private void Rotation()
    {
        transform.Rotate(0, steerInput * moveInput * currentVelocityOffset * _settings.steerStrenght * Time.fixedDeltaTime, 0, Space.World);
        Quaternion handleQuaternion = Quaternion.Euler(handle.transform.localRotation.eulerAngles.x, _settings.handleRotVal * steerInput, handle.transform.localRotation.eulerAngles.z);
        handle.transform.localRotation = Quaternion.Slerp(handle.transform.localRotation, handleQuaternion, _settings.handleRotSpeed);
    }
    private void Break()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SphereRB.linearVelocity *= _settings.breakingFactor / 10;
        }
    }
    private void BikeTilt()
    {

        float xRot = (Quaternion.FromToRotation(BikeRB.transform.up, hitInfo.normal) * BikeRB.transform.rotation).eulerAngles.x;
        float zRot = 0;
        zRot = -_settings.zTiltAngle * steerInput * currentVelocityOffset;
        if (currentVelocityOffset > 0)
        {
            zRot = -_settings.zTiltAngle * steerInput * currentVelocityOffset;
        }
        Quaternion newRotation = Quaternion.Euler(xRot, transform.eulerAngles.y, zRot);
        Quaternion targetRotation = Quaternion.Slerp(BikeRB.transform.rotation, newRotation, _settings.biketiltIncrement);
        Quaternion Rot = Quaternion.Euler(targetRotation.eulerAngles.x, transform.eulerAngles.y, targetRotation.eulerAngles.z);
        BikeRB.MoveRotation(Rot);

    }
    private bool Grounded()
    {
        if (Physics.Raycast(SphereRB.position, Vector3.down, out hitInfo, rayLenght, derivableSurface))
        {

            return true;

        }
        else
        {

            return false;

        }
    }
    private void Gravity()
    {
        SphereRB.AddForce(Vector3.down * _settings.gravity, ForceMode.Acceleration);
    }
    private void SkidMarks()
    {
        if (Grounded() && Mathf.Abs(velocity.x) > _settings.minSkidVelocity)
        {
            skidMarks.emitting = true;
            SkidSource.mute = false;
        }
        else
        {
            skidMarks.emitting = false;
            SkidSource.mute = true;
        }
    }
    private void EngineSound()
    {
        EngineSource.pitch = Mathf.Lerp(_settings.minPitchSound, _settings.maxPitchSound, Mathf.Abs(currentVelocityOffset));
    }
    private void Smoke()
    {
        if (skidMarks.emitting)
        {
            _smoke.Play();
        }
        else
        {
            _smoke.Stop();
        }
    }

}
