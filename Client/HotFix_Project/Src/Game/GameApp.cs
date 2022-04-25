using UnityEngine;
// 注意，你可以调用Unity工程的类，使用Unity工程的类，但是你不能继承它;
public class GameApp {
    public static void Init() {
        // 正式进入游戏逻辑
        GameApp.EnterGameScene();
        // end
    }

    public static void EnterGameScene() {
        Debug.Log("EnterGameScene");

        // 释放地图,
        // end

        // 释放角色
        // end

        // 释放UI
        GameObject uiPrefab = ResMgr.Instance.GetAssetCache<GameObject>("GUI/UI_Prefabs/UIMainCity.prefab");
        GameObject uiView = GameObject.Instantiate(uiPrefab);
        uiView.transform.SetParent(GameObject.Find("Canvas").transform, false);
        // end
    }
}
