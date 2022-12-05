using System;
using UnityEngine;

namespace Services
{
    public class InputService : Service
    { 
        public event Action<Vector2, Collider2D> OnWorldPointClicked;

        private CameraService _cameraService;
        private Camera _currentCamera;
        private RaycastHit2D[] _raycastHitsBuffer = new RaycastHit2D[3];

        private void Start()
        {
            _cameraService = ServicesProvider.Get<CameraService>();
            _currentCamera = _cameraService.MainCamera;
            _cameraService.OnMainCameraChanged += OnMainCameraChanged;
        }

        private void OnMainCameraChanged(Camera newMainCamera)
        {
            _currentCamera = newMainCamera;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var mousePosition = Input.mousePosition;
                var ray = _currentCamera.ScreenPointToRay(mousePosition);

                for (int i = 0; i < _raycastHitsBuffer.Length; i++)
                {
                    _raycastHitsBuffer[i] = default;
                }

                Physics2D.GetRayIntersectionNonAlloc(ray, _raycastHitsBuffer, float.MaxValue);

                for (int i = 0; i < _raycastHitsBuffer.Length; i++)
                {
                    var raycastHit = _raycastHitsBuffer[i];
                    if (raycastHit.collider == null) continue;

                    OnWorldPointClicked?.Invoke(raycastHit.point, raycastHit.collider);

                    return;
                }

                OnWorldPointClicked?.Invoke(_currentCamera.ScreenToWorldPoint(mousePosition), null);
            }
        }
    }
}