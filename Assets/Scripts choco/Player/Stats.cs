using UnityEngine;

public class Stats : MonoBehaviour
{
      private int XP;
      

    public void AddXP(int value)
    {
        XP += value;
        Debug.Log("XP: " + XP);
    }

}
