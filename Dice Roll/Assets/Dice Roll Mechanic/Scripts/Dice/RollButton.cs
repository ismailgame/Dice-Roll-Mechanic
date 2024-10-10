using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Button values")]
    [SerializeField] private Transform button; //Button transform for effect
    private WaitForSeconds clickTimer = new WaitForSeconds(0.05f);//Click spam prevention timer

    [Header("Game Event")]
    [SerializeField] private GameEvent OnRollButtonClicked;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Grow up when arrow on button
        button.transform.localScale = new Vector3(1.1f, 1.1f, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Call effect coroutine
        StartCoroutine(ClickEffect());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Make it normal scale when arrow outside
        button.localScale = Vector3.one;
    }

    private IEnumerator ClickEffect()
    {
        //Play sound effect
        AudioPlayer.Play(transform.position, SoundManager.i.rollButtonSound);
        //Change scale for effect
        button.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        yield return clickTimer;
        button.transform.localScale = Vector3.one; ;
        //Raise OnRoll event
        OnRollButtonClicked.Raise(null, null);
        gameObject.SetActive(false);
    }

    public void OnDiceRollOver()
    {
        gameObject.SetActive(true);
    }
}
