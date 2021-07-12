using System.Collections.Generic;
using UnityEngine;

public class ShotPool : MonoBehaviour
{
    public int Size = 3;
    public GameObject prefabShot = null;
    private Camera cam = null;
    private List<Shot> Pool = new List<Shot>();

    private void Start()
    {
        cam = this.GetComponentInChildren<Camera>();

        for (int i = 0; i < Size; i++)
        {
            GameObject obj = Instantiate(prefabShot);
            obj.name = prefabShot.name + i.ToString();
            Physics.IgnoreCollision(this.GetComponent<Collider>(), obj.GetComponent<Collider>());
            Shot script = obj.GetComponent<Shot>();
            script.SetPooled();
            Pool.Add(script);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Vector3 target = Vector3.zero;
            if (cam != null)
            {
                Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f));
                target = ray.origin + (ray.direction * 5.0f);
            }
            else
            {
                target = this.transform.forward * 5.0f;
            }            

            foreach (Shot script in Pool)
            {
                if (script.FireShot(this.transform.position, target) == true)
                {
                    break;
                }
            }
        }
    }
}
