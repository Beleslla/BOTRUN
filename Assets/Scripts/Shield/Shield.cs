using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 mousePos;

    [SerializeField] private float turnSpeed = .01f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, mousePos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, turnSpeed * Time.deltaTime);
    }
}
