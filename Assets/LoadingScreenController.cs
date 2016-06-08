using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
  public Image Gear;
  public Text LoaadingText;

  private int m_UpdatesCount;

  // Use this for initialization
  void Start()
  {
    m_UpdatesCount = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (m_UpdatesCount % 3 == 0)
      Gear.rectTransform.Rotate(0, 3, 0);

    if (m_UpdatesCount % 10 == 0)
      LoaadingText.text += ".";

    if (m_UpdatesCount == 100)
      SceneManager.LoadSceneAsync("GameLevel");

    m_UpdatesCount++;
  }
}
