using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouvment : MonoBehaviour
{
    [SerializeField, Range(0, 500)] public float StamenaMax = 500;
    [SerializeField, Range(0, 500)] public float Stamena = 100;
    public Transform cam;
    public CharacterController characterController;
    public Vector3 Velocity;
    public float Speed;
    public float SpeedSprint;
    public float SpeedWalking;
    public float Gravity = -9.8f;
    public float jumpHight = 7.5f;
    public int state;
    void Start()
    {
        Speed = SpeedWalking;
        characterController = GetComponent<CharacterController>();
    }
    public void Update()
    {
        HeightManager();
        Stamena = Mathf.Clamp(Stamena, 0, StamenaMax);
    }
    public void Move(Vector2 Input)
    {
        Velocity.x = Input.x;
        Velocity.z = Input.y;
        characterController.Move(transform.TransformDirection(Velocity) * Speed * Time.deltaTime);
    }
    public void MoveAir(Vector2 Input)
    {
        characterController.Move(transform.TransformDirection(Velocity) * Speed * Time.deltaTime);
    }
    public void ApplyGravity()
    {
        Velocity.y += (Gravity*.5f) * Time.deltaTime;
        characterController.Move(transform.TransformDirection(Velocity) * Time.deltaTime);
    }
    public void Jump()
    {
        if (Stamena>100 && characterController.isGrounded && state<1)       
        {
            Stamena -= 100;
            Velocity.y = Mathf.Sqrt(jumpHight* -0.03f * Gravity);
        }
    }
    public void Sprint(float Input)
    {   if (Input==0 && state == -1) { state = 0; }
        if (Input>0 && Stamena>0f && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo, 1.5f))
        {
            state = -1;
            Speed = Mathf.Lerp(Speed, SpeedSprint, 3.5f * Time.deltaTime);
            Stamena -= 0.3f;
        }
        else
        {
            Speed = Mathf.Lerp(Speed, SpeedWalking, 10 * Time.deltaTime);
            StamenaRegen();
        }
    }
    public void Crouch()
    {
        if (characterController.isGrounded && characterController.height == 1f && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo, 1.5f))
        {
            state = 0;
        }
        else
        {
            state = 1;
        }
    }
    public void Prone()
    {
        if (characterController.isGrounded && characterController.height == 0.5f && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hitInfo, 1.5f)) 
        {
            state = 0;
        }
        else
        {
            state = 2;
        }
    }
    private void HeightManager()
    {
        if (state == -1)
        {
            characterController.height = 2f;
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0.6f, 0), 3 * Time.deltaTime);
            return;
        }
        if (state == 0) 
        {
            characterController.height = 2f;
            cam.transform.localPosition=Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0.6f, 0), 3 * Time.deltaTime);
            return; 
        }
        if (state == 1) 
        {
            characterController.height = 1f;
            cam.transform.localPosition=Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0.2f, 0), 5 * Time.deltaTime);
            Speed = SpeedWalking * 0.4f; 
            return; 
        }
        if (state == 2) 
        {
            characterController.height = 0.5f;
            cam.transform.localPosition=Vector3.Lerp(cam.transform.localPosition, new Vector3(0, -0.3f, 0), 5 * Time.deltaTime);
            Speed = SpeedWalking * 0.2f; 
            return; 
        }
    }
    private void StamenaRegen()
    {
        if (characterController.velocity.magnitude > 0)
        {
            Stamena += 0.25f;
        }
        else//Faster Regen if Standing Still
        {
            Stamena += 0.75f;
        }
    }
}
