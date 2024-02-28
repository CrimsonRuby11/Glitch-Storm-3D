using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    [SerializeField]private RectTransform dashMeter;
    [SerializeField]private RectTransform dashIcon;

    private float dashMeterSize;
    private float dashTimer;
    private Vector3 dashMeterScale;

    // Start is called before the first frame update
    void Start()
    {
        setDashScale(0f);
        dashTimer = 0f;
        setDashIcon(false);
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
}
