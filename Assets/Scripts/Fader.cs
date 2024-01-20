using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    /*
     * Incharge of fadeing in & out.
     */


    public Image img;
    public AnimationCurve curve;

    private void Start() {
        StartCoroutine(FadeIn());
    }
    
    public void Fading(GameObject CanvasOut, GameObject CanvasIn) {
        StartCoroutine(FadeOut(CanvasOut, CanvasIn));
    }

    IEnumerator FadeIn() {
        float t = 1f;

        while (t > 0) { 
            float a = curve.Evaluate(t);
            t -= Time.deltaTime;
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        
    }

    public IEnumerator FadeOut(GameObject CanvasOut, GameObject CanvasIn) {
        float t = 0f;
        CanvasOut.SetActive(false);

        while (t < 1f) {
            float a = curve.Evaluate(t);
            t += Time.deltaTime;
            img.color = new Color(0, 0, 0, a);
            yield return 0;
        }
        CanvasIn.SetActive(true);
        StartCoroutine(FadeIn());
    }



}
