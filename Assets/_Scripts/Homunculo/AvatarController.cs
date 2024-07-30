using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject[] avatars;
    public GameManager _gameManager;


    public void GrowthAvatar()
    {
        DisableAllAvatars();
        avatars[ExtensionMethods.HomunculusAvatar[_gameManager.currentDay - 1] - 1].SetActive(true);

        print("Se cambio el avatar al nro" + (ExtensionMethods.HomunculusAvatar[_gameManager.currentDay] - 1));
    }

    private void DisableAllAvatars()
    {
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false);
        }
    }
}