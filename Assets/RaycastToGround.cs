using UnityEngine;

public class RaycastToGround : MonoBehaviour
{
    [SerializeField] private float offset;
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformPoint(Vector3.forward), out hit,  Mathf.Infinity))
        {
            this.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - offset, hit.transform.position.z );
        }
    }
}
