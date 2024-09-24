using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// A code system similar to a Singleton but is not an Beheavior-Object in any scene. <br/>
/// The Staticon will be initialized the first time it is referenced by any script, and exist until the program ends.
/// </summary>
/// <typeparam name="T">The Behavior's Type</typeparam>
public abstract class Staticon<T> where T : Staticon<T>
{
	public static bool initialized { get; private set; }
	private static T _instance;
	public static T Get()
	{ if (!initialized) Initialize(); return _instance; }
	public static T I => Get();

	public static void Initialize()
	{
		if (initialized) return;
		_instance = Activator.CreateInstance<T>();
		_instance.Awake();
		initialized = true;
	}

	public abstract void Awake();
}