using UnityEngine;
using System.Collections;

public class TitleNeedleAnimation : MonoBehaviour
{
    [SerializeField] private float minAngle = 170f;
    [SerializeField] private float midAngle = 70f;
    [SerializeField] private float maxAngle = 0f;

    [SerializeField] private float moveUpDuration = 0.4f;
    [SerializeField] private float moveDownDuration = 0.7f;
    [SerializeField] private float waitDuration = 0.6f;

    private void Start()
    {
        transform.localRotation =
            Quaternion.Euler(0, 0, minAngle);

        StartCoroutine(AnimateNeedle());
    }

    private IEnumerator AnimateNeedle()
    {
        while (true)
        {

            float t = 0f;
            // 회색 구간 (빠르게)
            t = 0f;
            while (t < 0.25f)
            {
                t += Time.deltaTime;

                float angle = Mathf.Lerp(
                    minAngle,
                    midAngle,
                    t / 0.25f
                );

                transform.localRotation =
                    Quaternion.Euler(0, 0, angle);

                yield return null;
            }

            // 빨간 구간 (천천히)
            t = 0f;
            while (t < 0.75f)
            {
                t += Time.deltaTime;

                float angle = Mathf.Lerp(
                    midAngle,
                    maxAngle,
                    t / 0.75f
                );

                transform.localRotation =
                    Quaternion.Euler(0, 0, angle);

                yield return null;
            }

            yield return new WaitForSeconds(waitDuration);

            t = 0f;
            while (t < moveDownDuration)
            {
                t += Time.deltaTime;

                float angle = Mathf.Lerp(
                    maxAngle,
                    minAngle,
                    t / moveDownDuration
                );

                transform.localRotation =
                    Quaternion.Euler(0, 0, angle);

                yield return null;
            }

        }
    }
}