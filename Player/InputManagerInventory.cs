using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerInventory : MonoBehaviour
{
    private InputMaster Input;
    private InputMaster.WeaponActions Actions;
    private InventorySystem inventorySystem;
    // Start is called before the first frame update
    void Awake()//Keys for switching Weapons
    {
        inventorySystem = GetComponent<InventorySystem>();
        Input = new InputMaster();
        Actions = Input.Weapon;
        //Actions.ADS.performed += ctx => inventorySystem.ADS();
        Actions.Reload.performed += ctx => inventorySystem.Reload();
        Actions.MainWeapon.performed += ctx => inventorySystem.ChangeWeapon(inventorySystem.MainWeapon);
        Actions.SideWeapon.performed += ctx => inventorySystem.ChangeWeapon(inventorySystem.SideWeapon);
        Actions.Melee.performed += ctx => inventorySystem.ChangeWeapon(inventorySystem.Melee);
    }
    private void FixedUpdate()
    {
        inventorySystem.ADS(Actions.ADS.ReadValue<float>());
        inventorySystem.Shoot(Actions.Shoot.ReadValue<float>());
    }

    // Update is called once per frame
    private void OnEnable()
    {
        Actions.Enable();
    }
    private void OnDisable()
    {
        Actions.Disable();
    }
}
