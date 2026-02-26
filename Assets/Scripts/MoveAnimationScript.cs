using System;
using UnityEngine;

public class MoveAnimationScript : MonoBehaviour
{
    private EntityScript target;
    private Rigidbody2D targetRb;
    private Vector2 startPos;
    private Vector2 endPos;
    private float duration = 0.3f;
    private float elapsed = 0f;
    private Action onComplete;

    [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private ParticleSystem dustTrail;
    [SerializeField] private Animator moveAnimator;

    public void StartMove(EntityScript entity, Vector2 destination, Action callback)
    {
        target = entity;
        targetRb = entity.GetComponent<Rigidbody2D>();
        startPos = targetRb != null ? targetRb.position : (Vector2)entity.transform.position;
        endPos = destination;
        onComplete = callback;
        elapsed = 0f;

        if (moveAnimator != null)
        {
            moveAnimator.SetTrigger("Move");
        }

        if (dustTrail != null)
        {
            dustTrail.Play();
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        elapsed += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        float curveValue = movementCurve.Evaluate(t);

        Vector2 newPos = Vector2.Lerp(startPos, endPos, curveValue);

        if (targetRb != null)
        {
            targetRb.MovePosition(newPos);
        }
        else
        {
            target.transform.position = newPos;
        }

        if (elapsed >= duration)
        {
            if (targetRb != null)
            {
                targetRb.MovePosition(endPos);
            }
            else
            {
                target.transform.position = endPos;
            }
            onComplete?.Invoke();
            Destroy(gameObject);
        }
    }
}