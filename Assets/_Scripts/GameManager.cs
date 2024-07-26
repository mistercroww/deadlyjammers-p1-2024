using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        Application.targetFrameRate = 24;
        instance = this;
    }
    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
