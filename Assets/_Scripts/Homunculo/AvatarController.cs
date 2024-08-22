using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject[] avatars;
    public GameManager _gameManager;

    private void Start()
    {
        GrowthAvatar();
    }

    public void GrowthAvatar()
    {
        DisableAllAvatars();
        avatars[ExtensionMethods.CurrentAvatar(_gameManager.currentDay)].SetActive(true);

        print("Se cambio el avatar al nro" + (ExtensionMethods.HomunculusAvatar[_gameManager.currentDay]));
    }

    private void DisableAllAvatars()
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false);
        }
    }
}