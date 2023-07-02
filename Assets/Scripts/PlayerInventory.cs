using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Transform ItemPoint;

    private PlayerInput _playerInput;
    private List<Key> _items = new List<Key>();
    private int _currentItemIndex;

    public Key EquippedKey => _items.Count >= _currentItemIndex ? _items[_currentItemIndex] : null;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].performed += UseEquippedItem;
        _playerInput.actions["EquipNext"].performed += EquipNext;
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
        if(EquippedKey)
            EquippedKey.Use();
    }

    public void PickUp(Key key)
    {
        key.transform.SetParent(ItemPoint);
        key.transform.localPosition = Vector3.zero;
        _items.Add(key);
        _currentItemIndex = _items.Count -1;
        ToggleEquippedItem();
    }
}
