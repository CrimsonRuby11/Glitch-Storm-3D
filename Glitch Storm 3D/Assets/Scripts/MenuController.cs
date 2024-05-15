using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField]private GameObject arcadeButton;
    [SerializeField]private GameObject quitButton;
    [SerializeField]private Animator transition;

    private int currentButton; // 0 = arcade, 1 = quit

    // Start is called before the first frame update
    void Start()
    {
        currentButton = 0;

        arcadeButton.transform.Find("NotSelected").gameObject.SetActive(false);
        arcadeButton.transform.Find("Selected").gameObject.SetActive(true);
        quitButton.transform.Find("NotSelected").gameObject.SetActive(true);
        quitButton.transform.Find("Selected").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateChoice();
    }

    void updateChoice() {
        if(Input.GetKey(KeyCode.DownArrow)) {
            if(currentButton != 1) {
                currentButton += 1;
            }

            arcadeButton.transform.Find("NotSelected").gameObject.SetActive(true);
            arcadeButton.transform.Find("Selected").gameObject.SetActive(false);

            quitButton.transform.Find("NotSelected").gameObject.SetActive(false);
            quitButton.transform.Find("Selected").gameObject.SetActive(true);
        }

        if(Input.GetKey(KeyCode.UpArrow)) {
            if(currentButton != 0) {
                currentButton -= 1;
            }

            arcadeButton.transform.Find("NotSelected").gameObject.SetActive(false);
            arcadeButton.transform.Find("Selected").gameObject.SetActive(true);
            quitButton.transform.Find("NotSelected").gameObject.SetActive(true);
            quitButton.transform.Find("Selected").gameObject.SetActive(false);
        }

        if(Input.GetKey(KeyCode.Space)) {
            if(currentButton == 0) {
                StartCoroutine(loadArcade());
            }

            if(currentButton == 1) Application.Quit();
        }
    }

    private IEnumerator loadArcade() {

        transition.SetTrigger("Out");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
