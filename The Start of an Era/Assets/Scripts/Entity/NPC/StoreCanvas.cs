using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Class that treats all the instantiation and UI of the store
public class StoreCanvas : MonoBehaviour
{
    // Grid layout of the store
    [SerializeField]
    private RectTransform _gridLayout = null;
    // Item prefab of what to instantiate with component StoreItemUI
    [SerializeField]
    private StoreItemUI _itemPrefab = null;

    private IEnumerable<StoreItemUI> _activeItems;

    public void UpdateStoreUI
        (Func<Transform, StoreItemUI, IEnumerable<StoreItemUI>> instantiation)
    {
        _activeItems = instantiation(_gridLayout, _itemPrefab);
    }

    public IEnumerable<StoreItemUI> ActiveItems()
    {
        return _activeItems;
    }

    private void OnDisable()
    {
        if (_gridLayout.transform.childCount > 0)
        {
            for (int i = 0; i < _gridLayout.childCount; i++)
            {
                Destroy(_gridLayout.GetChild(i).gameObject);
            }
        }

        _activeItems = null;
    }
}
