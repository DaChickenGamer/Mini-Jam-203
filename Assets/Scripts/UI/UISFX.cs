using System;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UISFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private bool _scaledButton = false;

    // Should be switched to get the sfx from the ui manager later
    [SerializeField] private EventReference buttonClickEvent;
    [SerializeField] private EventReference buttonHoverEvent;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        
        if(!buttonClickEvent.IsNull)
            _button.onClick.AddListener(() => RuntimeManager.PlayOneShot(buttonClickEvent));
        _button.onClick.AddListener(ScaleButton);
    }

    private void OnDisable()
    {
       DOTween.Kill(transform); 
    }

    private void ScaleButton()
    {
        if (_scaledButton) return;
        
        transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.25f, 1, 0.5f).OnComplete(() => _scaledButton = false);
        _scaledButton = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!buttonHoverEvent.IsNull)
            RuntimeManager.PlayOneShot(buttonHoverEvent);
        transform.DOScale(1.2f, .3f);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, .3f);
    }
}
