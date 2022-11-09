using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public Transform Hand;
    public GameObject MainWeapon;
    public GameObject SideWeapon;
    public GameObject Melee;
    private GameObject CurrentWeapon;
    float sens;
    void Start()
    {
        sens = GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity;
        ChangeWeapon(MainWeapon);
        
        print(sens);
    }
    public void ChangeWeapon(GameObject Weapon)
    {
        if(Weapon == null) return;
        GameObject.Find("Player").GetComponent<PlayerLook>().Sensitivity = sens ;
        Destroy(CurrentWeapon);
        CurrentWeapon = Instantiate(Weapon, Hand.position,Hand.parent.parent.transform.rotation);
        CurrentWeapon.transform.parent = Hand;
    }
    public void Update()
    {
        //WeaponManagment();//kinda like headbob for the gun
    }
    private void WeaponManagment()
    {
        if(CurrentWeapon == null) return;
        CurrentWeapon.transform.rotation = Hand.parent.parent.transform.rotation;
    }
    public void Shoot(float Input)
    {
        if(Input == 0) return;
        CurrentWeapon.GetComponent<Gun>().Shoot(Input);
    }
    public void ADS(float Input)
    {
        CurrentWeapon.GetComponent<Gun>().toggleAds(Input);
    }
    public void Reload()
    {
        CurrentWeapon.GetComponent<Gun>().Reload();
    }
}
