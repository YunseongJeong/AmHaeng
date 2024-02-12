using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterwearController : MonoBehaviour
{
    Transform m_transform;
    Transform body_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.GetComponent<Transform>();
        body_transform =  this.GetComponentInParent<Transform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float body_rotate = body_transform.rotation.z;
        m_transform.rotation = new Quaternion(0, 0, body_rotate, 0);
    }
}
