using UnityEngine;

public class RaycastToGround : MonoBehaviour
{
    [SerializeField] private float offset;
    private Transform t;

    private void Awake()
    {
        t = this.transform;
    }

    void Update()
    {
        var position = t.position;
        position = new Vector3(position.x, 0 - offset, position.z);
        t.position = position;
        t.rotation = Quaternion.identity;
        
    }
}
