using TMPro;
using UnityEngine;

public class DestinationPoint : MonoBehaviour
{
    [SerializeField]
    private float force = 10.0f;

    [SerializeField]
    private TextMeshPro overlay;

    private string originalText;

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.name == "Player")
        {
            originalText = overlay.text;
            overlay.text = "ACTIVATED";
            other.gameObject.GetComponent<Rigidbody>().AddForce(force * Vector3.up, ForceMode.Force);
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.name == "Player")
        {
            overlay.text = originalText;
        }
    }
}
