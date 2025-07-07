using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    TMP_Text interactionText;
    PlayerInput playerInput;
    List<Door> doors = new();

    void Awake()
    {
        interactionText = GetComponentInChildren<TMP_Text>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Interact"].performed += Interact;
        interactionText.gameObject.SetActive(false);
    }

    private void Interact(InputAction.CallbackContext context)
    {
        foreach (var door in doors)
        {
            door.Interact(this);
        }
    }

    public void Add(Door door)
    {
        doors.Add(door);
        interactionText.gameObject.SetActive(true);
    }

    internal void Remove(Door door)
    {
        doors.Remove(door);
        interactionText.gameObject.SetActive(false);
    }
}
