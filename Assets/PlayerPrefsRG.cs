using UnityEngine;

public abstract class PlayerPrefsRG
{
    public enum Prefs { AdvancedThrowing, GameType, RulerLength, Handedness };
    public static bool runInEditor = true;


    public static void AdvancedThrowing(bool active)
    {
        SetPref(Prefs.AdvancedThrowing, active ? 1 : 0);
    }

    public static bool AdvancedThrowing(ref bool throwing)
    {
        throwing = IntBool(GetPrefInt(Prefs.AdvancedThrowing));
        return HasKey(Prefs.AdvancedThrowing);
    }


    public static void GameType(int gameType)
    {
        SetPref(Prefs.GameType, gameType);
    }

    public static bool GameType(ref int gameType) {
        gameType = GetPrefInt(Prefs.GameType);
        return HasKey(Prefs.GameType);
    }

    public static void RulerLength(int rulerLength)
    {
        SetPref(Prefs.RulerLength, rulerLength);
    }

    public static bool RulerLength(ref int rulerLength)
    {
        rulerLength = GetPrefInt(Prefs.RulerLength);
        return HasKey(Prefs.RulerLength);
    }


    public static void Handedness(bool rightHanded)
    {
        SetPref(Prefs.Handedness, BoolInt(rightHanded));
    }

    public static bool Handedness(ref bool rightHanded)
    {
        rightHanded = IntBool(GetPrefInt(Prefs.Handedness));
        return HasKey(Prefs.Handedness);
    }
    


	static void SetPref(Prefs pref, object data)
    {
#if UNITY_EDITOR
        if (!runInEditor) return;
#endif
        string prefName = pref.ToString();
        if (data.GetType() == typeof(float))
        {
            PlayerPrefs.SetFloat(prefName, (float) data);
        }
        else if (data.GetType() == typeof(int))
        {
            PlayerPrefs.SetInt(prefName, (int) data);
        }
        else if (data.GetType() == typeof(string))
        {
            PlayerPrefs.SetString(prefName, (string) data);
        }
        else
        {
            Debug.Log("PlayerPrefsDG-SetPref() received type not supported");
        }
    }

    static bool HasKey(Prefs pref)
    {
        return PlayerPrefs.HasKey(pref.ToString());
    }

    static float GetPrefFloat(Prefs pref)
    {
        return PlayerPrefs.GetFloat(pref.ToString(), -123f);
    }

    static int GetPrefInt(Prefs pref)
    {
        return PlayerPrefs.GetInt(pref.ToString(), -123);
    }
    
    static string GetPrefString(Prefs pref)
    {
        return PlayerPrefs.GetString(pref.ToString(), "-123");
    }

    static bool IntBool(int integer) { return integer == 1; }
    static int BoolInt(bool boolean) { return boolean ? 1 : 0; }

}
