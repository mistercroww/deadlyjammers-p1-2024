using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_controller : MonoBehaviour
{
    public bool autoFadeToNextScene;
    public float autoFadeDelay = 20f;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        if (autoFadeToNextScene) {
            Invoke(nameof(FadeScene), autoFadeDelay);
        }
    }
    private void FadeScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
    }
}
