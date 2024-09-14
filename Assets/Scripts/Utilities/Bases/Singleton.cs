#define HasAddressables

using System;
using Unity.VisualScripting;
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

			_instance.Awake();
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
		if (AttemptFind(out T attempt)) return attempt;

		Debug.LogError("No Singleton of type" + nameof(T) + "could be found.");
		return null;
	}

	protected static T InitCreate(bool dontDestroyOnLoad = false, string name = null)
	{
		if (_instance != null) return _instance;
		if (AttemptFind(out T attempt)) return attempt;

		GameObject GO = new(name??typeof(T).ToString());
		T result = GO.AddComponent<T>();
		_instance = result;
		if(dontDestroyOnLoad) DontDestroyOnLoad(result.gameObject);
		
		_instance.Awake();
		return result;
	}

#if HasAddressables
	protected static T InitInstantiate(string path)
	{
		if (_instance != null) return _instance;
		if (AttemptFind(out T attempt)) return attempt;

		GameObject result = Instantiate(UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion());
		_instance = result.GetComponent<T>();
		
		_instance.Awake();
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
			_instance = (T)this;
			OnAwake();
			//Debug.Log(
			//    "The " +
			//    typeof(T).ToString() +
			//    " Singleton has been successfully Created/Reset."
			//    );
		}
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

	/*

	/// <summary>
	/// Creates an instance of this singleton and attaches it to the desired Game Object.
	/// </summary>
	/// <param name="object"> The object you are attaching the singleton to.</param>
	/// <param name="replace"> Whether or not this will forcibly replace an existing instance with the new one.</param>
	public static T Create(GameObject @object, bool replace = false)
	{
		if (!replace)
			if (_instance != null) return null;
			else Destroy(true);
		T result = @object.AddComponent<T>();
		result.Awake();
		return result;
	}

#if true
	/// <summary>
	/// Instantiates a prefab using the Addressables system. <br/>
	/// Should be called by a public static void with a hard-coded path.
	/// </summary>
	/// <param name="path">The input path. Fill this with a hard-coded path provided by a publicly available wrapper function.</param>
	protected static void InstantiateFromPath(string path, System.Action<T> response = null)
	{
		if (Get() != null) return;
		Addressables.LoadAssetAsync<GameObject>(path).Completed +=
		 (op) => {
			 GameObject prefab = Instantiate(op.Result);
			 T inst = prefab.GetComponent<T>();
			 inst.Awake();
			 response?.Invoke(inst);
		 };
	}
#endif
	 */

}