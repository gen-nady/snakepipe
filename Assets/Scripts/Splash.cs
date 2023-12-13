// /*
// Created by Darsan
// */

using System;
using System.Collections;
using MyGame;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        GameManager.LoadScene("MainMenu");
    }
}