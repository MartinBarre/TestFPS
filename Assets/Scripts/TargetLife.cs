using UnityEngine;

public class TargetLife : MonoBehaviour
{
    private AutoSpawns autoSpawns;

    private void Start()
    {
        autoSpawns = transform.parent.GetComponent<AutoSpawns>();
    }
    public void Attacked()
    {
        autoSpawns.Spawn();
        Destroy(gameObject);
    }
}
