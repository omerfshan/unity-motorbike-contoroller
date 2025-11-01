using UnityEngine;

public class BikeVehicle: MonoBehaviour
{
    public float horizontalInput { get; set; }
	public float verticalInput { get; set; }
	public bool braking { get; set; }
	public bool isInControl { get; private set; }
	public bool slipFront { get; private set; }
	public bool slipRear { get; private set; }
    public Contact controlContact;
    public Transform COM;
        
        Rigidbody rb;
        void Awake()
		{
			rb = GetComponent<Rigidbody>();
        InControl(true);
        rb.centerOfMass = COM.localPosition;
		}


    public void ConstrainRotation(bool state)
    {
        if (state == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    public void InControl(bool state)
    {
        if (isInControl != state)
        {
            isInControl = state;
        }
    }
    public bool OnGround()
    {
        return controlContact.GetContact;
    }
    private void FixedUpdate()
{
    if (isInControl)
    {
        ApplyUprightForce();
        // sonra motor, steer, lean'i ekleyeceğiz
    }
}

private void ApplyUprightForce()
{
    Vector3 predictedUp = Quaternion.AngleAxis(
        rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.5f,
        rb.angularVelocity
    ) * transform.up;

    Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
    rb.AddTorque(torqueVector * (rb.mass * 20f)); 
}

        
}