using UnityEngine;

public class DemoFoceObject : MonoBehaviour
{
    
    [SerializeField] private Rigidbody targetRigidbody;
    [SerializeField] private BodyPhysics bodyPhysics;
    private void OnGUI()
    {
        if(GUILayout.Button("Apply Unity Force"))
        {
            targetRigidbody.AddForce(transform.right * 10, ForceMode.Force);
        }
        if (GUILayout.Button("Apply Custom Force"))
        {
            bodyPhysics.AddForce(transform.right * 10);
        }
    }
    private void ApplyForce()
    {
        
    }
}
