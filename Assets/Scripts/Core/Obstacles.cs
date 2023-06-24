using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]  float moveSpeed = 2f;
    [SerializeField]  float verticalRange = 5f;

    private bool _isMovingUp = true;
    private float _initialYPosition;

    private void Start()
    {
        _initialYPosition = transform.position.y;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        float direction = _isMovingUp ? 1f : -1f;
        float newYPosition = _initialYPosition + (direction * verticalRange);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, newYPosition, transform.position.z), moveSpeed * Time.deltaTime);

        if (transform.position.y >= newYPosition && _isMovingUp)
            _isMovingUp = false;
        else if (transform.position.y <= newYPosition && !_isMovingUp)
            _isMovingUp = true;
    }
}
