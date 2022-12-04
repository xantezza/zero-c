using Services;
using UnityEngine;

public class Character : MonoBehaviour
{
    private AudioService _audioService;
    private UIService _UIService;

    private void Awake()
    {
        _audioService = ServicesProvider.Get<AudioService>();
        _UIService = ServicesProvider.Get<UIService>();
    }
}
