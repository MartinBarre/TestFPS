using UnityEngine;

public class AutoSpawns : MonoBehaviour
{
    public int targetCount = 5;
    public GameObject target;

    private void Start()
    {
        for (var i = 0; i < targetCount; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        var random = new Vector3(Random.Range(-8, 8) / 2f, Random.Range(-6, 6) / 2f, 0);
        var tmp = Instantiate(target, transform.position + random, Quaternion.identity);
        tmp.transform.parent = transform;

        var size = Random.Range(6, 10) / 10f;
        tmp.transform.localScale = new Vector3(size, size, size);
    }
}
