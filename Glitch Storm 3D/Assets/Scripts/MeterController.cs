using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    [SerializeField]private RectTransform dashMeter;
    [SerializeField]private RectTransform dashIcon;
    [SerializeField]private RectTransform pauseScreen;

    private float dashMeterSize;
    private float dashTimer;
    private int pauseSelector; // 0 for resume, 1 for restart, 2 for home
    private bool paused;
    private Vector3 dashMeterScale;

    // Start is called before the first frame update
    void Start()
    {
        setDashScale(0f);
        dashTimer = 0f;
        setDashIcon(false);

        pauseScreen.gameObject.SetActive(false);
    }

    void Update() {
        if(paused) pauseMechanics();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dashMeter.localScale.x >= 0) {
            setDashScale(dashTimer*(1f/2.5f));
            dashTimer = dashTimer - Time.deltaTime;
            if(dashTimer < 0) {
                dashTimer = 0;
            }
        }

        if(Input.GetKey(KeyCode.Escape)) {
            pauseClicked();
        }

        
    }

    public void startDashMeter() {
        dashTimer = 2.5f;
        setDashScale(1f);
    }

    private void setDashScale(float size) {
        dashMeter.localScale = new Vector3(size, 1, 1);
    }

    public void setDashIcon(bool isDashing) {
        if(isDashing) dashIcon.gameObject.GetComponent<Image>().color = Color.red;
        else dashIcon.gameObject.GetComponent<Image>().color = Color.white;
    }

    void pauseClicked() {
        pauseScreen.gameObject.SetActive(true);

        paused = true;
    }

    void pauseMechanics() {
        if(pauseSelector == 0) {
            pauseScreen.transform.Find("Selector").GetComponent<RectTransform>().localPosition = new Vector3(0, -180, 0);

            if(Input.GetKeyUp(KeyCode.Space)) {
                paused = false;
                pauseScreen.gameObject.SetActive(false);
            }

            if(Input.GetKeyUp(KeyCode.LeftArrow)) {
                pauseSelector = 1;
            }

            if(Input.GetKeyUp(KeyCode.RightArrow)) {
                pauseSelector = 2;
            }

        }

        if(pauseSelector == 1) {
            pauseScreen.transform.Find("Selector").GetComponent<RectTransform>().localPosition = new Vector3(-300, -180, 0);

            if(Input.GetKeyUp(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if(Input.GetKeyUp(KeyCode.RightArrow)) {
                pauseSelector = 0;
            }
        }

        if(pauseSelector == 2) {
            pauseScreen.transform.Find("Selector").GetComponent<RectTransform>().localPosition = new Vector3(300, -180, 0);

            if(Input.GetKeyUp(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }

            if(Input.GetKeyUp(KeyCode.LeftArrow)) {
                pauseSelector = 0;
            }
        }
    }

    public bool isPaused() {
        return paused;
    }
}
