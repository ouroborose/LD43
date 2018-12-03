using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VuLib;

public class UIManager : Singleton<UIManager> {

    public BaseUI _titleScreen;

    protected override void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(0);
            return;
        }
        base.Awake();
    }
}
