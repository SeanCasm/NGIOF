using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public GameObject gunInterface;
    public Image gunImage, bulletImage;
    public RectTransform gunBullets;
    public RectTransform loadBar;
    private void OnEnable() {
    }
    /// <summary>
    /// Reloads a gun ammo UI.
    /// </summary>
    public IEnumerator Reload(Gun.GunZeroAmmoEventArgs e)
    {
        var sizeDelta = loadBar.sizeDelta;
        //this float represent the loading bar increment per miliseconds.
        //126 is the load bar width on UI.
        //0.1f represents the miliseconds
        float sizeIncrement = 126 / e.reloadTime * 0.1f;
        while (e.currentLoadTime < e.reloadTime)
        {
            sizeDelta = new Vector2(sizeDelta.x += sizeIncrement, sizeDelta.y);
            loadBar.sizeDelta = sizeDelta;
            e.currentLoadTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        loadBar.sizeDelta = new Vector2(0, loadBar.sizeDelta.y);//Sets the width back to zero. 
    }
}
