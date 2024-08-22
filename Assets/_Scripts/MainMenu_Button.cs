using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject indicatorObj;
    public float sideOffset = 25f;

    private void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (indicatorObj) {
            indicatorObj.transform.position = transform.position + (Vector3.right * sideOffset);
            indicatorObj.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        if (indicatorObj) {
            indicatorObj.SetActive(false);
        }
    }
}
