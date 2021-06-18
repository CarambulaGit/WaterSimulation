using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchimedesForce : MonoBehaviour {
    [SerializeField] private string tagOfObjects = "Box";
    [SerializeField] private BoxCollider collider;
    private Vector3 forceAtUnit = new Vector3(0, 20f, 0);
    private float dragAtUnit = 1f;
    private float yCoordOfTopPoint;

    private void Start() {
        yCoordOfTopPoint = collider.bounds.max.y;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag(tagOfObjects)) {
            if (other.TryGetComponent<Rigidbody>(out var rb)) {
                var otherBounds = other.bounds;
                var lowPoint = otherBounds.min;
                var topPoint = otherBounds.max;
                var deltaY = topPoint.y > yCoordOfTopPoint ? yCoordOfTopPoint - lowPoint.y : topPoint.y - lowPoint.y;
                rb.drag = dragAtUnit * deltaY;
                rb.angularDrag = dragAtUnit * deltaY;
                rb.AddForceAtPosition(forceAtUnit * deltaY, rb.worldCenterOfMass);
            }
        }
    }
}