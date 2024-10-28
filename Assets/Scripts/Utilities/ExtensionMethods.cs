using System.Collections;
using UnityEngine;


public static class ExtensionMethods
{
	public static bool Toggle(this ref bool boolean) => boolean = !boolean;
	public static bool IsTrue(this bool boolean) => boolean == true;
	public static bool IsFalse(this bool boolean) => boolean == false;
	public static bool True(this ref bool boolean) => boolean = true;
	public static bool False(this ref bool boolean) => boolean = false;

	public static int Int(this bool boolean) => boolean ? 1 : 0;
	public static bool Bool(this int integral) => integral > 0;

	public static Color SetRed(this ref Color color, float set) => color =			new Color(set,              color.g,           color.b,          color.a);
	public static Color ChangeRed(this ref Color color, float change) => color =	new Color(color.r + change, color.g,           color.b,          color.a);
	public static Color SetBlue(this ref Color color, float set) => color =			new Color(color.r,          set,			   color.b,          color.a);
	public static Color ChangeBlue(this ref Color color, float change) => color =	new Color(color.r,          color.g + change,  color.b,          color.a);
	public static Color SetGreen(this ref Color color, float set) => color =		new Color(color.r,          color.g,		   set,              color.a);
	public static Color ChangeGreen(this ref Color color, float change) => color =	new Color(color.r,          color.g,           color.b + change, color.a);
	public static Color SetAlpha(this ref Color color, float set) => color =		new Color(color.r,          color.g,           color.b,          set);
	public static Color ChangeAlpha(this ref Color color, float change) => color =  new Color(color.r,          color.g,           color.b,          color.a + change);


}

public static class EasierMathExtensions
{
	public static float P(this float F) => Mathf.Pow(F, 2);
	public static float P(this float F, int power) => Mathf.Pow(F, power);
	public static float SQRT(this float F) => Mathf.Sqrt(F);
	public static float Sin(this float F) => Mathf.Sin(F);
	public static float Cos(this float F) => Mathf.Cos(F);
	public static float Tan(this float F) => Mathf.Tan(F);
	public static float ASin(this float F) => Mathf.Asin(F);
	public static float ACos(this float F) => Mathf.Acos(F);
	public static float ATan(this float F) => Mathf.Atan(F);

	public static float Clamp(this float value, float min, float max) => (value < min) ? min : (value > max) ? max : value;
	public static float Min(this float value, float min) => (value < min) ? min : value;
	public static float Max(this float value, float max) => (value > max) ? max : value;

	public static int Int(this float value) => (int)value;
	public static float Float(this int value) => (float)value;
	public static int Floor(this float value) => Mathf.FloorToInt(value);
	public static int Ceil(this float value) => Mathf.CeilToInt(value);

	public static int Sign(this float value) => (int)Mathf.Sign(value);
	public static float Abs(this float value) => Mathf.Abs(value);
	public static float Repeat(this float value, float length) => Mathf.Repeat(value, length);


	public static float Randomize(this float value, float min, float max) { value = UnityEngine.Random.Range(min, max); return value; }
	public static int Randomize(this int value, int min, int max) { value = UnityEngine.Random.Range(min, max); return value; }

	public static float RandomFrom(this float value, float min = 0) => UnityEngine.Random.Range(min, value);
	public static int RandomFrom(this int value, int min = 0) => UnityEngine.Random.Range(min, value);

	public static float RandomFrom(this Vector2 input) => UnityEngine.Random.Range(input.x, input.y);
	public static int RandomFrom(this Vector2Int input) => UnityEngine.Random.Range(input.x, input.y);

	public static float DistanceFrom(this float input, float second) => Mathf.Abs(input - second);

}

public static class MonoBehaviorHelpers
{
	public static void LateAwake(this MonoBehaviour m, Delegate result) => m.StartCoroutine(LateWakeENUM(result));
	
	static IEnumerator LateWakeENUM(Delegate result)
	{
		yield return new WaitForEndOfFrame();
		result();
	}

	public static bool Unloading(this MonoBehaviour m) => m.gameObject.scene.isLoaded;

	public static void SafeDestroyers(this MonoBehaviour m, Delegate SafeDestroy, Delegate UnloadDestroy)
	{
		if (!m.gameObject.scene.isLoaded) SafeDestroy();
		else UnloadDestroy();
	}

	public static void Set(this Transform T, Vector3? pos = null, Vector3? rot = null, Vector3? scale = null, Transform parent = null)
	{
		if(pos != null) T.localPosition = pos.Value;
		if(rot != null) T.localEulerAngles = rot.Value;
		if(scale != null) T.localPosition = scale.Value;
		if (parent != null) T.parent = parent;
	}

	public static GameObject NewGameObject(this Object O, string name = "NewGameObject", Vector3? pos = null, Quaternion? rot = null, Vector3? scale = null, Transform parent = null, params System.Type[] additions)
	{
		GameObject result = new(name, additions);

		if (parent != null) result.transform.parent = parent;
		if (pos != null) result.transform.localPosition = pos.Value;
		if (rot != null) result.transform.localRotation = rot.Value;
		if (scale != null) result.transform.localPosition = scale.Value;

		return result;
	}

	public static void Reset(this Transform transform, bool position = true, bool rotation = true, bool scale = true)
	{
		if(position) transform.localPosition = Vector3.zero;
		if (rotation) transform.localRotation = Quaternion.identity;
		if (scale) transform.localScale = Vector3.one;
	}

	public static T Random<T>(this T[] array) => array[UnityEngine.Random.Range(0, array.Length)];


}

public delegate void Delegate();