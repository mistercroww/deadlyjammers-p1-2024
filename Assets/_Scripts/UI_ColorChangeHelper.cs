using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ColorChangeHelper : MonoBehaviour
{
    public bool autoLerpOnEnable;
    public Image targetImg;
    public TextMeshProUGUI targetText;
    public Color color_A, color_B;
    public AnimationCurve lerpCurve;
    public float lerpDuration = 1f;

    private void OnEnable() {
        if (autoLerpOnEnable) {
            TriggerLerp();
        }
    }
    public void ResetColor() {
        if (targetImg)targetImg.color = color_A;

        if (targetText)targetText.color = color_A;
    }
    public void TriggerLerp() {
        if (targetImg == null && targetText == null) return;
        ResetColor();
        StopAllCoroutines();
        StartCoroutine(TriggerSeq());
    }
    IEnumerator TriggerSeq() {
        float lerp = 0;

        while (lerp < 1f) {

            if (targetText)targetText.color = Color.Lerp(color_A, color_B, lerpCurve.Evaluate(lerp));

            if (targetImg)targetImg.color = Color.Lerp(color_A, color_B, lerpCurve.Evaluate(lerp));

            lerp += Time.deltaTime / lerpDuration;
            yield return new WaitForEndOfFrame();
        }

        if (targetImg) targetImg.color = color_B;

        if (targetText) targetText.color = color_B;
    }
}
