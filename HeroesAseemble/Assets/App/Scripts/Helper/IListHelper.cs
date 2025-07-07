using System;
using System.Collections.Generic;

public static class IListHelper
{
    /// <summary>
    /// Check list is empty or null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this ICollection<T> list)
    {
        return list == null || list.Count <= 0;
    }

    /// <summary>
    /// Check index out of range of list or not. Return TRUE if list is null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static bool IsIndexOutOfList<T>(this ICollection<T> list, int index)
    {
        return list == null || index < 0 || index >= list.Count;
    }

    /// <summary>
    /// Check minimum length of list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool HasMinLength<T>(this ICollection<T> list, int length)
    {
        if (length < 0)
            return false;

        return list != null && list.Count >= length;
    }

    /// <summary>
    /// Check maximum length of list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool HasMaxLength<T>(this ICollection<T> list, int length)
    {
        if (length < 0)
            return false;

        return list != null && list.Count <= length;
    }

    /// <summary>
    /// Cast value of element in list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="initParams"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static T GetValueAtIndex<T>(this IList<object> initParams, int index = 0)
    {
        if (initParams != null && initParams.Count > index)
        {
            // Safe cast: if the type does not match, return default(T)
            return initParams[index] is T value ? value : default(T);
        }
        return default(T);
    }

    /// <summary>
    /// Clone a new list. If list is null, return empty list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <returns></returns>
    public static List<T> Clone<T>(this List<T> lst) where T : ICloneable
    {
        var result = new List<T>();

        if (lst.IsNullOrEmpty())
            return result;

        foreach (var item in lst)
            result.Add((T)item.Clone());

        return result;
    }

    #region Random

    /// <summary>
    /// Get a random item in list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    /// <exception cref="System.IndexOutOfRangeException"></exception>
    public static T RandomItem<T>(this IList<T> list)
    {
#if UNITY_EDITOR
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
#endif
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Get a random item in list with state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    /// <exception cref="System.IndexOutOfRangeException"></exception>
    public static T RandomItem<T>(this IList<T> list, System.Random state)
    {
#if UNITY_EDITOR
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
#endif
        return list[state.Next(0, list.Count)];
    }

    /// <summary>
    /// Remove a random item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    /// <exception cref="System.IndexOutOfRangeException"></exception>
    public static T RemoveRandom<T>(this IList<T> list)
    {
#if UNITY_EDITOR
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
#endif
        int index = UnityEngine.Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }

    /// <summary>
    /// Remove a random item in list with state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    /// <exception cref="System.IndexOutOfRangeException"></exception>
    public static T RemoveRandom<T>(this IList<T> list, System.Random state)
    {
#if UNITY_EDITOR
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
#endif
        int index = state.Next(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }

    /// <summary>
    /// Use percent list to random item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="percentList"></param>
    /// <param name="defaultIndex"></param>
    /// <returns></returns>
    public static T RandomItemWithPercent<T>(this IList<T> list, IList<float> percentList, int defaultIndex = 0)
    {
        if (list.IsNullOrEmpty())
            return default(T);

        T result = list.IsIndexOutOfList(defaultIndex) ? default(T) : list[defaultIndex];
        float percentValue = UnityEngine.Random.Range(0f, 100f);
        float curPercent = 0f;

        for (int index = 0; index < list.Count; index++)
        {
            curPercent += percentList.IsIndexOutOfList(index) ? 0f : percentList[index];
            if (percentValue <= curPercent)
            {
                result = list[index];
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Use percent list itself to random index
    /// </summary>
    /// <param name="percentList"></param>
    /// <param name="defaultIndex"></param>
    /// <returns></returns>
    public static int RandomIndexPercentList(this IList<float> percentList, int defaultIndex = 0)
    {
        if (percentList.IsNullOrEmpty())
            return defaultIndex;

        float percentValue = UnityEngine.Random.Range(0f, 100f);
        float curPercent = 0f;

        for (int i = 0; i < percentList.Count; i++)
        {
            int index = i;
            curPercent += percentList[index];
            if (percentValue < curPercent)
                return index;
        }

        return percentList.Count - 1;
    }

    #endregion

    #region Shuffle

    /// <summary>
    /// Shuffle list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ts"></param>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    /// <summary>
    /// Shuffle list with state
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ts"></param>
    /// <param name="state"></param>
    public static void Shuffle<T>(this IList<T> ts, System.Random state)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = state.Next(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
    #endregion

}
