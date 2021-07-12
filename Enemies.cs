using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int Count = 3;
    public GameObject prefabEnemy = null;
    public Renderer groundRend = null;

    private void Start()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject obj = Instantiate(prefabEnemy, this.transform);
            obj.name = prefabEnemy.name + i.ToString();
            AIController script = obj.GetComponent<AIController>();
            script.ground = this.groundRend;
            obj.transform.position = PickRandomSpawn(obj);
        }
    }
    private Vector3 PickRandomSpawn(GameObject obj)
    {
        float rx = Random.Range(groundRend.bounds.min.x, groundRend.bounds.max.x);
        float rz = Random.Range(groundRend.bounds.min.z, groundRend.bounds.max.z);
        float y = groundRend.bounds.max.y + obj.GetComponent<Renderer>().bounds.size.y;
        Vector3 spawn = new Vector3(rx, y, rz);
        return spawn;
    }
}
