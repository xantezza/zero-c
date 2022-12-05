using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Facades.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterNavMeshFacade : MonoBehaviour
    {
        [SerializeField, HideInInspector] private NavMeshAgent _navMeshAgent;
        [SerializeField, HideInInspector] private Transform _cachedTransform;

        private Dictionary<Object, float> _speedModifiers = new Dictionary<Object, float>();

        private float _averageModifier = 1f;
        private float _defaultAgentSpeed;

        private void Awake()
        {
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            _defaultAgentSpeed = _navMeshAgent.speed;

            _speedModifiers.Add(this, 1f);
        }

        private void OnValidate()
        {
            if (_navMeshAgent == null) _navMeshAgent = GetComponent<NavMeshAgent>();
            if (_cachedTransform == null) _cachedTransform = GetComponent<Transform>();
        }

        public void AddSpeedModifier(Object modifier, float value)
        {
            if (_speedModifiers.ContainsKey(modifier))
            {
                _speedModifiers[modifier] = value;
            }
            else
            {
                _speedModifiers.Add(modifier, value);
            }

            RecalculateAverageModifier();
        }

        public void RemoveSpeedModifier(Object modifier)
        {
            if (_speedModifiers.ContainsKey(modifier))
            {
                _speedModifiers.Remove(modifier);
            }

            RecalculateAverageModifier();
        }

        private void RecalculateAverageModifier()
        {
            _averageModifier = _speedModifiers.Values.Average();
            _navMeshAgent.speed = _defaultAgentSpeed * _averageModifier;
        }

        public void UpdateAgentTargetPosition(Vector2 targetPosition)
        {
            _navMeshAgent.SetDestination(new Vector3(targetPosition.x, targetPosition.y, _cachedTransform.position.z));
        }
    }
}