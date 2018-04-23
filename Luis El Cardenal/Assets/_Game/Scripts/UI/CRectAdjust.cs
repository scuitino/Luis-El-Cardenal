using UnityEngine;
using UnityEditor;

public class CRectAdjust : EditorWindow
{
    [MenuItem("Tools/Adjust recttransform")]
    public static void OnWindow()
    {
        GetWindow<CRectAdjust>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Adjust"))
        {
            foreach (GameObject tObj in Selection.gameObjects)
            {
                if (tObj.transform.parent != null && tObj.transform is RectTransform)
                {
                    RectTransform tTransform = tObj.transform as RectTransform;
                    RectTransform tParent = tTransform.parent as RectTransform;

                    Vector2 tNormSize = new Vector2(tTransform.rect.width / tParent.rect.width,
                        tTransform.rect.height / tParent.rect.height);
                    float tPosX = tTransform.anchorMin.x * tParent.rect.width
                        + tTransform.anchoredPosition.x - tTransform.pivot.x * tTransform.rect.width;
                    float tPosY = tTransform.anchorMin.y * tParent.rect.height
                        + tTransform.anchoredPosition.y - tTransform.pivot.y * tTransform.rect.height;
                    Vector2 tNormPos = new Vector2(tPosX / tParent.rect.width, tPosY / tParent.rect.height);
                    tTransform.anchorMin = tNormPos;
                    tTransform.anchorMax = tNormSize + tNormPos;
                    tTransform.offsetMin = Vector2.zero;
                    tTransform.offsetMax = Vector2.zero;
                }
            }
        }
    }
}
