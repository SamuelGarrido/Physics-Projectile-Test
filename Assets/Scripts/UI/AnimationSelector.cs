using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationSelector : MonoBehaviour {


    #region Fields
    [Header("Animator")]
    [SerializeField]
    private Animator SoldierAnim;
    [SerializeField]
    private Transform NewPos;
    [Header("Buttons")]
    [SerializeField]
    private List <Button> Buttons;

    private IEnumerator c_helpPlayer;
    #endregion

    void Start() {
        c_helpPlayer = HelpPlayer();
    }

    #region Public Methods
    //Set Animation
    public void SelectAnim(Button btn) {
        SoldierAnim.Play(btn.name);
    }

    //Confirm animation and next escene
    public void AcceptSelection() {

        if (AnimatorIsPlaying()) {
            //Set new soldier position
            GameObject SoldierParent = gameObject.transform.parent.gameObject;

            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.position = NewPos.position;

            //Clear UI
            for (int i = 1; i < SoldierParent.transform.childCount; i++) {
                Destroy(SoldierParent.transform.GetChild(i).gameObject);
            }
            //Load next Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        } else {
            //Visually show player what to do
            StartCoroutine(c_helpPlayer);

        }

    }
    #endregion

    #region Private Methods
    //Check if any animation is playing
    private bool AnimatorIsPlaying() {
        return SoldierAnim.GetCurrentAnimatorStateInfo(0).length >
               SoldierAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private IEnumerator HelpPlayer() {
        for (int i = 0; i < 2; i++) {
            foreach (Button item in Buttons) {
                item.GetComponent<Image>().color = item.colors.pressedColor;
            }
            yield return new WaitForSeconds(.2f);
            foreach (Button item in Buttons) {
                item.GetComponent<Image>().color = item.colors.normalColor;
            }
            yield return new WaitForSeconds(.2f);
        }
    }
    #endregion
}
