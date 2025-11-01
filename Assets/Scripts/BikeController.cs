using UnityEngine;

public class BikeController: MonoBehaviour
{
    BikeVehicle bike;
    [SerializeField] private bool is_control;

    private void Awake()
    {
        bike = GetComponent<BikeVehicle>();
    }
    private void Update()
    {
        bike.verticalInput = Input.GetAxis("Vertical");
        bike.horizontalInput = Input.GetAxis("Horizontal");
        BrakingInput();

        //Extending functionality 
        bike.InControl(is_control);

        if (is_control)
        {

            bike.ConstrainRotation(bike.OnGround());
        }
        else
        {
            bike.ConstrainRotation(false);
        }


    }
    void BrakingInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bike.braking = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                bike.braking = false;
            }

        }
        

}