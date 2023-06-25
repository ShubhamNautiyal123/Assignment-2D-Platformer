using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Assignment.Others
{
    public class Parallax : MonoBehaviour
    {
        #region Fields

        [SerializeField] Transform mainCharacter;
        [SerializeField] float parallaxMultiplier = 0.5f; // Adjust the speed of the tree movement

        private UnityEngine.Vector3 _initialPosition;
        #endregion

        #region Monobehaviours
        private void Start()
        {
            _initialPosition = transform.position;
        }

        private void Update()
        {
            float distanceMovedByPlayer = mainCharacter.position.x - _initialPosition.x;
            transform.position = _initialPosition + UnityEngine.Vector3.right * (distanceMovedByPlayer * parallaxMultiplier);
        }
        #endregion
    }
}