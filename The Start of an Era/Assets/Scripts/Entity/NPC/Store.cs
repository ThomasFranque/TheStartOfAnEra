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
        _stock = new StoreStock();
        PotionDummyTest p1 = new PotionDummyTest(20);
        PotionDummyTest p2 = new PotionDummyTest(120);
        PotionDummyTest p4 = new PotionDummyTest(220);
        PotionDummyTest p3 = new PotionDummyTest(45);

        _stock.AddStock(p1);
        _stock.AddStock(p2);
        _stock.AddStock(p3);
        _stock.AddStock(p4);
        _stock.AddStock(p1);
    }

    private void Start()
    {
        _storeCanvas = GetComponentInChildren<StoreCanvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _storeCanvas.gameObject.SetActive(!_storeCanvas.gameObject.activeSelf);
            _storeCanvas.UpdateStoreUI(PopulateShop);
        }
    }

    private void PopulateShop(Transform gridLayoutTransform, StoreItemUI itemPrefab)
    {
        foreach (IValue item in _stock.Items)
        {
            StoreItemUI itemObj = Instantiate(itemPrefab, gridLayoutTransform);

            itemObj.SetItemValue(item.Price);
            Debug.Log(item.Price);
        }
    }

    private void OnItemBuy()
    {
        ItemBuy?.Invoke();
        _stock.TimesBought++;
    }

    public event Action ItemBuy; 
}
