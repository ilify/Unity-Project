using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform gun;
    // Start is called before the first frame update
    public void ShootRay(float Input)
    {
        if(Input==0) return;
        Debug.DrawRay(gun.transform.position, gun.TransformDirection(Vector3.forward)*100f,Color.red);
        //print("shoot");
    }
}
