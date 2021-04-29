using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathAI : MonoBehaviour
{
	public Transform targetPosition;
	private Transform patrolWaypoint;
	private Transform playerPosition;

	private Seeker seeker;
	private FoxT_Pumpkin_Controller enemyController;

	public Path path;

	[SerializeField]
	private float pathRefresh;

	public float nextWaypointDistance = 3;

	private int currentWaypoint = 0;

	public bool reachedEndOfPath, backToPatrol;

	private void OnEnable()
	{
		StartPathFinding();
		StartCoroutine(PathUpdate());
	}

	private void OnDisable()
	{
		seeker.pathCallback -= OnPathComplete;
		StopAllCoroutines();
	}

	private void Awake()
	{
		enemyController = GetComponent<FoxT_Pumpkin_Controller>();
		seeker = GetComponent<Seeker>();
		patrolWaypoint = GetComponent<FoxT_Pumpkin_Controller>().wayPointsPatrol[0];
		playerPosition = targetPosition;
	}

	public void StartPathFinding()
	{
		seeker.pathCallback += OnPathComplete;

		seeker.StartPath(transform.position, targetPosition.position);
	}

	public void OnPathComplete(Path p)
	{
		Debug.Log("Path calculated. Error: " + p.error);

		if (!p.error)
		{
			path = p;

			currentWaypoint = 0;
		}
	}

	public void BackToPatrol()
	{
		seeker.pathCallback -= OnPathComplete;
		backToPatrol = true;
		targetPosition = patrolWaypoint;
		seeker.pathCallback += OnPathComplete;
		seeker.StartPath(transform.position, targetPosition.position);
	}

	private void Update()
	{
		if (path == null)
		{
			return;
		}

		//Vérifier le joueur est proche de l'ennemi

		reachedEndOfPath = false;

		float distanceToWaypoint;
		while (true)
		{
			distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

			if (distanceToWaypoint < nextWaypointDistance)
			{
				if (currentWaypoint + 1 < path.vectorPath.Count)
				{
					currentWaypoint++;
				}
				else
				{
					reachedEndOfPath = true;
					break;
				}
			}
			else break;
		}
		Vector2 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

		enemyController.Move(dir);

		if (reachedEndOfPath && backToPatrol)
		{
			ChangeTarget();
		}
	}

	public void ChangeTarget()
	{
		if (!backToPatrol) return;
		backToPatrol = false;
		enemyController.PCdetected = false;
		targetPosition = playerPosition;
		this.GetComponent<EnemyPathAI>().enabled = false;
	}

	IEnumerator PathUpdate()
	{
		while (true)
		{
			yield return new WaitForSeconds(pathRefresh);
			seeker.StartPath(transform.position, targetPosition.position);
		}
	}

}
