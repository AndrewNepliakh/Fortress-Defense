using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class User
{
    private string _userID;
    private int _score;
    private int _attempts;

    public string UserID
    {
        get { return _userID; }
        set { _userID = !String.IsNullOrWhiteSpace(value) && !String.IsNullOrEmpty(value) ? value : "Code name: 47"; }
    }
    public int Score
    {
        get { return _score; }
        set { if(value >= 0) _score = value;}
    }
    
    public int Attempts
    {
        get { return _attempts; }
        set { if(value >= 0) _attempts = value;}
    }

    public User()
    {
        _userID = Guid.NewGuid().ToString("N").Substring(0, 6);
        _score = 0;
        _attempts = 0;
    }

    public User(string id)
    {
        _userID = id;
        _score = 0;
        _attempts = 0;
    }
    
    public int AddScore(int value = 1)
    {
        _score += value;
        return _score;
    }

    public int SetUpAttempts(int value = 1)
    {
        _attempts -= value;
        return _attempts;
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
