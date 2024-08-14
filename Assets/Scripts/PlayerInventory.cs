using System;
using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    int _maxInventoryCapacity = 5;
    public int CurrentInventoryAmount { get; set; }

    public Action OnInventoryCountChange;

    Coroutine _coroutine;
    WaitForSeconds _waitOneSecond = new WaitForSeconds(1);

    public void StartFillingInventory()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(FillingInventoryCoroutine());
    }

    public void StopFillingInventory()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    IEnumerator FillingInventoryCoroutine()
    {
        while (_maxInventoryCapacity != CurrentInventoryAmount)
        {
            CurrentInventoryAmount++;
            UpdateUIInventoryCount();
            yield return _waitOneSecond;
        }
    }

    public void UpdateUIInventoryCount()
    {
        OnInventoryCountChange?.Invoke();
    }
}
