using UnityEngine;

using SpaceBeat.Sound;
using SpaceBeat.Objects3D;

public class MainTestPlayer : MonoBehaviour
{
  //
  // Config
  //

  public Canvas LoadingScreen;

  // analyzer params
  public int sampleSize = 1024;
  public int soundFeed = 100;
  public int beatSubbands = 30;
  public double beatSensitivity = 1.5;
  public int thresholdSize = 20;
  public float thresholdMultiplier = 1.5f;

  //
  // Members
  //

  public MusicAnalyzer analyzer;
  public new AudioSource audio;

  private int lastTime;
  private int verticalDirection;

  //
  // Functions
  //

  // Use this for initialization
  void Awake()
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
    LoadingScreen.enabled = true;

    while (!analyzer.Analyze())
      ; // make fancy rotation animation

    LoadingScreen.enabled = false;

    // debug
    var beats = analyzer.Beats;
    var detectedBeats = analyzer.m_soundParser.DetectedBeats;
    var thresholds = analyzer.Thresholds;

    audio.Play();
  }

  // Update is called once per frame
  void Update()
  {
    var time = audio.timeSamples / sampleSize;

    //var beats = analyzer.Beats;
    var thresholds = analyzer.Thresholds;
    var peaks = analyzer.Peaks;
    var speedFactor = analyzer.SpeedFactor;

    var detectedBeats = analyzer.m_soundParser.DetectedBeats;
    for (var i = lastTime; i <= time; i++) // todo count first and then change one time
      transform.Translate(0, verticalDirection * (float)(peaks[i]), (float)(peaks[i] * speedFactor * Time.deltaTime));

    verticalDirection = -verticalDirection;

    lastTime = time;
  }
}
