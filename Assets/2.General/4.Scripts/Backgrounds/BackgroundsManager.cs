using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Summary: Controls backgrounds activation and UI of backgrounds screen. */
public class BackgroundsManager : MonoSingleton<BackgroundsManager>
{

    public List<BaseBackground> backgrounds = new List<BaseBackground>();

    [Header("Background UI.")]
    public Color selectedOutline;
    public Color deselectedOutline;
    public Color activatedTint;
    public Color deactivatedTint;
    public Color blockedtint;

	void Start () {
		
	}
	

	void Update () {
		
	}

    public void ActivateBackground<T>() where T : BaseBackground
    {
        T targetBackground = GetBackground<T>();
        targetBackground.gameObject.SetActive(true);
        DeactivateAllExсept<T>();
    }

    public void DeactivateAllExсept<T>()
    {
        foreach(BaseBackground background in backgrounds)
        {
            if(background.GetType() != typeof(T) && background.gameObject.activeInHierarchy)
            {
                background.gameObject.SetActive(false);
            }
        }
    }

    public T GetBackground<T>() where T : BaseBackground
    {
        T targetBackground = null;

        foreach(BaseBackground background in backgrounds)
        {
            if(background.GetType() == typeof(T))
            {
                targetBackground = (T)background;
                return targetBackground;
            }
        }

        return targetBackground;
    }
}
