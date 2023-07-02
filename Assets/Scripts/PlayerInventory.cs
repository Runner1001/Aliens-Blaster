using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Transform ItemPoint;

    private PlayerInput _playerInput;
    private List<IItem> _items = new List<IItem>();
    private int _currentItemIndex;

    public IItem EquippedItem => _items.Count >= _currentItemIndex ? _items[_currentItemIndex] : null;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].performed += UseEquippedItem;
        _playerInput.actions["EquipNext"].performed += EquipNext;

        foreach (var item in GetComponentsInChildren<IItem>())
        {
            PickUp(item);
        }
    }

    private void EquipNext(InputAction.CallbackContext obj)
    {
        _currentItemIndex++;
        if (_currentItemIndex >= _items.Count)
        {
            _currentItemIndex = 0;
        }

        ToggleEquippedItem();
    }

    private void ToggleEquippedItem()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i].gameObject.SetActive(i == _currentItemIndex);
        }
    }

    private void UseEquippedItem(InputAction.CallbackContext obj)
    {
        if(EquippedItem != null)
            EquippedItem.Use();
    }

    public void PickUp(IItem item)
    {
        item.transform.SetParent(ItemPoint);
        item.transform.localPosition = Vector3.zero;
        _items.Add(item);
        _currentItemIndex = _items.Count -1;
        ToggleEquippedItem();

        var collider = item.gameObject.GetComponent<Collider2D>();
        if(collider != null )
            collider.enabled = false;
    }
}
