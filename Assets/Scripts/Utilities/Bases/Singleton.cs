using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// A type of Behavior that can only exist once in a scene.
/// </summary>
/// <typeparam name="T">The Behavior's Type</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static T _Instance;
	public static T instance => Get();
	public static T i => Get();
	public static T I => Get();

	public static T Get(bool createIfNone = false)
	{
		if (_Instance == null)
		{
			T findAttempt = FindFirstObjectByType<T>();
			if (findAttempt)
			{
				findAttempt.Awake();
				return findAttempt;
			}
			else
			{
				if (createIfNone)
				{
					Create(new(typeof(T).ToString()));
					return _Instance;
				}
				Debug.LogWarning("There's no Singleton of that type in this scene.");
				return null;
			}
		}
		else return _Instance;
	}

	public static T Get(ref T item, bool createIfNone = false)
	{
		if (item == null) item = Get(createIfNone);
		return item;
	}

	public static T Get() => Get(false);
	public static T Get(ref T item) => Get(ref item, false);

	public static T GetOrCreate() => Get(true);
	public static T GetOrCreate(ref T item) => Get(ref item, true);

	/// <summary>
	/// This is the Unity Function which runs some code necessary for Singleton Function. Use OnAwake() instead.
	/// </summary>
	public void Awake()
	{
		if (_Instance && _Instance != this)
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
			_Instance = (T)this;
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
		if (_Instance == this)
		{
			_Instance = null;
		}
		OnDestroyed();
	}
	protected virtual void OnDestroyed() { }

	/// <summary>
	/// Destroys the instance of this singleton, wherever it is.
	/// </summary>
	/// <param name="leaveGameObject"> Whether the Game Object that contains the Singleton is left behind.</param>
	public static void Destroy(bool leaveGameObject = false)
	{
		if (instance == null) return;
		if (!leaveGameObject)
		{
			MonoBehaviour.Destroy(instance.gameObject);
		}
		else
		{
			Object.Destroy(instance);
			instance.OnDestroy();
		}
	}

	/// <summary>
	/// Very Dangerous. Do not use if you don't know what you're doing.
	/// </summary>
	public void Reset()
	{
		GameObject obj = _Instance.gameObject;
		Destroy(true);
		Create(obj);
	}

	/// <summary>
	/// Creates an instance of this singleton and attaches it to the desired Game Object.
	/// </summary>
	/// <param name="object"> The object you are attaching the singleton to.</param>
	/// <param name="replace"> Whether or not this will forcibly replace an existing instance with the new one.</param>
	public static T Create(GameObject @object, bool replace = false)
	{
		if (!replace)
			if (instance != null) return null;
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

}
