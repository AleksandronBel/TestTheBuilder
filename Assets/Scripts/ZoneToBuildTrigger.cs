using System.Collections;
using TMPro;
using UnityEngine;

public class ZoneToBuildTrigger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _countToBuild;
    [SerializeField] GameObject[] _buildParts;
    [SerializeField] GameObject _zoneToBuild;
    [SerializeField] int _boxesToBuild;

    int _partsPerBox;
    int _currentCountBoxes;

    PlayerInventory _player;

    WaitForSeconds _waitOneSecond = new WaitForSeconds(1);

    Coroutine _coroutine;

    void Start()
    {
        _partsPerBox = _buildParts.Length / _boxesToBuild;

        if (_partsPerBox == 0)
            _partsPerBox = 1;

        _countToBuild.text = _boxesToBuild.ToString();

        foreach (var part in _buildParts)
            part.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory player))
        {
            _player = player;

            if (_coroutine == null && _boxesToBuild != 0)
                _coroutine = StartCoroutine(WastingInventoryCoroutine());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_player != null)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }

    void BuildParts(int boxesUsed)
    {
        int partsToActivate = _partsPerBox * boxesUsed;
        for (int i = 0; i < partsToActivate && i < _buildParts.Length; i++)
        {
            if (!_buildParts[i].activeSelf)
                _buildParts[i].SetActive(true);
        }
    }

    IEnumerator WastingInventoryCoroutine()
    {
        while (_player.CurrentInventoryAmount != 0)
        {
            _player.CurrentInventoryAmount--;
            _boxesToBuild--;
            _currentCountBoxes++;

            _player.OnInventoryCountChange();
            _countToBuild.text = _boxesToBuild.ToString();

            BuildParts(_currentCountBoxes);

            if (_boxesToBuild == 0)
            {
                EndBuild();
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                yield return null;
            }

            yield return _waitOneSecond;
        }
    }

    void EndBuild()
    {
        _zoneToBuild.SetActive(false);
    }
}
