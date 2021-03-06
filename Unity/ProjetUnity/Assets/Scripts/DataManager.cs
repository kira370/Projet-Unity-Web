﻿using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;

public class DataManager
{
    private static DataManager instance = null;
    private JObject user;
    private string idUser = null;
    private string formation = "";
    //private string dirFile = "/storage/emulated/0/data.txt";
    private string dirFile = "data.txt";
    private DataManager() {}

    public static DataManager GetInstance()
    {
        if(instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

    public void VerifFichier()
    {
        if (!File.Exists(dirFile))
        {
            string[] ini = { "", "", "0.5", "0.5" };
            File.WriteAllLines(dirFile, ini);
        }
    }

    public void EcrirePseudoID(string pseudo, string id)
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[0] = pseudo;
        ancien[1] = idUser;
        File.WriteAllLines(dirFile, ancien);
    }

    public void EcrireSonMusic(string son, string music)
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[2] = son;
        ancien[3] = music;
        File.WriteAllLines(dirFile, ancien);
    }

    public bool Connecter()
    {
        string[] ancien = File.ReadAllLines(dirFile);
        if (ancien[0] != "" && ancien[1] != "")
        {
            if (idUser == null)
            {
                idUser = ancien[1];
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    public float GetVolume()
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[2] = ancien[2].Replace(",", ".");
        return float.Parse(ancien[2], CultureInfo.InvariantCulture.NumberFormat);

    }

    public float GetSetSfx()
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[3] = ancien[3].Replace(",", ".");
        return float.Parse(ancien[3], CultureInfo.InvariantCulture.NumberFormat);

    }

    public void SetVolume(float volume)
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[2] = volume.ToString();
        File.WriteAllLines(dirFile, ancien);
    }

    public void GetSfx(float sfx)
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[3] = sfx.ToString();
        File.WriteAllLines(dirFile, ancien);
    }

    public void Deconnecter()
    {
        string[] ancien = File.ReadAllLines(dirFile);
        ancien[0] = "";
        ancien[1] = "";
        File.WriteAllLines(dirFile, ancien);
    }

    public void SetUser(JObject u)
    {
        user = u;
        idUser = u["_id"].ToString();
        EcrirePseudoID(u["pseudo"].ToString(), idUser);
    }

    public JObject GetUser()
    {
        return user;
    }


    public string GetIdUser()
    {
        return idUser;
    }
    
    public void SetFormation(string form)
    {
        formation = form;
    }

    public string GetFormation()
    {
        return formation;
    }
}