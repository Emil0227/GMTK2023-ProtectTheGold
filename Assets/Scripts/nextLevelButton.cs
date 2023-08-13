using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class nextLevelButton : MonoBehaviour
{
    public void OnPointerClick()
    {
        ScreenUtils.SceneIndex += 1;
        SceneManager.LoadScene(ScreenUtils.SceneIndex);
    }
}
