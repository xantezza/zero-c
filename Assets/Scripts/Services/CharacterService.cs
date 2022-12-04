using UnityEngine;

namespace Services
{
    public class CharacterService : Service
    {
        [SerializeField] private Character _characterPrefab;
        
        public Character Character { get; private set; }

        private void Awake()
        {
            Character = Instantiate(_characterPrefab);
        }
    }
}
