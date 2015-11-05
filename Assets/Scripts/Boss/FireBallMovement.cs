using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FireBallMovement : MonoBehaviour 
{
	public float duration;

	public Vector3 target;

	public bool from = false;
	public bool relative = true;
	public Ease ease = Ease.OutCubic;

	private Transform tf;

	// Use this for initialization
	void Start () 
	{
		DOTween.Init();
		tf = GetComponent<Transform>();


		if(from == false)
		{
			tf.DOMove(target, duration, false).SetRelative(relative).SetEase(ease);
		}
		else
		{
			tf.DOMove(target, duration, false).SetEase(ease).From(relative);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
