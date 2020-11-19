using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mode_AssetDatabase : ResourcesLoadingMode
{
    public override void ResourcesLoading<T>(T t, string path, bool IsAsync)
    {
        if (IsAsync == false)
        {
            T load = AssetDatabase.LoadAssetAtPath<T>(path);
            t = load;
            if (t.GetType() == Img.sprite.GetType())
            {
                Img.sprite = t as Sprite;
                Resources.UnloadAsset(t);
            }
            else
            {
                Debug.Log(t.name);
            }
        }

        else
        {
            Debug.Log("assetdatabase没有异步加载");
        }
    }
}
