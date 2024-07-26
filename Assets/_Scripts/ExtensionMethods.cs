using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static float Remap(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public static string TintString(this string txt, Color c) {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + txt + "</color>";
    }
    public static void CopyTargetTransform(this Transform t, Transform target) {
        t.SetPositionAndRotation(target.position, target.rotation);
    }
}
