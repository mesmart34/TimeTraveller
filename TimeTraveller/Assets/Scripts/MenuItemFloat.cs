using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemFloat : MonoBehaviour
{
    [SerializeField] float movementRange = 0.0f;
    [SerializeField] float movementSpeed = 0.0f;
    [SerializeField] float rotationRange = 0.0f;
    [SerializeField] float rotationSpeed = 0.0f;
    private Vector3 _position;
    private float _startTime;

    private void Start() {
        _position = transform.position;
        _startTime = Random.Range(0, 100);
    }

    private void Update() {
        _startTime += Time.deltaTime;
        
        var y = Mathf.Sin(_startTime * movementSpeed) * movementRange;
        var offset = new Vector3(0, y);
        transform.position = _position + offset;

        var rotX = Mathf.Sin(_startTime * rotationSpeed) * rotationRange;
        var rotationOffset = Quaternion.Euler(0, 0, rotX);
        transform.rotation = rotationOffset; 
    }
}
