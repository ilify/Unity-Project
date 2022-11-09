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
    private void Update()
    {
        if(GetComponent<Rigidbody>().velocity != Vector3.zero) GetComponent<Rigidbody>().velocity += transform.up * -9.8f * 2f * Time.deltaTime;
    }
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
    }
}
