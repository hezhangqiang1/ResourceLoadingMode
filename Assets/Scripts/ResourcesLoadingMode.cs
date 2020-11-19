using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 资源加载的基类
/// </summary>
public class ResourcesLoadingMode : IResourcesLoadingMode
{
    public Image Img;
    public MonoBehaviour MB;

    /// <summary>
    /// 资源加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <param name="path"></param>
    /// <param name="IsAsync"></param>
    public virtual void ResourcesLoading<T>(T t, string path, bool IsAsync) where T : UnityEngine.Object
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 资源卸载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public virtual void ResourcesUnLoading<T>(T t) where T : UnityEngine.Object
    {
        Debug.Log("卸载资源");
    }
}