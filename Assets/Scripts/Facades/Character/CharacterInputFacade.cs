using Services;
using UnityEngine;

namespace Facades.Character
{
    [RequireComponent(typeof(CharacterNavMeshFacade))]
    public class CharacterInputFacade : MonoBehaviour
    {
        private InputService _inputService;

        [SerializeField, HideInInspector] private CharacterNavMeshFacade _characterNavMeshFacade;

        private void OnValidate()
        {
            if (_characterNavMeshFacade == null) _characterNavMeshFacade = GetComponent<CharacterNavMeshFacade>();
        }

        private void Awake()
        {
            _inputService = ServicesProvider.Get<InputService>();
        }

        private void OnEnable()
        {
            _inputService.OnWorldPointClicked += OnWorldPointClicked;
        }

        private void OnDisable()
        {
            _inputService.OnWorldPointClicked -= OnWorldPointClicked;
        }

        private void OnWorldPointClicked(Vector2 point, Collider2D collider)
        {
            if (collider != null && collider.TryGetComponent<TreeProp>(out var tree)){

                //TODO: state machine chop tree state
                _characterNavMeshFacade.UpdateAgentTargetPosition(tree.CutDownPosition.position);
                return;
            }

            _characterNavMeshFacade.UpdateAgentTargetPosition(point);
        }
    }
}
