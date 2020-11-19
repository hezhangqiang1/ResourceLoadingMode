using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode_WWW : ResourcesLoadingMode
{
    public override void ResourcesLoading<T>(T t, string path, bool isAsync)
    {
        if (isAsync == true)
        {
            // MB.StartCoroutine(WWWLoad());
        }
        else
        {
            Debug.Log("----WWW没有同步加载----");
        }
    }

    public override void ResourcesUnLoading<T>(T t)
    {
        base.ResourcesUnLoading<T>(t);

    }

    public static IEnumerator WWWLoad(string url, Action<WWW> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        callback.Invoke(www);
        yield return null;
    }
}
