using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void ResetLeftArmUnlock()
    {
        PlayerPrefs.DeleteKey("LeftArmUnlocked");
        PlayerPrefs.DeleteKey("RightArmUnlocked");
        PlayerPrefs.DeleteKey("KickUnlocked");
        PlayerPrefs.DeleteKey("DefenceUnlocked");
        PlayerPrefs.DeleteKey("UnlockedLevel");
    }
}
