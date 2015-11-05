﻿using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour 
{
	public float timeBeforeDestroy;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("Destroy");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	IEnumerator Destroy ()
	{
		yield return new WaitForSeconds (timeBeforeDestroy);
		Destroy(gameObject);
	}
}
