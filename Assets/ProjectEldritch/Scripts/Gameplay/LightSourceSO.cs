using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Data/Light Source")]
public class LightSourceSO : ScriptableObject
{
	public float lightInnerRadius;
	public float lightOuterRadius;
	public float lightDuration;
}
