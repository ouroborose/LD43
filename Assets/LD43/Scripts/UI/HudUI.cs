using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : BaseUI {

    public Slider _progressIndicator;
    public float _progress = 0.0f;

    protected void Start()
    {
        Hide(true);

        EventManager.OnProgress.Register(OnProgress);
    }

    protected void OnDestroy()
    {
        EventManager.OnProgress.Unregister(OnProgress);
    }

    private void OnProgress(float progressPercent)
    {
        _progress = progressPercent;
    }

    public override void Show(bool instant = false)
    {
        base.Show(instant);
        OnProgress(EnvironmentManager.Instance._progress);
    }

    protected void Update()
    {
        _progressIndicator.value = Mathf.Lerp(_progressIndicator.value, _progress, Time.deltaTime);
    }
}
