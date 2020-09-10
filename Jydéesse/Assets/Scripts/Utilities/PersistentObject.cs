﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
	private void Awake()
	{
		if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
}
