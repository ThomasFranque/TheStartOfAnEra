using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreItemUI : MonoBehaviour
{
    private TextMeshProUGUI _itemValueText;

    private void Awake()
    {
        _itemValueText = GetComponentInChildren<TextMeshProUGUI>();
        _itemValueText.text = 0.ToString();
    }

    public void SetItemValue(int value)
    {
        _itemValueText.text = value.ToString();
    }
}
