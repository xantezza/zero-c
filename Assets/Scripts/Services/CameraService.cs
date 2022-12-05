using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class CameraService : Service
    {
        public event Action<Camera> OnMainCameraChanged;

        [field: SerializeField, ReadOnly] public Camera MainCamera { get; private set; }

        private void Awake()
        {
            MainCamera = Camera.main;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene current, Scene next)
        {
            MainCamera = Camera.main;
            OnMainCameraChanged?.Invoke(MainCamera);
        }
    }
}
