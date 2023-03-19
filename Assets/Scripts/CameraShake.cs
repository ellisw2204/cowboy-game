using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            // the camera shake will be made by using an offset of the x and y positions of the camera, in random
            // positions anywhere between -1 and 1. It will also multiply by the magnitude variable that i set.

            transform.localPosition = new Vector3(x, y, originalPos.z);
            // will apply these changes to the cameras current position, not affecting the z axis.

            elapsed += Time.deltaTime;

            yield return null;
            // upon each fram that changes, it will check against the elapsed time and will keep repeating until the
            // elapsed time exceeds the duration.
        }

        transform.localPosition = originalPos;
        // once the Shake has been carried out the camera will retaurn to the original position.
    }

    //this code is known as a coroutine, which means that it will repeat until it is stopped, this also means that
    // i need to start the code with 'IEnumerator' instead of 'Void' (they essentially still do the same thing.)

}
