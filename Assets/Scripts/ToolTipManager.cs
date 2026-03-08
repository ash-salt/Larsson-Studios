using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject main;
    public GameObject other1;
    public GameObject other2;
    public void SetToolTipVisibility()
    {
        bool isActive = main.activeSelf;

        main.SetActive(!isActive);
        other1.SetActive(false);
        other2.SetActive(false);
    }
    void Update()
    {
        if (main.activeSelf && Input.GetMouseButtonDown(0))
        {
            HideTooltip();
        }
    }

    void HideTooltip()
    {
        main.SetActive(false);
    }
}
