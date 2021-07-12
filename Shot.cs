using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Shot : MonoBehaviour
{
    public float MoveSpeed = 10.0f;
    public float LifeTime = 3.0f;
    private float mark = -1;
    private Collider coll = null;
    private Renderer rend = null;
    private Rigidbody rb = null;

    private void Start()
    {
        coll = this.GetComponent<Collider>();
        rend = this.GetComponent<Renderer>();
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = false;
    }
    private void Update()
    {
        if (mark != -1 && Time.time - mark < LifeTime)
        {
            this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        else
        {
            SetPooled();
        }
    }
    public void SetPooled()
    {
        if (coll == null)
        {
            coll = this.GetComponent<Collider>();
        }
        coll.enabled = false;
        if (rend == null)
        {
            rend = this.GetComponent<Renderer>();
        }
        rend.enabled = false;
        mark = -1;
    }
    public bool FireShot(Vector3 source, Vector3 target)
    {
        if (coll.enabled == false && rend.enabled == false && mark == -1)
        {
            this.transform.position = source;
            this.transform.LookAt(target);
            coll.enabled = true;
            rend.enabled = true;
            mark = Time.time;
            return true;
        }

        return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
}
