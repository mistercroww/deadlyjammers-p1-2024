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

    Vector2 curMovementVect, mouseRotation;
    Transform t;
    private void Awake() {
        instance = this;
        t = this.transform;
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
            cc.Move(speed * Time.deltaTime * moveVect.normalized);
            if (mouseRotation != Vector2.zero) {
                t.Rotate(mouseRotation.x * rotationSpeed * Vector3.up);
                cameraRotator.localRotation = Quaternion.Euler(Vector3.right * mouseRotation.y);
            }
        }
    }
    public static void SetPlayerDriveState(bool b) {
        if (instance == null) return;
        instance.canDrive = b;
    }
}
