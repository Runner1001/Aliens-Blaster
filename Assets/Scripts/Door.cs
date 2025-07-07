using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject open1;
    [SerializeField] GameObject open2;
    [SerializeField] GameObject closed1;
    [SerializeField] GameObject closed2;

    [ContextMenu("Open Door")]
    public void Open()
    {
        open1.SetActive(true);
        open2.SetActive(true);
        closed1.SetActive(false);
        closed2.SetActive(false);
    }

    [ContextMenu("Close Door")]
    public void Close()
    {
        open1.SetActive(false);
        open2.SetActive(false);
        closed1.SetActive(true);
        closed2.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerInteractionController = other.GetComponent<PlayerInteractionController>();

        if(playerInteractionController != null)
        {
            playerInteractionController.Add(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var playerInteractionController = other.GetComponent<PlayerInteractionController>();

        if (playerInteractionController != null)
        {
            playerInteractionController.Remove(this);
        }
    }

    public void Interact(PlayerInteractionController playerInteractionController)
    {
        var destination = Vector2.Distance(playerInteractionController.transform.position, open1.transform.position) > 
            Vector2.Distance(playerInteractionController.transform.position, open2.transform.position)
            ? open1.transform.position 
            : open2.transform.position;

        playerInteractionController.transform.position = destination;
    }
}
