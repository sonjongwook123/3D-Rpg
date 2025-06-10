using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text stageText;
    public Text valueText;

    void Start()
    {
        GameManager.Instance.StageChanged += ChangeStageText;
        GameManager.Instance.ValueChanged += ChangeValueText;
    }

    public void ChangeStageText()
    {
        stageText.text = "Stage" + GameManager.Instance.gameData.Stage.ToString() + "\n"
        + "최대 스테이지 : " + GameManager.Instance.gameData.MaxStage.ToString();
    }

    public void ChangeValueText()
    {
        valueText.text = "Gold" + ValueView.ToCurrencyString(GameManager.Instance.gameData.Gold);
    }
}