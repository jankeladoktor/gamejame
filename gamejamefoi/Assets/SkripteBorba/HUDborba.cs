using UnityEngine;
using UnityEngine.UI;

public class HUDborba : MonoBehaviour
{
    public Text nameText;
    public RectTransform hpFill;

    private float fullWidth;

    private void Awake()
    {
        if (hpFill != null)
            fullWidth = hpFill.rect.width;
    }

    public void SetHUD(string unitName, int level, int hp, int maxHp)
    {
        if (nameText != null) nameText.text = unitName;
        SetHP(hp, maxHp);
    }

    public void SetHP(int hp, int maxHp)
    {
        if (hpFill == null) return;

        if (fullWidth <= 0f) fullWidth = hpFill.rect.width;

        float pct = (maxHp <= 0) ? 0f : (float)hp / (float)maxHp;
        pct = Mathf.Clamp01(pct);

        hpFill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fullWidth * pct);
    }
}
