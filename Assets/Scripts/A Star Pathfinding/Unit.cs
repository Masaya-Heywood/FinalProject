﻿using System.Collections;
using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;


//credit to Sebastian Lauge 
public class Unit : MonoBehaviour
{

	public Transform target;
	public float speed = 20;
	public int aggroThreshold = 10;
	private Rigidbody2D rb2D;
	public GameObject mySpawner;
	private float timer = 1;



	Vector2[] path;
	public Vector2 pathDirection;
	int targetIndex;
	public int dist;

	void Start()
	{
		StartCoroutine(RefreshPath());
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		target = GameObject.Find("Player").GetComponent<Transform>();

		if (SceneManager.GetActiveScene().name == "NewLevel" || SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour")
		{
			speed += 70;
		}
	}

	void Update()
	{
		dist = Mathf.RoundToInt(Vector2.Distance((Vector2)target.position, (Vector2)transform.position));

		timer -= Time.deltaTime;

		if (timer <= 0 && speed < 200)
		{
			speed += 2;
			timer = 1;
		}
	}

	IEnumerator RefreshPath()
	{
		Vector2 targetPositionOld = (Vector2)target.position + Vector2.up; // ensure != to target.position initially

		while (true)
		{
			if (targetPositionOld != (Vector2)target.position)
			{
				targetPositionOld = (Vector2)target.position;

				path = Pathfinding.RequestPath(transform.position, target.position);
				StopCoroutine("FollowPath");
				StartCoroutine("FollowPath");
			}

			yield return new WaitForSeconds(.25f);
		}
	}

	IEnumerator FollowPath()
	{
		if (path.Length > 0 && dist <= aggroThreshold && path.Length < 6)
		{
			targetIndex = 0;
			Vector2 currentWaypoint = path[0];

			while (true)
			{
				if ((Vector2)transform.position == currentWaypoint)
				{
					targetIndex++;
					if (targetIndex >= path.Length)
					{
						yield break;
					}
					currentWaypoint = path[targetIndex];
				}

				//transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

				pathDirection = (currentWaypoint - (Vector2)transform.position).normalized;
                if (rb2D != null)
                {
                    rb2D.velocity = pathDirection * (speed * Time.deltaTime);
                    transform.up = rb2D.velocity;
                }
				yield return null;

			}
		}
	}



    private void OnDestroy()
    {
        if(mySpawner != null)
		    mySpawner.GetComponent<EnemySpawner>().currentEnemies -= 1;
    }

    public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}