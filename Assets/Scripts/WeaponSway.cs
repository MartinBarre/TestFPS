using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSway : MonoBehaviour
{
    public float drag = 2.5f;
    public float dragThreshold = -5f;
    public float smooth = 5;

    private Quaternion localRotation;

    private void Start()
    {
        localRotation = transform.localRotation;
    }

    private void Update()
    {
        var delta = Mouse.current.delta.ReadValue();
        var z = (delta.y) * drag;
        var y = -(delta.x) * drag;

        if (drag >= 0) // Weapon lags behind camera
        {
            y = (y > dragThreshold) ? dragThreshold : y;
            y = (y < -dragThreshold) ? -dragThreshold : y;

            z = (z > dragThreshold) ? dragThreshold : z;
            z = (z < -dragThreshold) ? -dragThreshold : z;
        }
        else // Camera lags behind weapon
        {
            y = (y < dragThreshold) ? dragThreshold : y;
            y = (y > -dragThreshold) ? -dragThreshold : y;

            z = (z < dragThreshold) ? dragThreshold : z;
            z = (z > -dragThreshold) ? -dragThreshold : z;
        }

        // Weapon default rotation transform has to be (0, 0, 0)
        var newRotation = Quaternion.Euler(localRotation.x + z, localRotation.y + y, localRotation.z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, (Time.deltaTime * smooth));
    }
}