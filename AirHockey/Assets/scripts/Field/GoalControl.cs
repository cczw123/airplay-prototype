using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalControl : MonoBehaviour
{
    public GoalType goalType;
    private Coroutine goalCoroutine;

    public enum GoalType
    {
        LEFT = 0,
        RIGHT = 1
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hockey"))
        {
            if (goalCoroutine != null)
            {
                StopCoroutine(goalCoroutine);
            }

            goalCoroutine = StartCoroutine(Goal(collision.gameObject, collision.transform));
        }
    }

    private IEnumerator Goal(GameObject hockey, Transform current)
    {
        if (hockey.CompareTag("Hockey"))
        {
            hockey.SetActive(false);
            GameManager.Inst.scores[(int) goalType] += 1;
            AudioManager.Instance.PlayGoalAudio(current);
            yield return new WaitForSeconds(0.2f);
            AudioManager.Instance.PlayJubilianceAudio(current);
            yield return new WaitForSeconds(1f);
            // todo: add a sound for resetting the ball
            hockey.transform.position = Vector3.zero;
            hockey.SetActive(true);
        }
    }
}