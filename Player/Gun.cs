using EZCameraShake;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public enum Type {
        AR,
        Shotgun,
        Melee};
    public bool toggleADS = false;
    [SerializeField] Type type = new Type();
    public bool HasScope;
    public int Damage;
    public float FireRate;
    public float Range;
    public float RangeBP;
    public int Ammo;
    public Vector3 ADSpostions;
    public Vector3 Hippostions;
    public Quaternion ADSRot;
    public Quaternion HipRot;
    public AudioClip gunshotSound;
    public GameObject Scope;
    public float scopeSensMultiplier;
    //RunTime Variables
    [Header("RunTime")]
    private float nextFire = 0.0f;
    public int CurrentAmmo;
    private PlayerLook PlayerLook;
    public bool is_ADS;
    float sens;
    public void Start()
    {
        sens = GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity;
        is_ADS = false;
        FireRate = 1/FireRate;
        PlayerLook = transform.parent.parent.parent.parent.parent.GetComponent<PlayerLook>();
        Reload();//Start With full Mag
    }

    public void Update()
    {
        ADS();
        if(!HasScope || !is_ADS)Sway(PlayerLook.InputMouse);
        ResetRecoil();
    }
    public void Sway(Vector2 Input)
    {
        float mouseX = Input.x;
        float mouseY = Input.y;
        transform.localRotation = Quaternion.LerpUnclamped(transform.localRotation,
                                                           Quaternion.Euler(mouseY*0.1f, mouseX * 0.1f, 0),
                                                           0.3f);
    }
    public void UI(bool state)
    {
        GameObject.Find("Player").transform.GetChild(2).gameObject.SetActive(state);
    }
    //ADS
    float next = 0;
    public void toggleAds(float Input)
    {
        if (!toggleADS) { is_ADS = Input > 0; UI(!is_ADS); return; }
        else
        {
             // block ads for 0.5 sc
            if (Input > 0 && Time.time > next)
            {
                next = Time.time + 0.5f;
                is_ADS = !is_ADS;
                UI(!is_ADS);
            }
        } 
    }
    public void ADS()
    {
        if(!HasScope) GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity = sens;
        if (!is_ADS) 
        {
            if (HasScope)
            {
                Scope.transform.GetChild(1).gameObject.SetActive(false);
                Scope.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity = sens;
            }
            transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition,Hippostions, 15 * Time.deltaTime);
            transform.parent.localRotation = HipRot;
        }
        else 
        {
            if (HasScope)
            {
                Scope.transform.GetChild(1).gameObject.SetActive(true);
                Scope.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity = sens * scopeSensMultiplier;
            }
            transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition,ADSpostions, 15 * Time.deltaTime);
            transform.parent.localRotation = ADSRot;
        }
    }
    public void Shoot(float Input)
    {
        if (Input == 0) {  return; }
        switch (type)
        {
            case Type.AR:
                if (Time.time > nextFire)
                {
                    nextFire = Time.time+FireRate;
                    ShootAR(Input);
                }
                break;
            case Type.Shotgun:
                ShootSH(Input);
                break;
            case Type.Melee:
                ShootME(Input);
                break;
        }
    }
    public void Reload()
    {
        CurrentAmmo = Ammo;
    }
    public void ShootAR(float Input)
    {
        if(Input ==0) return;
        
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
            AudioSource.PlayClipAtPoint(gunshotSound, transform.position);
            if (Physics.Raycast(transform.parent.parent.position, transform.parent.parent.TransformDirection(Vector3.forward), out RaycastHit hitInfo, RangeBP))
            {
                var bullet = Instantiate(Bullet, hitInfo.point , transform.parent.rotation);
            }
            else
            {
                var bullet = Instantiate(Bullet, transform.parent.parent.position + transform.parent.parent.forward * RangeBP, transform.parent.parent.parent.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.parent.parent.forward * 250.0f;
            }
            if(FireRate < 0.2f) { CameraShaker.Instance.ShakeOnce(2f, 2f, 0f, 0.5f); }
            else { CameraShaker.Instance.ShakeOnce(FireRate * 10f, 2f, 0f, 0.5f); }
            if(!is_ADS) Recoil(1f);
            else Recoil(0.08f);
        }
    }
    private void Recoil(float x)
    {
        //transform.localRotation = Quaternion.Euler(-10f, 0f, 0f);
        transform.localPosition += new Vector3(0, 0, -x);
    }
    private void ResetRecoil()
    {
        //transform.localRotation = Quaternion.LerpUnclamped(transform.localRotation, Quaternion.Euler(0f, 0f, 0f),Time.deltaTime*10f);
        transform.localPosition = Vector3.LerpUnclamped(transform.localPosition,Vector3.zero,5*Time.deltaTime);
    }
    public void ShootSH(float Input)
    {
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
            print("SH shoots");
            print("SH shoots");
            print("SH shoots");
            print("SH shoots");
            print("SH shoots");
        }
    }
    public void ShootME(float Input)
    {
        print("Melee");
    }
}
