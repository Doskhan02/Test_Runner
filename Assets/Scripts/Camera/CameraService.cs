using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not set for CameraService.");
            return;
        }
        
    }
    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3.Lerp(transform.position, targetPosition,0.5f);
            transform.position = targetPosition;
        }
    }
}
