using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform CamHolder;
    private float xRotation;
    public float Sensitivity;
    [SerializeField, Range(0f, 90f)] public float MinLookingAngle;
    [SerializeField, Range(-90f,0f)] public float MaxLookingAngle;
    public Vector2 InputMouse;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Look(Vector2 Input)
    {
        InputMouse = Input;
        float mouseX = Input.x;
        float mouseY = Input.y;
        xRotation -= (mouseY * Sensitivity);
        //Limit Looking Angle
        xRotation = Mathf.Clamp(xRotation, MaxLookingAngle, MinLookingAngle);
        //Apply Rotaion to camera
        CamHolder.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX * Sensitivity);
    }
}
