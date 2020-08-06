using UnityEngine;
/// <summary>
/// The main class which handle entire game processes  
/// </summary>
[CreateAssetMenu(fileName = "GameManager", menuName = "Managers/GameManager")]
public sealed class GameManager : BaseInjectable, IGlobal
{
    private UserManager _userManager; 
        
    private User _currentUser;

    public void Initialize()
    {
        _currentUser = new User();
    }

    public User GetCurrentUser() => _currentUser;
}