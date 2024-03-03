﻿using System.Collections.Generic;
using UnityEngine;

public class PlayZone : MonoBehaviour
{
    [SerializeField] private List<GameObject> _playingObjects;
    [SerializeField] private CanvasHider _userInterface;

    public void Enter()
    {
        foreach (GameObject obj in _playingObjects)
        {
            obj.SetActive(true);
        }

        _userInterface.Show();
    }

    public void Exit()
    {
        foreach (GameObject obj in _playingObjects)
        {
            obj.SetActive(false);
        }

        _userInterface.Hide();
    }
}