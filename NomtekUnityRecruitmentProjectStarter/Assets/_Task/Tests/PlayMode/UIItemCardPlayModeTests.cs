using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class UIItemCardPlayModeTests: MonoBehaviour, IMonoBehaviourTest
{
    private GameObject _testObject;
    private UIItemCard _uiItemCard;

    public bool IsTestFinished { get; private set; }

    [SetUp]
    public void SetUp()
    {
        string prefabPath = "Assets/_Task/UI/Components/UIItemCard.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        _testObject = Instantiate(prefab);
        _uiItemCard = _testObject.GetComponent<UIItemCard>();
    }

    [UnityTest]
    public IEnumerator UIItemCard_SetImageContent_SetsTexture()
    {
        Texture2D testTexture = new Texture2D(10, 10);
        _uiItemCard.SetImageContent(testTexture);
        yield return null;

        Assert.AreEqual(testTexture, _uiItemCard.GetTexture());
    }

    [UnityTest]
    public IEnumerator UIItemCard_SetTextContent_SetsText()
    {
        string testText = "Test Text";
        _uiItemCard.SetTextContent(testText);
        yield return null;

        Assert.AreEqual(testText, _uiItemCard.GetTextContent());
    }

    [UnityTest]
    public IEnumerator UIItemCard_ButtonClick_InvokesProperID()
    {
        int invokedItemID = -1;
        _uiItemCard.OnItemCardSelected += (itemID) => invokedItemID = itemID;
        _uiItemCard.OnItemCardSelected?.Invoke(10);

        yield return null;

        Assert.AreEqual(10, invokedItemID);
    }

    [TearDown]
    public void TearDown()
    {
        DestroyImmediate(_testObject);
        IsTestFinished = true;
    }
}

