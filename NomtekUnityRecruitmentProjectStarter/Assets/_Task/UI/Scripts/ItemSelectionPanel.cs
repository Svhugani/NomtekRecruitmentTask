using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSelectionPanel : UIPanel
{
    [SerializeField] private ItemsAssetPack itemsAssetPack;
    [SerializeField] private RectTransform gridContent;
    [SerializeField] private TMP_InputField searchField;
    [SerializeField] private UIItemCard itemCardPrefab;

    private List<UIItemCard> _itemCards = new();

    public Action<GameObject> OnItemSelected;

    protected override void Awake()
    {
        base.Awake();

        searchField.onValueChanged.AddListener((x) => SearchByName(x));
    }

    public override void AnimateOut()
    {
        Body.DOAnchorPos(
            InitAnchor - new Vector2(1.1f * Body.sizeDelta.x, 0),
            FadeInDuration).SetEase(Ease.OutQuad);
    }

    public override void AnimateIn()
    {
        Body.DOAnchorPos(
            InitAnchor,
            FadeInDuration).SetEase(Ease.OutQuad);
    }

    public void PopulateGridContent()
    {
        Utils.DestroyChildren(gridContent.transform);
        _itemCards.Clear();

        for (int i = 0; i < itemsAssetPack.Items.Count; i++)
        {
            ItemAsset actorAsset = itemsAssetPack.Items[i];
            UIItemCard card = Instantiate(itemCardPrefab, gridContent.transform);
            card.SetTextContent(actorAsset.Text);
            card.SetImageContent(actorAsset.Preview);
            card.ItemID = i;
            card.OnItemCardSelected += TriggerItemSelection;
            _itemCards.Add(card);
        }
    }

    private void TriggerItemSelection(int itemIndex)
    {
        OnItemSelected?.Invoke(itemsAssetPack.Items[itemIndex].Content);
    }

    private void SearchByName(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            foreach (var card in _itemCards)
            {
                card.gameObject.SetActive(true);
            }

            return;
        }

        key = key.ToLower();

        foreach (var card in _itemCards)
        {
            bool contains = card.GetTextContent().ToLower().Contains(key);
            card.gameObject.SetActive(contains);
        }
    }
}
