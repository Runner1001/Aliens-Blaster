using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void Use();

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.PickUp(this, true);

        }
    }
}