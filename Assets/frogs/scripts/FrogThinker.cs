//using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using TouchScript.Gestures;
using UnityEngine;

public class FrogThinker : MonoBehaviour
{
	public Animator animator;

	[SerializeField] private GameObject WaterSplash;
	[SerializeField] private float minStaySec = 1.0f;
	[SerializeField] private float maxStaySec = 3.0f;
	[SerializeField] private GameObject hitObject;
	[SerializeField] private int visits = 3;

	public float speed = 1f; // Speed of movement

	//private Transform jumpTarget;
	public float jumpHeight = 2.0f;
	public float jumpDistance = 5.0f;
	public float jumpDuration = 2.0f;
	public float LastDiveLevel = -0.8f;

	private TapGesture tapGesture;

	private int visitedCound = 0;

	private FrogRestPoint restPoint = null;
	//private string lastRestPointName = string.Empty;
	private int lastRestPointName = -1;

	private bool isResting = false;

	private ChainRunner chainRunner;
	private RestObjectManager restObjectManager = null;
	private FrogsManager frogsManager = null;
	private randomListPlayer _randomListPlayer = null;

	// Start is called before the first frame update
	void Start()
	{
		tapGesture = GetComponent<TapGesture>();

		if(tapGesture != null)
		{
			tapGesture.Tapped += TapGesture_Tapped; ;
		}

		chainRunner = new ChainRunner();

		//temporary before writting good spawner
		restObjectManager = FindAnyObjectByType<RestObjectManager>();
		frogsManager = FindAnyObjectByType<FrogsManager>();
		_randomListPlayer = FindAnyObjectByType<randomListPlayer>();

		StartCoroutine(init());
	}

	bool isHit = false;

	private void TapGesture_Tapped(object sender, System.EventArgs e)
	{
		if (!isResting) return;

		if (isHit) return;

		isHit = true;

		chainRunner.Stop();// можно добавить шаг-прерывание; машина прекращает выполнять текущую операцию и немедленно переходит к этой внеочереди
		restObjectManager.leaveCity();
		frogsManager.AddCaughtScore();
		//frogsManager.leaveCity();
		if (restPoint != null) restPoint.isBusy = false;
		Instantiate(hitObject, transform.position, Quaternion.identity);
		Destroy(gameObject, 0.5f);
	}

	IEnumerator init()
	{
		restPoint = restObjectManager.enterCity();

		while (restPoint == null)
		{
			yield return null;

			restPoint = restObjectManager.enterCity();
		}

		if (restPoint != null)
		{
			restPoint.isBusy = true;
			//lastRestPointName = restPoint.name;
			lastRestPointName = restPoint.GetInstanceID();
		}

		chainRunner.AddStep(new LinearMoveXZStep()
		{
			speed = speed,
			OnStartMotion = (x) => animator.SetTrigger("swim"),
			targetPoint = CalculatePointForJumpToObject(transform.position, restPoint.transform.position, jumpDistance),
			movableObject = transform,
		});

		chainRunner.AddStep(new JumpFromWaterStep("jump-between")
		{
			OnStartMotion = (x) => { animator.SetTrigger("jump"); x.targetPoint = restPoint.transform.position; },// or in OnReinit, ReStart
			movableObject = transform,
			OnExitWater = (x) => { Instantiate(WaterSplash, x, Quaternion.identity); _randomListPlayer.Play(); },
			//jumpObject.targetPoint = jumpTarget.position;
			jumpDuration = jumpDuration,
			jumpHeight = jumpHeight,
		});
		
		// resting
		chainRunner.AddStep(new StayStep()
		{
			OnEnter = (x) => { animator.SetTrigger("idle"); x.stayForSeconds = Random.Range(minStaySec, maxStaySec); isResting = true; },
		});

		// write away from rest place
		chainRunner.AddStep(new OperationStep()
		{
			Do = () => { visitedCound++; restPoint.isBusy = false; restPoint = null; isResting = false; }
		});

		// request for the next place
		chainRunner.AddStep(new WaitForContitionStep()
		{
			While = () => restPoint == null,
			Do = () => {
				restPoint = restObjectManager.provideRestObjectExcept(lastRestPointName);
				if (restPoint != null)
				{
					restPoint.isBusy = true;
					//lastRestPointName = restPoint.name;
					lastRestPointName = restPoint.GetInstanceID();
				}
			}
		});

		// repeater
		chainRunner.AddStep(new RepeatStep()
		{
			RepeatWhile = () => visitedCound <= visits,
			FromStep = "jump-between"
		});

		// leave an object and destroy
		chainRunner.AddStep(new JumpParabolaStep()
		{
			OnStartMotion = (x) => { animator.SetTrigger("jump"); x.targetPoint = CalculateEndPointWhenLeaving(LastDiveLevel, 3.0f); },
			OnEndMotion = (x) => { animator.SetTrigger("disappear"); Destroy(gameObject, 1.0f); restPoint.isBusy = false; restObjectManager.leaveCity(); frogsManager.leaveCity(); },
			movableObject = transform,
			jumpDuration = jumpDuration,
			jumpHeight = jumpHeight
		});
	}

	public void SetRestManager(RestObjectManager manager)
	{
		this.restObjectManager = manager;
	}

	bool go = false;
	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//	go = true;

		go = frogsManager.letsGo();

		if (go)
		{
			chainRunner.Update();
		}
	}

	Vector3 CalculatePointForJumpToObject(Vector3 A, Vector3 B, float distance)
	{
		// Direction vector from A to B
		Vector3 direction = B - A;

		// Normalize the direction vector to get the unit vector
		Vector3 unitDirection = direction.normalized;

		// Calculate point C
		Vector3 pointC = B - unitDirection * distance;

		return pointC;
	}

	private Vector3 CalculateEndPointWhenLeaving(float targetYLevel, float jdist)
	{
		// Calculate the end point based on the frog's forward direction

		var res = transform.position + transform.forward * jdist;

		res.y = targetYLevel;

		return res;
	}
}