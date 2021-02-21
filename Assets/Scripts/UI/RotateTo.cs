using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{

    public GameObject Target;

    private void Start()
    {
        if (!Target)
            Target = FindObjectOfType<Camera>().gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        if(Target)
          transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position, Vector3.up);
    }
}
