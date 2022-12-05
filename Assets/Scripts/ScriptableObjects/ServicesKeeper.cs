using Sirenix.OdinInspector;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Services
{
    public class ServicesKeeper : ScriptableObject
    {
        [field:SerializeField] public Service[] Services { get; private set; }

        private void OnValidate()
        {
            Services = Services.Where(x => x != null).ToArray();
        }

        [Button]
        private void LoadAllServices()
        {
            Services = Resources.FindObjectsOfTypeAll<Service>();
        }
    }
}