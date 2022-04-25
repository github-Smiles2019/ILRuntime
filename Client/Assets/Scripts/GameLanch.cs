using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//下面这行为了取消使用WWW的警告，Unity2018以后推荐使用UnityWebRequest，处于兼容性考虑Demo依然使用WWW
#pragma warning disable CS0618

public class GameLanch : UnitySingleton<GameLanch>
{
    public override void Awake() {
        base.Awake();

        // 初始化Unity项目的底层框架：资源管理，网络管理，声音管理, ILRuntime虚拟机
        this.gameObject.AddComponent<ResMgr>();
        this.gameObject.AddComponent<ILRuntimeMgr>().Init();
        // ...
        // end

    }

    IEnumerator CheckHotFix() {
        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改
#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix/HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix/HotFix_Project.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        www.Dispose();

        ILRuntimeMgr.Instance.LoadHotFixAssembly(dll, pdb);
        yield break;
    }

    IEnumerator CheckHotUpdate() {
        // 检查服务器上资源，然后更新下载来;
        // end 

        // 检查服务器上热更的.dll然后下载下来
        yield return this.CheckHotFix();
        // end

        ILRuntimeMgr.Instance.EnterGame();
        yield break;
    }


    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.CheckHotUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
