using UnityEngine;

public class Contact : MonoBehaviour
{
    private bool _contact;
    public bool GetContact => _contact;
    private void OnTriggerStay(Collider other)
    {
        _contact = true;
    }
    private void OnTriggerExit(Collider other)
    {
         _contact = false;
    }
}
