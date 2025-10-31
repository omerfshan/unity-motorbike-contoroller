using System.Diagnostics;
using Unity.Android.Gradle.Manifest;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MotorbikeController : MonoBehaviour
{
    [SerializeField] private MotorSettingsSO _settings;
    private float moveInput,steerInput,rayLenght;
    [SerializeField] private Rigidbody SphereRB, BikeRB;
    [SerializeField] private LayerMask derivableSurface;
    RaycastHit hitInfo;

    void Start()
    {
        SphereRB.transform.parent = null;
        BikeRB.transform.parent = null;

        rayLenght = SphereRB.GetComponent<SphereCollider>().radius+0.2f;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
        transform.position = SphereRB.transform.position;
       
    }
    private void FixedUpdate()
    {
        Movement();
     
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
        transform.Rotate(0, steerInput * moveInput * _settings.steerStrenght * Time.fixedDeltaTime, 0, Space.World);
    }
    private void Break()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SphereRB.linearVelocity *= _settings.breakingFactor / 10f;
        }
    }
    private void BikeTilt()
    {
        float xRot = (Quaternion.FromToRotation(BikeRB.transform.up, hitInfo.normal) * BikeRB.transform.rotation).eulerAngles.x;
         Quaternion newRotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
        Quaternion targetRotation = Quaternion.Slerp(BikeRB.transform.rotation, newRotation, _settings.biketiltIncrement);
        Quaternion Rot = Quaternion.Euler(targetRotation.x, transform.eulerAngles.y, transform.eulerAngles.z);
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
}
