using UnityEngine;

using SpaceBeat.Sound;
using SpaceBeat.Objects3D;

public class TestPlayer : MonoBehaviour
{
  //
  // Config
  //

  public float speed = 10.0F;
  public float rotationSpeed = 100.0F;

  // analyzer params
  public int    sampleSize          = 1024;
  public int    soundFeed           = 100;
  public int    beatSubbands        = 30;
  public double beatSensitivity     = 1.5;
  public int    thresholdSize       = 20;
  public float  thresholdMultiplier = 1.5f;

  //
  // Members
  //

  private MusicAnalyzer analyzer;
  private new AudioSource audio;

  private int lastTime;
  private int verticalDirection;

  //
  // Functions
  //

  // Use this for initialization
  void Start()
  {
    verticalDirection = 1;
    lastTime = 0;

    audio = GetComponent<AudioSource>();
    audio.Stop();

    analyzer = new MusicAnalyzer(
        audio.clip,
        sampleSize,
        soundFeed,
        beatSubbands,
        beatSensitivity,
        thresholdSize,
        thresholdMultiplier
      );
    
    // todo make loading screen or progress bar and wrap calls once per update
    while (!analyzer.Analyze())
      ;

    // debug
    var beats = analyzer.Beats;
    var detectedBeats = analyzer.m_soundParser.DetectedBeats;
    var thresholds = analyzer.Thresholds;

    audio.Play();
  }

  // Update is called once per frame
  void Update()
  {
    audio.Pause();

    var time = audio.timeSamples / sampleSize;
    //var delta = Mathf.Max(0, time - lastTime);

    //var beats = analyzer.Beats;
    var thresholds = analyzer.Thresholds;
    var peaks = analyzer.Peaks;
    var speedFactor = analyzer.SpeedFactor;

    var detectedBeats = analyzer.m_soundParser.DetectedBeats;
    for (var i = lastTime; i <= time; i++) // todo count first and then change one time
      transform.Translate(0, verticalDirection * (float) (peaks[i] > 0 ? 1 :  0), (float) (detectedBeats[i] * speedFactor * Time.deltaTime));

    verticalDirection = -verticalDirection;
    /*
    for (int i = 0; i < beatSubbands; i++)
    {
      var b = beats[time, i];

      if (b != 0)
      {
        transform.Translate(0, 0, (float)b * 50 * Time.deltaTime);
        Debug.Log("!!BEAT");
        return;
      }

    }
    */

    lastTime = time;

    //Debug.Log(
    //  time + 
    //  " beats [" + beats[time, 0] + ", " + beats[time, 1] + ", " + beats[time, 2] + "\n" +
    //  " thresholds " + thresholds[time] + "\n" +
    //  " peaks " + peaks[time] + "\n"
    //  );

    audio.UnPause();
  }
}
