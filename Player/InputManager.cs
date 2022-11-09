using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private InputMaster Input;
    private InputMaster.PlayerActions PlayerActions;
    private Mouvment moveScript;
    private PlayerLook lookScript;
    // Start is called before the first frame update
    void Awake()//Keys
    {
        Input = new InputMaster();
        PlayerActions = Input.Player;
        moveScript = GetComponent<Mouvment>();
        lookScript = GetComponent<PlayerLook>();
        PlayerActions.Jump.performed += ctx => moveScript.Jump();
        PlayerActions.Crouch.performed += ctx => moveScript.Crouch();
        PlayerActions.Prone.performed += ctx => moveScript.Prone();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }//Init
    private void Update()
    {
        lookScript.Look(PlayerActions.Look.ReadValue<Vector2>());
    }//Mouse Mouvment
    void FixedUpdate()
    {
        if (moveScript.characterController.isGrounded)
        {
            moveScript.Move(PlayerActions.Move.ReadValue<Vector2>());
            moveScript.Sprint(PlayerActions.Sprint.ReadValue<float>());
        }
        else
        {
            moveScript.MoveAir(PlayerActions.Move.ReadValue<Vector2>());
            moveScript.ApplyGravity();
        }
    }//Player Mouvment
    private void OnEnable()
    {
        PlayerActions.Enable();
    }
    private void OnDisable()
    {
        PlayerActions.Disable();
    }
}
