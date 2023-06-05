using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public GameObject bulletHolePrefab;

    public void LaunchRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            var decalObject = Instantiate(bulletHolePrefab, hit.point, Quaternion.identity);
            decalObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            decalObject.transform.parent = hit.transform;

            var targetLife = hit.collider.GetComponentInParent<TargetLife>();
            if (targetLife)
            {
                targetLife.Attacked();
            }

            var rb = hit.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddForceAtPosition(transform.TransformDirection(Vector3.forward) * 100, hit.point);
            }
        }
    }
}
