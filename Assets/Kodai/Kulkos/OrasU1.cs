using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrasU1 : MonoBehaviour
{
	void Start ()
	{
		InvokeRepeating ("Dideti", 2, 0.05f);
	}

	void Dideti ()
	{
		Vector3 Dydis = transform.localScale * 0.5f;
		transform.localScale = Dydis;
	}
}
