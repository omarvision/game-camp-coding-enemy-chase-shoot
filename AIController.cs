using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AIController : MonoBehaviour
{
    public Renderer ground = null;
    public float distThreshold = 0.1f;
    public float timeThreshold = 5.0f;    
    private NavMeshAgent nma = null;
    private Rigidbody rb = null;
    private float mark = -1;
    private GameObject eye = null;
    private Renderer eyerend = null;

    private void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
   
        eye = this.transform.Find("Eye").gameObject;
        eyerend = eye.GetComponent<Renderer>();
    }
    private void Update()
    {
        if (nma.remainingDistance < distThreshold || Time.time - mark > timeThreshold || mark == -1)
        {
            PickRandomDestination();
        }
    }
    private void PickRandomDestination()
    {
        float rx = Random.Range(ground.bounds.min.x, ground.bounds.max.x);
        float rz = Random.Range(ground.bounds.min.z, ground.bounds.max.z);
        float y = ground.bounds.max.y;
        Vector3 destination = new Vector3(rx, y, rz);
        nma.SetDestination(destination);
        mark = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (eye == null)
        {
            eye = this.transform.Find("Eye").gameObject;            
        }
        if (eyerend == null)
        {
            eyerend = eye.GetComponent<Renderer>();
        }
        eyerend.material.SetColor("_Color", Color.red);
    }
    private void OnTriggerExit(Collider other)
    {
        if (eye == null)
        {
            eye = this.transform.Find("Eye").gameObject;
        }
        if (eyerend == null)
        {
            eyerend = eye.GetComponent<Renderer>();
        }
        eyerend.material.SetColor("_Color", Color.white);
    }
}
