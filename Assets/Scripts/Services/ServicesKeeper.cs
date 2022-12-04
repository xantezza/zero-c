using System.Collections;
using UnityEngine;

namespace Services
{
    public class ServicesKeeper : ScriptableObject
    {
        [field:SerializeField] public Service[] Services { get; private set; }
    }
}