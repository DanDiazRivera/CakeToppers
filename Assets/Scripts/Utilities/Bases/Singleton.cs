#define HasAddressables

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// A type of Behavior that can only exist once in a scene.
/// </summary>
/// <typeparam name="T">The Behavior's Type</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T _instance;

	public static T instance => Get();
	public static T I => Get();

	public static T Get() => InitFind();


	protected static bool AttemptFind(out T result)
	{
		T findAttempt = FindFirstObjectByType<T>();
		if (findAttempt)
		{
			result = findAttempt;
			_instance = result;

			(_instance as T).OnAwake();
			return true;
		}
		else
		{
			result = null;
			return false;
		}
	}

	protected static T InitFind()
	{
		if (_instance != null) return _instance;
		if (AttemptFind(out T attempt))
		{
			InitFinal(attempt);
			return _instance;
		}

		Debug.LogWarning("No Singleton of type " + typeof(T) + " could be found.");
		return null;
	}

	protected static T InitCreate(bool dontDestroyOnLoad = false, string name = null)
	{
		if (_instance != null) return _instance;
		if (AttemptFind(out T attempt)) return attempt;

		GameObject GO = new(name??typeof(T).ToString());
		T result = GO.AddComponent<T>();

		InitFinal(result);
		if(dontDestroyOnLoad) DontDestroyOnLoad(result.gameObject);
		return result;
	}

#if HasAddressables
	protected static T InitInstantiate(string path)
	{
		if (_instance != null) return _instance;
		if (AttemptFind(out T attempt)) return attempt;

		GameObject result = Instantiate(UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion());
		
		InitFinal(result.GetComponent<T>());
		return _instance;
	}
	#endif

	public static bool TryGet(out T output)
	{
		output = Get();
		return output != null;
	}


	/// <summary>
	/// This is the Unity Function which runs some code necessary for Singleton Function. Use OnAwake() instead.
	/// </summary>
	public void Awake()
	{
		if (_instance && _instance != this)
		{
			Debug.LogError(
				"Something or someone is attempting to create a second " +
				typeof(T).ToString() +
				". Which is a Singleton. If you wish to reset the " +
				typeof(T).ToString() +
				", destroy the first before instantiating its replacement. The duplicate " +
				typeof(T).ToString() +
				" will now be Deleted."
				);

			Object.Destroy(this);
		}
		else
		{
			if (_instance == this) return;
			InitFinal(this as T);
		}
	}

	protected static void InitFinal(T input)
	{
		if (_instance || _instance == input) return;
        _instance = input;
        (_instance as T).OnAwake(); 
    }

	protected virtual void OnAwake() { }

	/// <summary>
	/// This is the Unity Function which runs some code necessary for Singleton Function. Use OnDestroyed() instead.
	/// </summary>
	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
		OnDestroyed();
	}
	protected virtual void OnDestroyed() { }

	/// <summary>
	/// Destroys the instance of this singleton, wherever it is.
	/// </summary>
	/// <param name="leaveGameObject"> Whether the Game Object that contains the Singleton is left behind.</param>
	public static void DestroyS(bool leaveGameObject = false)
	{
		if (_instance == null) return;
		if (!leaveGameObject)
		{
			Object.Destroy(_instance.gameObject);
		}
		else
		{
			Object.Destroy(_instance);
			_instance.OnDestroy();
		}
	}

	/// <summary>
	/// Very Dangerous. Do not use if you don't know what you're doing.
	/// </summary>
	public void Reset(bool ResetWholeGameObject)
	{
		if(ResetWholeGameObject)
		{
			GameObject obj = _instance.gameObject;
			DestroyS(true);
			obj.AddComponent<T>();
		}
		else
		{
			DestroyS(false);
			Get();
		}
		
	}


}