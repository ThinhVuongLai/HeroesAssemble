using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;
#if USE_SPINE
using Spine.Unity;
#endif

public static class GameHelper
{
    #region Component

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        if (!component.TryGetComponent<T>(out var result))
            result = component.gameObject.AddComponent<T>();

        return result;
    }

    #endregion

    #region Object

    public static string ToStringLower(this object obj) => obj?.ToString().ToLower();

    public static string ToStringLowerInvariant(this object obj) => obj?.ToString().ToLowerInvariant();

    public static string ToStringUpper(this object obj) => obj?.ToString().ToUpper();

    public static string ToStringUpperInvariant(this object obj) => obj?.ToString().ToUpperInvariant();

    /// <summary>
    /// Convert into string that has spaces before uppercase letters
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToStringTitle(this object obj)
    {
        string input = obj?.ToString();

        if (string.IsNullOrEmpty(input))
            return input;

        var result = new StringBuilder();
        bool startOfWord = true;  // Flag to indicate the start of a word for capitalization
        char lastChar = '\0';

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];

            // Insert a space before uppercase letters, but ignore the first character
            if (i > 0 && char.IsUpper(currentChar) && lastChar != ' ')
                result.Append(" ");

            // If it's the start of a word, capitalize the first letter
            if (startOfWord && char.IsLetter(currentChar))
            {
                result.Append(char.ToUpper(currentChar));  // Capitalize first letter
                startOfWord = false;  // Next characters in the word shouldn't be capitalized
            }
            else if (currentChar == ' ') // If a space, the next letter is the start of a new word
            {
                result.Append(currentChar);  // Add the space to the result
                startOfWord = true;  // Indicate the start of a new word
            }
            else
            {
                // If it's not the start of a word, add the character as-is
                result.Append(currentChar);
            }

            // Cache last character
            lastChar = currentChar;
        }

        return result.ToString();
    }

    #endregion

    #region Vecter2

    public static float GetAngleInRadian(this Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v2.y - v1.y, v2.x - v1.x);
    }

    public static Vector2 Rotate(this Vector2 v, float angle)
    {
        v = Quaternion.AngleAxis(angle, Vector2.up) * v;
        return v;
    }

    public static Vector2 GetDirect(float value)
    {
        float radian = Mathf.Deg2Rad * value;
        return new Vector2(Mathf.Cos(radian), MathF.Sin(radian));
    }

    #endregion

    #region Vecter3

    public static float GetAngleInRadian(this Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.y - v1.y, v2.x - v1.x);
    }

    public static float GetAngleInDegree(this Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v2.y - v1.y, v2.x - v1.x) * Mathf.Rad2Deg;
    }

    public static float GetAngleInDegree(this Vector3 v1)
    {
        return Mathf.Atan2(v1.y, v1.x) * Mathf.Rad2Deg;
    }

    public static float GetAngleInDegree(this Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }

    public static Vector3 Rotate(this Vector3 v, float angle)
    {
        v = Quaternion.AngleAxis(angle, Vector3.back) * v;
        return v;
    }

    public static Vector3 CalculateThirdVertex(Vector3 vertex1, Vector3 vertex2, float angleDegrees)
    {
        // Chuyển đổi góc từ độ sang radian
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        // Tính vector từ đỉnh 1 đến đỉnh 2
        Vector3 vertex1ToVertex2 = (vertex2 - vertex1) / 2f;

        // Tính vector mới bằng cách xoay vector từ đỉnh 1 đến đỉnh 2
        Vector3 rotatedVector = Quaternion.Euler(0, 0, angleDegrees) * vertex1ToVertex2;

        // Tính điểm thứ ba bằng cách cộng vector xoay với đỉnh 1
        Vector3 thirdVertex = vertex1 + rotatedVector;

        return thirdVertex;
    }

    public static Vector3 WorldToCanvasPosition(Transform worldTransform, Camera camera, Vector3 offset)
    {
        if (camera == null)
            return Vector3.zero;

        Vector3 worldPos = worldTransform != null ? worldTransform.position : Vector3.zero;
        // Chuyển từ WorldSpace sang ScreenSpace
        Vector3 screenPoint = camera.WorldToScreenPoint(worldPos);
        return screenPoint + offset;
    }

    public static Vector3 GetMidPoint(Vector3 point1, Vector3 point2)
    {
        return (point1 + point2) / 2f;
    }

    #endregion

    #region RectTransform

    public static void SetSizeByWidth(this RectTransform rect, float width, float aspect)
    {
        rect.sizeDelta = new Vector2(width, width * aspect);
    }

    public static void SetSizeByHeight(this RectTransform rect, float height, float aspect)
    {
        rect.sizeDelta = new Vector2(height / aspect, height);
    }

    public static void SetSize(this RectTransform rect, Vector2 size)
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }

    public static void SetAnchorPosX(this RectTransform rect, float posX)
    {
        Vector2 anchorPos = rect.anchoredPosition;
        anchorPos.x = posX;
        rect.anchoredPosition = anchorPos;
    }

    public static void SetAnchorPosY(this RectTransform rect, float posY)
    {
        Vector2 anchorPos = rect.anchoredPosition;
        anchorPos.y = posY;
        rect.anchoredPosition = anchorPos;
    }

    public static void SetSizeDeltaX(this RectTransform rect, float sizeX)
    {
        Vector2 sizeDelta = rect.sizeDelta;
        sizeDelta.x = sizeX;
        rect.sizeDelta = sizeDelta;
    }

    public static void SetSizeDeltaY(this RectTransform rect, float sizeY)
    {
        Vector2 sizeDelta = rect.sizeDelta;
        sizeDelta.y = sizeY;
        rect.sizeDelta = sizeDelta;
    }

    public static void SetSizeDeltaPreferred(this RectTransform rect, ILayoutElement layout, bool updateSizeX, bool updateSizeY)
    {
        if (updateSizeX)
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, layout.preferredWidth);

        if (updateSizeY)
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, layout.preferredHeight);
    }

    #endregion

    #region Sprite / SpriteRenderer

    public static Vector2 GetPivot(this Sprite s)
    {
        return new Vector2(s.pivot.x / s.rect.width, s.pivot.y / s.rect.height);
    }

    public static void SetAlpha(this SpriteRenderer sprite, float alpha)
    {
        var color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    #endregion

    #region Image

    public static void SetSizeByWidth(this Image img, float width)
    {
        if (img.sprite == null)
            return;
        var sprite = img.sprite;
        float aspect = sprite.bounds.size.y / sprite.bounds.size.x;
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(width, width * aspect);
    }

    public static void SetSizeByHeight(this Image img, float height)
    {
        if (img.sprite == null)
            return;
        var sprite = img.sprite;
        float aspect = sprite.bounds.size.y / sprite.bounds.size.x;
        img.GetComponent<RectTransform>().sizeDelta = new Vector2(height / aspect, height);
    }

    public static void SetAlpha(this Image img, float alpha)
    {
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }

    #endregion

    #region Text

    public static void SetAlpha(this Text txt, float alpha)
    {
        Color color = txt.color;
        color.a = alpha;
        txt.color = color;
    }

    public static void SetAlpha(this TMPro.TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

    #endregion

    #region Sprite Atlas

    public static Texture2D[] GetTextureOfAtlas(UnityEngine.U2D.SpriteAtlas spriteAtlas)
    {
#if UNITY_EDITOR
        var method = typeof(UnityEditor.U2D.SpriteAtlasExtensions).GetMethod("GetPreviewTextures", BindingFlags.Static | BindingFlags.NonPublic);
        object obj = method.Invoke(null, new object[] { spriteAtlas });
        Texture2D[] textures = obj as Texture2D[];
        return textures;
#endif
        return null;
    }

    #endregion

    #region AsssetDatabase

    public static void SetDirty(Object obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(obj);
#endif
    }

    public static void SaveAssetDatabase(Object obj, bool isRefresh = false)
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(obj);
        UnityEditor.AssetDatabase.SaveAssets();
        if (isRefresh)
            UnityEditor.AssetDatabase.Refresh();
#endif
    }

    public static List<T> GetAllAssetAtPath<T>(string filter, string path)
    {
#if UNITY_EDITOR
        string[] findAssets = UnityEditor.AssetDatabase.FindAssets(filter, new[] { path });
        List<T> os = new List<T>();
        foreach (var findAsset in findAssets)
        {
            os.Add((T)Convert.ChangeType(
                UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(findAsset),
                    typeof(T)), typeof(T)));
        }

        return os;
#endif
        return null;
    }

    public static List<Object> GetAllAssetsAtPath(string path)
    {
#if UNITY_EDITOR
        string[] paths = { path };
        var assets = UnityEditor.AssetDatabase.FindAssets(null, paths);
        var assetsObj = assets.Select(s =>
            UnityEditor.AssetDatabase.LoadMainAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(s))).ToList();
        return assetsObj;
#endif
        return null;
    }

    public static List<Sprite> GetAllSpriteAssetsAtPath(string path)
    {
#if UNITY_EDITOR
        string[] paths = { path };
        var assets = UnityEditor.AssetDatabase.FindAssets("t:sprite", paths);
        var assetsObj = assets.Select(s =>
            UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(UnityEditor.AssetDatabase.GUIDToAssetPath(s))).ToList();
        return assetsObj;
#endif
        return null;
    }

    public static List<Material> GetAllMaterialAssetsAtPath(string path)
    {
#if UNITY_EDITOR
        string[] paths = { path };
        var assets = UnityEditor.AssetDatabase.FindAssets("t:material", paths);
        var assetsObj = assets.Select(s =>
            UnityEditor.AssetDatabase.LoadAssetAtPath<Material>(UnityEditor.AssetDatabase.GUIDToAssetPath(s))).ToList();
        return assetsObj;
#endif
        return null;
    }

    public static void PingAssetAtPath(string path)
    {
#if UNITY_EDITOR
        // Load the asset at the specified path
        Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(path);

        // If the asset exists, select and ping it in the Project window
        if (asset != null)
        {
            UnityEditor.Selection.activeObject = asset;
            UnityEditor.EditorGUIUtility.PingObject(asset);
        }
        else
            Debug.LogError($"Asset is not found at path: {path}"!);
#endif
    }

    #endregion

    #region Scene

    public static void DeleteScene(string sceneName)
    {
#if UNITY_EDITOR
        var scene = GetAllScenes().Find(s => s == sceneName);
        if (scene != null)
        {
            var lstObjs = SceneManager.GetSceneByName(scene).GetRootGameObjects();
            foreach (var gameObject in lstObjs)
                GameObject.Destroy(gameObject);
        }
#endif
    }

    public static List<string> GetAllScenes()
    {
        List<string> scenes = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
            scenes.Add(SceneManager.GetSceneAt(i).name);
        return scenes;
    }

    #endregion

    #region Spine

#if USE_SPINE
    public static string GetSkinNameAtIndex(this SkeletonDataAsset spine, int index)
    {
        return spine.GetSkeletonData(true).Skins.Items[index].Name;
    }

    public static void SetSkin(this SkeletonGraphic spine, string skinName)
    {
        spine.Skeleton.SetSkin(skinName);
        spine.Skeleton.SetSlotsToSetupPose();
        spine.AnimationState.Apply(spine.Skeleton);
    }

    public static void SetSkin(this SkeletonAnimation spine, string skinName)
    {
        spine.Skeleton.SetSkin(skinName);
        spine.Skeleton.SetSlotsToSetupPose();
        spine.AnimationState.Apply(spine.Skeleton);
    }
#endif

    #endregion
}