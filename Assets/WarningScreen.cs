using LeopotamGroup.Globals;

namespace Zlodey
{
    public class WarningScreen : Screen
    {
        public override void Hide()
        {
            Service<SceneData>.Get().MonitorUI.TimerScreen.WarningAnimator.SetTrigger("Warning");
            base.Hide();
        }
    }
}