using UnityEngine;

public class OnBuildMaterialsZone : MonoBehaviour
{
    PlayerInventory _player;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory player))
        {
            _player = player;
            _player.StartFillingInventory();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_player != null)
            _player.StopFillingInventory();
    }
}
