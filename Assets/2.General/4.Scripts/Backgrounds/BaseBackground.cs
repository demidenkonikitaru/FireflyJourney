using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseBackground : MonoBehaviour {

    [Header("Related UI.")]
    public Button activateBtn;

    public Image tint;

    public Image lockIcon;
    public int cost;
    public Image diamondIcon;
    public TextMeshProUGUI costText;

    public Outline cellOutline;

    [Header("Gameplay elements.")]
    public BaseBackground backgroundToActive;
    public Sprite platform;
    public Sprite hero;
    public List<Jewel> jewels = new List<Jewel>();
    

    void Start () {
		
	}

	void Update () {
		
	}

    public void Activate()
    {
        this.gameObject.SetActive(true);
        
        
    }

    public void ActivateUI()
    {
        cellOutline.effectColor = BackgroundsManager.Instance.selectedOutline;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        
        
    }

    public void DeactivateUI()
    {
        cellOutline.effectColor = BackgroundsManager.Instance.deselectedOutline;
    }
}
