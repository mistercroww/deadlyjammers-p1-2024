using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static readonly float[] NeedsMultiplier = { 1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2f, 5f };

    public static readonly int MaxDays = 10;

    public static readonly int[] DayTracks = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 3 };

    public static readonly int[] HomunculusAvatar = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };


    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static string TintString(this string txt, Color c)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(c) + ">" + txt + "</color>";
    }

    public static void CopyTargetTransform(this Transform t, Transform target)
    {
        t.SetPositionAndRotation(target.position, target.rotation);
    }

    public static float AddToValueWithMax(float value, float add, float max)
    {
        return ((value + add) > max) ? value = max : value + add;
    }

    public static float SubtractToValueWithMin(float value, float substract, float min)
    {
        return ((value - substract) < min) ? value = min : value - substract;
    }

    public static float Percentage(float current, float maxValue)
    {
        return (current / maxValue) * 100f;
    }
}

public enum InteractableType
{
    Default,
    Item,
    ItemReceiver,
    Switch,
    Door,
    Radio
}

public interface IInteractable
{
    public bool IsInteractable();
    public void TriggerInteraction();
    public InteractableType InteractionType();
}