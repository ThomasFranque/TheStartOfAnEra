using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Store : MonoBehaviour
{
    private StoreCanvas _storeCanvas;
    protected StoreStock _stock;

    private void Awake()
    {
        // Create a stock and add some items to it
        _stock = new StoreStock();
        PotionDummyTest p1 = new PotionDummyTest(20);
        PotionDummyTest p2 = new PotionDummyTest(120);
        PotionDummyTest p4 = new PotionDummyTest(220);
        PotionDummyTest p3 = new PotionDummyTest(45);
        PotionDummyTest p5 = new PotionDummyTest(300);

        _stock.AddStock(p1);
        _stock.AddStock(p2);
        _stock.AddStock(p3);
        _stock.AddStock(p4);
        _stock.AddStock(p5);

        // Add the same object for debug purposes
        _stock.AddStock(p1);
    }

    private void Start()
    {
        // Get the UI element for this store
        _storeCanvas = GetComponentInChildren<StoreCanvas>();
        _storeCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        // If you open the store
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Activates UI and populates it with the shop items
            _storeCanvas.gameObject.SetActive(!_storeCanvas.gameObject.activeSelf);
            if (_storeCanvas.gameObject.activeSelf)
            {
                _storeCanvas.UpdateStoreUI(PopulateShop);
            }
        }
    }

    public void SetStoreActive()
    {
        _storeCanvas.gameObject.SetActive(!_storeCanvas.gameObject.activeSelf);
        if (_storeCanvas.gameObject.activeSelf)
        {
            _storeCanvas.UpdateStoreUI(PopulateShop);
        }
    }

    private IEnumerable<StoreItemUI> PopulateShop(Transform gridLayoutTransform, StoreItemUI itemPrefab)
    {
        List<StoreItemUI> populatedItems = new List<StoreItemUI>();
        StoreItemUI itemObj;
        foreach (IValue item in _stock.Items)
        {
            itemObj = Instantiate(itemPrefab, gridLayoutTransform);

            itemObj?.SetItemValue(item);
            itemObj.SetEvent(OnItemBuy);
            populatedItems.Add(itemObj);
        }

        return populatedItems;
    }

    public void OnItemBuy()
    {
        UpdatePrices();
    }

    private void UpdatePrices()
    {
        _stock.UpdatePrices();
    }
}
