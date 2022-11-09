using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //ERROR::GetComponent<Rigidbody>().

        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject,5f);//Destroy after 1sc
    }
}
