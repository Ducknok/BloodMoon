using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    [SerializeField] private float smoothTime;

    [SerializeField] private Vector3 positionOffSet;

    //[Header("Axis Limitation")]
    //[SerializeField] private Vector2 xLimit;
    //[SerializeField] private Vector2 yLimit;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void LateUpdate()
    {
        Vector3 targetPosition = target.position + positionOffSet;
        //targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y), Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y), -10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
