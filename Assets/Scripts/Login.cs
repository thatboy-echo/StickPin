using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private string userName = "";
    private string pwd = "";
    private string message = "管理员账号登录";
    public Texture2D ButtomImage;

    private void OnGUI()
    {
        var y_offset = Screen.height / 2 -80;
        GUI.Label(new Rect(Screen.width / 2 - 30, y_offset, 200, 40), message);
        if (GUI.Button(new Rect(Screen.width / 2 - 30, y_offset+130, 100, 30), "开始游戏"))
        {
            Regex regex = new Regex("^[a-zA-Z]{5}$");
            Regex pwdregex = new Regex("^[a-zA-Z0-9_]{5,12}$");
            if (userName == "" || pwd == "")
            {
                message = "用户名密码不能为空";
                return;
            }
            else if (!regex.IsMatch(userName))
            {
                message = "用户名只能是5位字母";
                return;
            }
            else if (!pwdregex.IsMatch(pwd))
            {
                message = "密码5-12位字母数字下划线";
                return;
            }
            else if (userName != "admin" || pwd != "admin")
            {
                message = "用户名不存在或密码不匹配";
            }
           
            SceneManager.LoadScene("Main");
            
        }
        GUI.Label(new Rect(Screen.width / 2 - 50, y_offset+30, 50, 30), "用户名");
        GUI.Label(new Rect(Screen.width / 2 - 50, y_offset+70, 50, 30), "密码");
        userName = GUI.TextField(new Rect(Screen.width / 2, y_offset+30, 100, 25), userName, 10);
        pwd = GUI.PasswordField(new Rect(Screen.width / 2, y_offset+70, 100, 25), pwd, '*');
    }
}
