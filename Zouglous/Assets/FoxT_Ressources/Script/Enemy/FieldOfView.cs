using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public Transform playableCharacter;

    [HideInInspector]
    public bool PlayerIsVisible;

	private void Awake()
	{
        playableCharacter = GameObject.Find("Playable_Character").transform;
	}

	private void OnEnable()
	{
        StartCoroutine(FindTargetWithDelay(0.2f));
	}

	private void OnDisable()
	{
        StopAllCoroutines();
	}

    private IEnumerator FindTargetWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindVisibleTargets();
        StartCoroutine(FindTargetWithDelay(0.2f));
    }

    private void FindVisibleTargets()
    {
        Collider2D targetsInViewRadius = Physics2D.OverlapCircle(transform.position, viewRadius, playerMask);

        if (targetsInViewRadius == null)
        {
            PlayerIsVisible = false;
            return;
        }

        Vector3 dirToTarget = (playableCharacter.position - transform.position).normalized;
        if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
        {
            float dstToTarget = Vector3.Distance(transform.position, playableCharacter.position);

            if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
            {
                PlayerIsVisible = true;
            }
            else PlayerIsVisible = false;
        }
        else PlayerIsVisible = false;
    }


    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
