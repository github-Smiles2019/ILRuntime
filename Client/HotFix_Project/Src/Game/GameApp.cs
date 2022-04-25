using UnityEngine;
// ע�⣬����Ե���Unity���̵��࣬ʹ��Unity���̵��࣬�����㲻�ܼ̳���;
public class GameApp {
    public static void Init() {
        // ��ʽ������Ϸ�߼�
        GameApp.EnterGameScene();
        // end
    }

    public static void EnterGameScene() {
        Debug.Log("EnterGameScene");

        // �ͷŵ�ͼ,
        // end

        // �ͷŽ�ɫ
        // end

        // �ͷ�UI
        GameObject uiPrefab = ResMgr.Instance.GetAssetCache<GameObject>("GUI/UI_Prefabs/UIMainCity.prefab");
        GameObject uiView = GameObject.Instantiate(uiPrefab);
        uiView.transform.SetParent(GameObject.Find("Canvas").transform, false);
        // end
    }
}
