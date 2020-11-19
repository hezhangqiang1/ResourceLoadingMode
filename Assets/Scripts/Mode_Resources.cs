using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode_Resources : ResourcesLoadingMode
{
    public override void ResourcesLoading<T>(T t, string path, bool IsAsync)
    {

        if (IsAsync == false)
        {
            Debug.Log("====" + path + "====");
            T load = Resources.Load<T>(path);
            t = load;
           
            if (t.GetType() == Img.sprite.GetType())
            {
                Img.sprite = t as Sprite;
                Resources.UnloadAsset(t);
            }
        }
        else
        {
            T load = Resources.LoadAsync<T>(path).asset as T;
            t = load;
            if (t.GetType() == Img.sprite.GetType())
            {
                Img.sprite = t as Sprite;
                Resources.UnloadAsset(t);
            }
        }
    }
}
