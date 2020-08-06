using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Class to control users and user's data
/// </summary>
[CreateAssetMenu(fileName = "UserManager", menuName = "Managers/UserManager")]
public class UserManager : BaseInjectable, IGlobal
{
    private List<User> _users = new List<User>();

    public User CreateNewUser(string id = default)
    {
        User user;
        
        if(id == default) user = new User();
        else user = new User(id);
        
        _users.Add(user);

        return user;
    }
    
    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }
    public List<User> GetUserList()
    {
        return new List<User>(_users);
    }
    
}
