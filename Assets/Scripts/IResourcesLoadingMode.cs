/// <summary>
/// 资源加载的接口
/// </summary>
public interface IResourcesLoadingMode
{
    void ResourcesLoading<T>(T t, string path, bool IsAsync) where T : UnityEngine.Object;
    void ResourcesUnLoading<T>(T t) where T : UnityEngine.Object;

}