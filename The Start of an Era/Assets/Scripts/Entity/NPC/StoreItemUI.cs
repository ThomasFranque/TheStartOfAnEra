using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StoreItemUI : MonoBehaviour
{
    private TextMeshProUGUI _itemValueText;
    private Button _button;
    private IValue _item;

    private void Awake()
    {
        _itemValueText = GetComponentInChildren<TextMeshProUGUI>();
        _button = GetComponent<Button>();
    }

    public void SetItemValue(IValue item)
    {
        if (item != null)
        {
            _item = item;
            _itemValueText.text = _item.Price.ToString();
        }
    }

    public void SetEvent(Action onclick)
    {
        _button.onClick.AddListener(() => onclick());
    }

    public void UpdateValue()
    {
        _item.TimesBought++;
        _itemValueText.text = _item.Price.ToString();
    }
}
