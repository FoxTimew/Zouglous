using UnityEngine;

namespace MathExtension
{
	public static class Mathe
	{
		public static bool IsBetweenRange(float boundA, float boundB, float x)
		{
			if (x >= boundA && x <= boundB) return true;
			else return false;
		}

		public static Vector2 Vector3To2(Vector3 vectorToConvert)
		{
			return new Vector2(vectorToConvert.x, vectorToConvert.y);
		}

		public static Vector3 Vector2To3(Vector2 vectorToConvert)
		{
			return new Vector3(vectorToConvert.x, vectorToConvert.y, 0f);
		}
	}
}