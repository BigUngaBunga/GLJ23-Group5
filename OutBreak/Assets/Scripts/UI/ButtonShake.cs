using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShake : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float angularSpeed;
    [Range(0.01f, 1f)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float waitDuration;
    private WaitForSeconds waitTime;
    private float initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation.eulerAngles.z;
        waitTime = new WaitForSeconds(waitDuration);
    }

    public void OnSelect()
    {
        StopAllCoroutines();
        StartCoroutine(StartShaking());
    }

    public void OnDeselect()
    {
        StopAllCoroutines();
        StartCoroutine(StopShaking());
    }

    private IEnumerator StartShaking()
    {
        float sinAngle = 0;
        while (true)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(sinAngle += angularSpeed) * amplitude);
            yield return waitTime;
        }
    }

    private IEnumerator StopShaking()
    {
        yield return RotateTowards(initialRotation);
    }

    private IEnumerator RotateTowards(float targetRotation)
    {
        float percentage = 0;
        float initialRotation = transform.rotation.eulerAngles.z;
        while(percentage < 1f) { 
            percentage = Mathf.Clamp(percentage + rotationSpeed, 0f, 1f);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(initialRotation, targetRotation, percentage));
            yield return waitTime;
        }

    }
}
