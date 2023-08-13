using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class tryAgainButton : MonoBehaviour
{
    public void OnPointerClick()
    {
        ScreenUtils.SceneIndex = 0;
        ScreenUtils.CumulativeGoldCount = 0;
        SceneManager.LoadScene(0);
    }
}
