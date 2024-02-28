using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]private Animator transition;
    [SerializeField]private TMP_Text stageText;
    [SerializeField]private Canvas canvasObj;

    private bool isLoadLevel;
    public bool isStageFailed {get; set;}

    void Awake() {
        isLoadLevel = true;
        stageText.text = "";
        canvasObj.enabled = true;
    }
    
    void Update() {
        if(isLoadLevel) {
            loadLevel();
        }

        if(isStageFailed) {
            stageFailed();
            isStageFailed = false;
        }
    }

    private IEnumerator loadLevel() {
        // animate
        
        transition.SetTrigger("Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // change to buildIndex + 1
        stageText.text = "NEXT STAGE";

        transition.SetTrigger("In");

        yield return new WaitForSeconds(1f);

        isLoadLevel = false;
        
    }

    private void stageFailed() {
        // animate crossfade
        transition.SetTrigger("Out");

        // change text to stage failed
        stageText.text = "STAGE FAILED";
        
    }

    public void loadNextLevel() {
        StartCoroutine(loadLevel());
    }
}
