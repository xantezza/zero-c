using Facades.Character;
using UnityEngine;

public class BushTiles : MonoBehaviour
{
    [SerializeField] private float _characterSpeedModifier;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<CharacterNavMeshFacade>(out var characterNavMesh))
        {
            characterNavMesh.AddSpeedModifier(this, _characterSpeedModifier);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<CharacterNavMeshFacade>(out var characterNavMesh))
        {
            characterNavMesh.RemoveSpeedModifier(this);
        }
    }
}
