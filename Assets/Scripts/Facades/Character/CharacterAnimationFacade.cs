using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Facades.Character
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(NavMeshAgent))]
    public class CharacterAnimationFacade : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Animator _animator;
        [SerializeField, HideInInspector] private SpriteRenderer _spriteRenderer;
        [SerializeField, HideInInspector] private NavMeshAgent _navMeshAgent;

        private bool _cachedSpriteFlipX;

        private int _isMovingAnimationParameterHash = Animator.StringToHash("IsMoving");
        private int _OnAttackAnimationParameterHash = Animator.StringToHash("OnAttack");
        private int _OnDeathAnimationParameterHash = Animator.StringToHash("OnDeath");

        private void Awake()
        {
            _cachedSpriteFlipX = _spriteRenderer.flipX;
        }

        private void OnValidate()
        {
            if (_animator == null) _animator = GetComponent<Animator>();
            if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_navMeshAgent == null) _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _animator.SetBool(_isMovingAnimationParameterHash, _navMeshAgent.velocity != Vector3.zero);

            var targetSpriteOrientationLeft = _navMeshAgent.velocity.x < 0;

            if (_cachedSpriteFlipX != targetSpriteOrientationLeft && !Mathf.Approximately(_navMeshAgent.velocity.x, 0))
            {
                _spriteRenderer.flipX = targetSpriteOrientationLeft;
                _cachedSpriteFlipX = targetSpriteOrientationLeft;
            }
        }
    }
}
