using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum ResourcesModeEnum
{
    ResourcesMode,
    WWWMode,
    AssetDatabaseMode,
    UnityWebMode,
    NULL,
}

public class ModeControl : MonoBehaviour
{


   
    public Image BG;
    public Text ResourcesModeText;
    public Text IsAnsynText;

    private bool Next;
    private bool IsAsync;                             //是否异步加载
    private ResourcesModeEnum ModeEnum;               //资源加载类型

    ResourcesLoadingMode mode = null;
    Mode_Resources resourcesMode = null;
    Mode_AssetDatabase assetDatabaseMode = null;
    Mode_WWW wwwMode = null;
    Mode_UnityWebRequest unityWebRequestMode = null;


    private int textureIndex = 1;                    // 图片路径索引


    void Awake()
    {
        resourcesMode = new Mode_Resources();
        assetDatabaseMode = new Mode_AssetDatabase();
        wwwMode = new Mode_WWW();
        unityWebRequestMode = new Mode_UnityWebRequest();
    }

    private void Start()
    {
        SetTipText(IsAnsynText, "当前为同步加载方式");
    }


    public void OnPreviousPageBtnClick()
    {
        Next = false;
        ResourceModeChoice(ModeEnum);
    }
  
    public void OnNextPageBtnClick()
    {
        Next = true;
        ResourceModeChoice(ModeEnum);
    }

    /// <summary>
    /// 资源加载方式公共方法
    /// </summary>
    /// <param name="modeEnum">资源类型</param>
    private void ResourceModeChoice(ResourcesModeEnum modeEnum)
    {
        switch (modeEnum)
        {
            case ResourcesModeEnum.ResourcesMode:
                mode = resourcesMode;
                ChangePage();
                mode.ResourcesLoading(BG.sprite, textureIndex.ToString(), IsAsync);
                break;
            case ResourcesModeEnum.WWWMode:
                mode = wwwMode;
                ChangePage();
                StartCoroutine(Mode_WWW.WWWLoad(string.Format(@"file://{0}/{1}.jpg", Application.streamingAssetsPath, textureIndex.ToString()), WWWcallback));
                break;
            case ResourcesModeEnum.AssetDatabaseMode:
                mode = assetDatabaseMode;
                ChangePage();
                mode.ResourcesLoading(BG.sprite, string.Format("Assets/Image/{0}.jpg", textureIndex.ToString()), IsAsync);
                break;
            case ResourcesModeEnum.UnityWebMode:
                mode = unityWebRequestMode;
                ChangePage();
                StartCoroutine(Mode_UnityWebRequest.UnityWebRequestLoad(string.Format(@"file://{0}/{1}.jpg", Application.streamingAssetsPath, textureIndex), unityWebRequestcallback));
                break;
            case ResourcesModeEnum.NULL:
                mode.ResourcesUnLoading(BG.sprite);
                BG.sprite = null;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 换页公共方法
    /// </summary>
    private void ChangePage()
    {
        if (Next == false) textureIndex--;
        if (textureIndex < 0)
        {
            textureIndex = 7;
        }
        if (Next == true) textureIndex++;
        if (textureIndex > 7)
        {
            textureIndex = 0;
        }
        mode.Img = BG;
        mode.MB = this.GetComponent<MonoBehaviour>();
    }
    public void ResourcesClick()
    {
        ModeEnum = ResourcesModeEnum.ResourcesMode;
        ResourceModeChoice(ModeEnum);
        SetTipText(ResourcesModeText, "ResourcesMode");
    }
    public void AssetBundleBtnClick()
    {
        //TODO 
    }
    public void WWWBtnClick()
    {
        if (IsAsync == false)
        {
            ModeEnum = ResourcesModeEnum.NULL;
            SetTipText(ResourcesModeText, "WWW没有同步加载");
        }
        else
        {
            ModeEnum = ResourcesModeEnum.WWWMode;
            ResourceModeChoice(ModeEnum);
            SetTipText(ResourcesModeText, "WWWMode");
        }

    }
    public void AssetDatabaseBtnClick()
    {
        if (IsAsync == false)
        {
            ModeEnum = ResourcesModeEnum.AssetDatabaseMode;
            ResourceModeChoice(ModeEnum);
            SetTipText(ResourcesModeText, "AssetDatabaseMode");
        }
        else
        {
            ModeEnum = ResourcesModeEnum.NULL;
            SetTipText(ResourcesModeText, "AssetDatabase没有异步加载");
        }

    }
    public void UnityWebRequestBtnClick()
    {
        if (IsAsync == false)
        {
            ModeEnum = ResourcesModeEnum.NULL;
            SetTipText(ResourcesModeText, "UnityWebRequest没有同步加载");
        }
        else
        {
            ModeEnum = ResourcesModeEnum.UnityWebMode;
            ResourceModeChoice(ModeEnum);
            SetTipText(ResourcesModeText, "UnityWebRequestMode");
        }

    }
    public void SynBtnClick()
    {
        IsAsync = false;
        SetTipText(IsAnsynText, "当前为同步加载方式");
    }
    public void AsynBtnClick()
    {
        IsAsync = true;
        SetTipText(IsAnsynText, "当前为异步加载方式");
    }

    /// <summary>
    /// WWW回调函数
    /// </summary>
    /// <param name="obj"></param>
    private void WWWcallback(object obj)
    {
        WWW www = obj as WWW;
        Texture2D tex = www.texture;
        BG.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        www.Dispose();
    }

    /// <summary>
    /// UnityWebRequest回调函数
    /// </summary>
    /// <param name="obj"></param>
    private void unityWebRequestcallback(object obj)
    {
        UnityWebRequest request = obj as UnityWebRequest;
        Texture2D tex = DownloadHandlerTexture.GetContent(request);
        BG.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        request.Dispose();
    }

    private void SetTipText(Text text, string str)
    {
        text.text = str;
    }
}