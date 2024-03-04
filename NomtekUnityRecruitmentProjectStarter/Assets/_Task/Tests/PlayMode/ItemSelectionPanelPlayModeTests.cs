using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class ItemSelectionPanelPlayModeTests: MonoBehaviour, IMonoBehaviourTest
{
    private GameObject _testObject;
    private ItemSelectionPanel _itemSelectionPanel;
    private ItemsAssetPack _itemsAssetPack;

    public bool IsTestFinished { get; private set; }

    [SetUp]
    public void SetUp()
    {
        string prefabPath = "Assets/_Task/UI/Components/ItemSelectionPanel.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        _testObject = Instantiate(prefab);
        _itemSelectionPanel = _testObject.GetComponent<ItemSelectionPanel>();

        _itemsAssetPack = ScriptableObject.CreateInstance<ItemsAssetPack>();

        _itemsAssetPack.Items = new List<ItemAsset>
        {
            ScriptableObject.CreateInstance<ItemAsset>(),
            ScriptableObject.CreateInstance<ItemAsset>(),
            ScriptableObject.CreateInstance<ItemAsset>()
        };

        for (int i = 0; i < _itemsAssetPack.Items.Count; i++)
        {
            _itemsAssetPack.Items[i].Text = $"Item_{i}";
            _itemsAssetPack.Items[i].Preview = new Texture2D(10, 10);
            _itemsAssetPack.Items[i].Content = new GameObject();
        }

        _itemSelectionPanel.SetItemsPack(_itemsAssetPack);
    }

    [UnityTest]
    public IEnumerator ItemSelectionPanel_PopulateGridContent_CreatesItemCards()
    {
        _itemSelectionPanel.PopulateGridContent();

        yield return null;

        List<UIItemCard> items = _itemSelectionPanel.GetItemCards();
        Assert.AreEqual(_itemsAssetPack.Items.Count, items.Count);

        for (int i = 0;  i < items.Count; i++)
        {
            var card = items[i];
            Assert.AreEqual(card.GetTextContent(), _itemsAssetPack.Items[i].Text);
            Assert.AreEqual(card.GetTexture(), _itemsAssetPack.Items[i].Preview);
        }
    }

    [UnityTest]
    public IEnumerator ItemSelectionPanel_AfterAnimateIn_IsOn()
    {
        _itemSelectionPanel.AnimateOut();
        yield return new WaitForSeconds(_itemSelectionPanel.FadeOutDuration);

        _itemSelectionPanel.AnimateIn();
        yield return new WaitForSeconds(_itemSelectionPanel.FadeInDuration);

        Assert.IsTrue(_itemSelectionPanel.IsOn);
    }

    [UnityTest]
    public IEnumerator ItemSelectionPanel_AfterAnimateIn_IsNotOn()
    {
        _itemSelectionPanel.AnimateIn();
        yield return new WaitForSeconds(_itemSelectionPanel.FadeInDuration);

        _itemSelectionPanel.AnimateOut();
        yield return new WaitForSeconds(_itemSelectionPanel.FadeOutDuration);

        Assert.IsTrue(!_itemSelectionPanel.IsOn);
    }

    [UnityTest]
    public IEnumerator ItemSelectionPanel_SearchByName_HidesUnmatchedItems()
    {
        _itemSelectionPanel.PopulateGridContent();
        _itemSelectionPanel.SearchByName(_itemsAssetPack.Items[0].Text);

        yield return null;

        foreach (var card in _itemSelectionPanel.GetItemCards())
        {
            if (card.GetTextContent() == _itemsAssetPack.Items[0].Text)
            {
                Assert.IsTrue(card.gameObject.activeSelf);
            }
            else
            {
                Assert.IsFalse(card.gameObject.activeSelf);
            }
        }
    }


    [TearDown]
    public void TearDown()
    {
        DestroyImmediate(_testObject);
        DestroyImmediate(_itemsAssetPack);

        IsTestFinished = true;
    }
}
