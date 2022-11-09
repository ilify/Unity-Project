using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class HeadBobController : MonoBehaviour
{
    public bool Enable = true;
    [SerializeField, Range(0, 0.1f)] public float Amplitude = 0.015f;
    [SerializeField, Range(0, 30)] public float Frequency = 10.0f;
    [SerializeField, Range(40f, 70f)] public float FovMin = 60.0f;
    [SerializeField, Range(70f, 110f)] public float FovMax = 80.0f;
    [SerializeField] private Transform cam = null;
    [SerializeField] private Transform camHolder = null;
    public Camera Camera;
    private Mouvment moveScript;
    private Vector3 StartPos;
    private CharacterController Player;
    private void Awake()
    {
        Player = GetComponent<CharacterController>();
        StartPos = cam.localPosition;
        moveScript = GetComponent<Mouvment>();
    }
    void Update()
    {
        if (!Enable) return;
        UpdateValues(moveScript.characterController.velocity.magnitude);
        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
    }
    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }
    private void CheckMotion()
    {
        if(!Player.isGrounded) return;
        if (moveScript.characterController.velocity.magnitude == 0) 
        {
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, FovMin,Time.deltaTime);
            PlayMotion(BreathMotion());
            return;
        }
        if(moveScript.characterController.velocity.magnitude > 0)
        {
            PlayMotion(FootStepMotion());
            return;
        }
    }
    private Vector3 BreathMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += (Mathf.Sin(Time.time * (Frequency/3)) * (Amplitude/5))+(Amplitude/2.5f);
        return pos;
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Abs(Mathf.Sin(Time.time * Frequency)) * Amplitude;
        pos.x += Mathf.Cos(Time.time * Frequency) * (Amplitude/3.5f);
        return pos;
    }
    private void UpdateValues(float speed)
    {
        if (moveScript.state == 2)
        {
            Frequency = 5f;
            Amplitude = 0.0035f;
        }
        if (moveScript.state == 1)
        {
            Frequency = 5f;
            Amplitude = 0.006f;
        }
        if (moveScript.state == 0 && speed < moveScript.SpeedWalking+1f)
        {
            Frequency = 5f;
            Amplitude = 0.005f;
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, FovMin, 2*Time.deltaTime);
        }
        if (moveScript.state == -1 && speed > moveScript.SpeedWalking + 1f)
        {
            Frequency = 7.5f;
            Amplitude = 0.0075f;
            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView, FovMax, Time.deltaTime);
        }
    }
    private void ResetPosition()
    {
        if (cam.localPosition == StartPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, StartPos, 5 * Time.deltaTime);
    }
    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 13.0f;
        return pos;
    }
}
