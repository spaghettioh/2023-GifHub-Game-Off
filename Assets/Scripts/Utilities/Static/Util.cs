using System;
using System.Collections.Generic;
using UnityEngine;

// INVESTIGATE can I have a static "DoSomethingOverTime" coroutine?
public static class Util
{
    private static int _currentId;

    public static bool CoinFlip => UnityEngine.Random.Range(0, 2) == 1;

    /// <summary>
    ///     Can check various types for null or their null equivalent
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNull<T>(this T obj) where T : class =>
        obj == null || obj.Equals(null);

    public static string GetNameTag(this string objectName)
    {
        objectName = objectName.Replace("(Clone)", "") + $"_{_currentId++}";
        return objectName;
    }

    public static int Random(int minInclusive, int maxExclusive) =>
        UnityEngine.Random.Range(minInclusive, maxExclusive);

    public static float Random(float minInclusive, float maxInclusive) =>
        UnityEngine.Random.Range(minInclusive, maxInclusive);

    public static float Deviation(this float input) => Random(-input, input);

    public static Vector3 RelativeRandom(
        this Vector3 value, float variableRange
    )
    {
        var x = value.x + Random(-variableRange, variableRange);
        var y = value.y + Random(-variableRange, variableRange);
        var z = value.z + Random(-variableRange, variableRange);
        return new(x, y, z);
    }

    public static List<T> Randomize<T>(this List<T> list)
    {
        var items = new List<T>(list);
        var newList = new List<T>();

        list.ForEach(l => l.Equals(default));

        foreach (var _ in list)
        {
            var randomItem = items.Random();
            newList.Add(randomItem);
            items.Remove(randomItem);
        }

        return newList;
    }

    public static T Random<T>(this List<T> list) =>
        list.Count > 0 ? list[Random(0, list.Count)] : default;

    public static float Normalize(
        this float input, float min, float max, float multiplier = 1f
    ) =>
        (input - min) / (max - min) * multiplier;

    public static float Remap(
        this float input, float inMin, float inMax, float outMin, float outMax,
        bool clamp
    )
    {
        var output = Mathf.Lerp(
            outMin, outMax, Mathf.InverseLerp(inMin, inMax, input)
        );
        return clamp ? Mathf.Clamp(output, outMin, outMax) : output;
    }

    public static float Remap(
        this float input, float inMin, float inMax, float outMin, float outMax
    ) =>
        input.Remap(inMin, inMax, outMin, outMax, false);

    public static float Lerp(this float current, float target) =>
        current.Lerp(target, Time.deltaTime);

    public static float
        Lerp(this float current, float target, float progress) =>
        Mathf.Lerp(current, target, progress);

    public static void ForEach<TKey, TValue>(
        this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> action
    )
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException(nameof(dictionary));
        }

        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        foreach (KeyValuePair<TKey, TValue> item in dictionary)
        {
            action(item.Key, item.Value);
        }
    }

    public static bool Contains(this LayerMask layer, int other) =>
        (layer.value & 1 << other) > 0;

    public static Vector3 Round(this Vector3 input)
    {
        input.x = Mathf.Round(input.x);
        input.y = Mathf.Round(input.y);
        input.z = Mathf.Round(input.z);
        return input;
    }
}
