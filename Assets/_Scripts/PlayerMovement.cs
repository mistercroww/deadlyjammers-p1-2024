using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public CharacterController cc;
    public bool canDrive = true;
    public float speed = 20, gravity = -9.8f;
    public float rotationSpeed = 1f;
    public Transform cameraRotator;
    public Vector2 yawAngleRange = new(-75f,60f);
    public AudioSource stepsAudio;
    public AudioClip[] stepClips;
    public float stepTime = 0.66f;
    public float stepVolume = 0.33f;
    float curStepTime;

    Vector2 curMovementVect, mouseRotation;
    Transform t;
    private void Awake() {
        instance = this;
        t = this.transform;
        curStepTime = stepTime / 2f;
    }
    private void Update() {
        if (cc == null) return;
        curMovementVect.x = Input.GetAxisRaw("Horizontal");
        curMovementVect.y = Input.GetAxisRaw("Vertical");

        mouseRotation.x = Input.GetAxisRaw("Mouse X");
        mouseRotation.y -= Input.GetAxisRaw("Mouse Y");
        mouseRotation.y = Mathf.Clamp(mouseRotation.y, yawAngleRange.x, yawAngleRange.y);

        Vector3 moveVect = (t.forward * curMovementVect.y) + (t.right * curMovementVect.x);
        moveVect.y = gravity;

        if (canDrive) {
            FootstepsHandler();
            cc.Move(speed * Time.deltaTime * Vector3.ClampMagnitude(moveVect, 1f));
            if (mouseRotation != Vector2.zero) {
                t.Rotate(mouseRotation.x * rotationSpeed * Vector3.up);
                cameraRotator.localRotation = Quaternion.Euler(Vector3.right * mouseRotation.y);
            }
        }
    }
    //funcion estatica para activar o desactivar el movimiento del player.
    public static void SetPlayerDriveState(bool b) {
        if (instance == null) return;
        instance.canDrive = b;
    }
    private void FootstepsHandler() {
        if (curMovementVect != Vector2.zero) {
            curStepTime += Time.deltaTime;
            if(curStepTime > stepTime) {
                curStepTime = 0;
                stepsAudio.PlayOneShot(stepClips[Random.Range(0, stepClips.Length)], stepVolume);
            }
        }
        else {
            curStepTime = stepTime/2f;
        }
    }
}
