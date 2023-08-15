using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform playerTransform;
    [SerializeField]private float distance;
    [SerializeField]private float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(-distance, distance-0.2f, -distance), lerpSpeed * Time.deltaTime);
    }
}
