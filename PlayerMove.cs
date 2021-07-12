using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 3.5f;
    public float TurnSpeed = 120.0f;
    private Rigidbody rb = null;
    private Camera cam = null;
    private ShotPool shotpool = null;

    private void Start()
    {
        shotpool = this.GetComponentInChildren<ShotPool>();

        cam = this.GetComponentInChildren<Camera>();
        if (cam != null && cam.enabled == true)
        {
            TurnOffOtherCameras();
            Cursor.lockState = CursorLockMode.Locked;
        }        

        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    private void Update()
    {
        if (cam == null)
        {
            //turn and move
            float x = Input.GetAxis("Horizontal");
            this.transform.localRotation *= Quaternion.AngleAxis(TurnSpeed * x * Time.deltaTime, Vector3.up);
            float z = Input.GetAxis("Vertical");
            this.transform.Translate(Vector3.forward * z * MoveSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            //move
            float x = Input.GetAxis("Horizontal");
            this.transform.Translate(Vector3.right * x * MoveSpeed * Time.deltaTime);
            float z = Input.GetAxis("Vertical");
            this.transform.Translate(Vector3.forward * z * MoveSpeed * Time.deltaTime);
            //look
            float mx = Input.GetAxis("Mouse X");
            this.transform.localRotation *= Quaternion.AngleAxis(mx * TurnSpeed * Time.deltaTime, Vector3.up);
            float my = Input.GetAxis("Mouse Y");
            cam.transform.localRotation *= Quaternion.AngleAxis(-my * TurnSpeed * Time.deltaTime, Vector3.right);
        }
    }
    private void TurnOffOtherCameras()
    {
        if (cam == null)
        {
            return;
        }
        foreach (Camera c in Camera.allCameras)
        {
            if (c.name != cam.name)
            {
                c.enabled = false;
                c.GetComponent<AudioListener>().enabled = false;
            }
        }
    }
}
