using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Zlodey
{
    public class TimerScreen : Screen
    {
        public TextMeshProUGUI TimerText;
        public TextMeshProUGUI StableText;
        public Slider StableSlider;
        public GameObject[] Phases;
        public Animator WarningAnimator;
    }
}