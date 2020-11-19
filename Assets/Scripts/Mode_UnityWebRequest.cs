using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mode_UnityWebRequest : ResourcesLoadingMode
{
    public override void ResourcesLoading<T>(T t, string path, bool isAsync)
    {

        if (isAsync == false)
        {
            Debug.Log("unityWebRequest没有同步加载");
        }
    }

    public override void ResourcesUnLoading<T>(T t)
    {
        base.ResourcesUnLoading<T>(t);
    }

    public static IEnumerator UnityWebRequestLoad(string url, Action<UnityWebRequest> callback)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            callback.Invoke(webRequest);
            yield return null;
        }
    }
}
