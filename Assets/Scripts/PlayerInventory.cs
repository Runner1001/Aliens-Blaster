using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour, IBind<PlayerData>
{
    public Transform ItemPoint;

    private PlayerInput _playerInput;
    private List<Item> _items = new List<Item>();
    private int _currentItemIndex;
    private PlayerData _data;

    public Item EquippedItem => _items.Count >= _currentItemIndex ? _items[_currentItemIndex] : null;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].performed += UseEquippedItem;
        _playerInput.actions["EquipNext"].performed += EquipNext;

        foreach (var item in GetComponentsInChildren<Item>())
        {
            PickUp(item, false);
        }
    }

    private void OnDestroy()
    {
        _playerInput.actions["Fire"].performed -= UseEquippedItem;
        _playerInput.actions["EquipNext"].performed -= EquipNext;
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
        if (EquippedItem != null)
            EquippedItem.Use();
    }

    public void PickUp(Item item, bool isNew = false)
    {
        item.transform.SetParent(ItemPoint);
        item.transform.localPosition = Vector3.zero;
        _items.Add(item);
        _currentItemIndex = _items.Count - 1;
        ToggleEquippedItem();

        var collider = item.gameObject.GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        if (isNew && _data.Items.Contains(item.name) == false)
        {
            _data.Items.Add(item.name);
        }
    }

    public void Bind(PlayerData data)
    {
        _data = data;
        foreach (var itemName in data.Items)
        {
            var itemGameObject = GameObject.Find(itemName);

            if (itemGameObject != null && itemGameObject.TryGetComponent<Item>(out var item))
            {
                PickUp(item);
            }
            else
            {
                item = GameManager.Instance.GetItem(itemName);
                if (item != null)
                    PickUp(item);
            }
        }
    }
}
