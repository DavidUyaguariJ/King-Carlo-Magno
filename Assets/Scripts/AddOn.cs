using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
	public bool activeAddOn()
	{
		Destroy(gameObject);
		return true;
	}
}
