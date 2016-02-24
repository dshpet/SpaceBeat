using UnityEngine;
using System.Collections;

public class TestPlayer : MonoBehaviour {

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        var spectrum = new float[1024];
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        float influence = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (i < spectrum.Length / 3)
                influence += spectrum[i] * 0.5f;
            else
            if (i >= spectrum.Length / 3 && i <= spectrum.Length * 2 / 3)
                influence += spectrum[i] * 0.3f;
            else
            if (i > spectrum.Length * 2 / 3)
                influence += spectrum[i] * 0.2f;
        }

        /*
        var c1 = spectrum[2] + spectrum[3] + spectrum[4];
        var c3 = spectrum[11] + spectrum[12] + spectrum[13];
        var c4 = spectrum[22] + spectrum[23] + spectrum[24];
        var c5 = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49];

        Debug.Log("C1 = " + c1);
        var influence = (float) (c1 * 0.1 + c3 * 0.3 + c4 * 0.5 + c5 * 0.1);
        */

        transform.Translate(0, 0, influence * 50 * Time.deltaTime);
        //transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
