using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabsPreviewGenerator : EditorWindow
{
    [MenuItem("Tools/Generate Prefab Previews")]
    private static void ShowWindow()
    {
        GetWindow<PrefabsPreviewGenerator>("Prefab Previews");
    }

    private string prefabsFolderPath = "Assets/_Task/Prefabs";
    private Camera previewCamera;
    private string previewFolderPath = "Assets/_Task/Previews";
    private Vector3 previewCameraPosition = new Vector3(-5, 2, -5);
    private float previewCameraSize = 5;

    private void OnGUI()
    {
        GUILayout.Label("Prefab Preview Renderer", EditorStyles.boldLabel);

        prefabsFolderPath = EditorGUILayout.TextField("Prefabs Folder Path:", prefabsFolderPath);
        previewFolderPath = EditorGUILayout.TextField("Previews Folder Path:", previewFolderPath);
        previewCameraPosition = EditorGUILayout.Vector3Field("Preview Camera Position:", previewCameraPosition);
        previewCameraSize = EditorGUILayout.FloatField("Preview Camera Size:", previewCameraSize);

        if (GUILayout.Button("Render Previews")) RenderPreviews();
    }

    private void RenderPreviews()
    {
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabsFolderPath });

        if (prefabGuids.Length == 0) return;

        CreateScreenshotCamera();

        foreach (var guid in prefabGuids)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));

            if (prefab == null) continue;

            GameObject prefabInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            RenderScreenshot(prefabInstance);
            DestroyImmediate(prefabInstance);
        }

        DestroyScreenshotCamera();
    }

    private void CreateScreenshotCamera()
    {
        GameObject camObject = new GameObject("PrefabPreviewCamera");
        camObject.AddComponent<Camera>();
        previewCamera = camObject.GetComponent<Camera>();
        previewCamera.backgroundColor = Color.white;
        previewCamera.clearFlags = CameraClearFlags.SolidColor;
        previewCamera.orthographic = true;
        previewCamera.orthographicSize = previewCameraSize;
        previewCamera.nearClipPlane = 0.3f;
        previewCamera.farClipPlane = 1000f;
        previewCamera.transform.position = previewCameraPosition;
        previewCamera.transform.LookAt(Vector3.zero);
    }

    private void RenderScreenshot(GameObject prefabInstance)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(512, 512, 24);
        previewCamera.targetTexture = renderTexture;
        previewCamera.Render();
        SaveScreenshot(renderTexture, prefabInstance.name);
        RenderTexture.ReleaseTemporary(renderTexture);
    }

    private void SaveScreenshot(RenderTexture renderTexture, string prefabName)
    {
        RenderTexture.active = renderTexture;
        Texture2D preview = new Texture2D(renderTexture.width, renderTexture.height);
        preview.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        preview.Apply();
        RenderTexture.active = null;

        byte[] bytes = preview.EncodeToPNG();

        string fileName = $"{prefabName}_preview.png";
        Directory.CreateDirectory(previewFolderPath);
        File.WriteAllBytes(Path.Combine(previewFolderPath, fileName), bytes);

        Debug.Log($"Screenshot saved to: {previewFolderPath}");
    }

    private void DestroyScreenshotCamera()
    {
        if (previewCamera != null)
        {
            DestroyImmediate(previewCamera.gameObject);
        }
    }
}
