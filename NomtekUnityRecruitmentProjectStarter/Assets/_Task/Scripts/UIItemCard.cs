using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCard : MonoBehaviour
{
    [SerializeField] private RawImage imageContent;
    [SerializeField] private TextMeshProUGUI textContent;
    [SerializeField] private Button button;

    public int ItemID { get; set; } = -1;

    public Action<int> OnItemCardSelected;

    private void Awake()
    {
        button.onClick.AddListener(() => OnItemCardSelected?.Invoke(ItemID));
    }

    public void SetImageContent(Texture2D texture)
    {
        imageContent.texture = texture;
    }

    public void SetTextContent(string text)
    {
        textContent.text = text;
    }

    public string GetTextContent()
    {
        return textContent.text;
    }

    public Texture2D GetTexture()
    {
        return (Texture2D)imageContent.texture;
    }


}
