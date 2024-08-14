using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _boxCount;
    [SerializeField] PlayerInventory _playerInventory;

    void OnEnable()
    {
        _playerInventory.OnInventoryCountChange += ChangeBoxCountUI;
    }

    void OnDisable()
    {
        _playerInventory.OnInventoryCountChange -= ChangeBoxCountUI;
    }

    void ChangeBoxCountUI()
    {
        _boxCount.text = _playerInventory.CurrentInventoryAmount.ToString();
    }
}
