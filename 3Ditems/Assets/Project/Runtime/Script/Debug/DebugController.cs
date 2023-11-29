using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Debuglist))]
public class DebugController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;
    string input;

    public static DebugCommand HELP;
    public static DebugCommand<string> LOADSCENE;
    public List<object> commandList;

    private void Awake()
    {
        CommandListInit();
    }

    private void CommandListInit()
    {
        HELP = new DebugCommand("Help", "show a list of command", "Help", () =>
        {
            showHelp = true;
        });

        LOADSCENE = new DebugCommand<string>("LoadScene", "load input value Scene", "LoadScene <String value>", (x) =>
        {
            Debug.Log(x);
            Debuglist.instance.loadSceneValue(x);
            Debug.Log("test2");
        });

        commandList = new List<object>
        {
            HELP,
            LOADSCENE,
        };

    }

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    public void OnReturn()
    {
        if (showConsole)
        {
            showHelp = false;
            HandleInput();
            input = "";
        }
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');

        for(int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (properties[0].Equals(commandBase.commandID))
            {
                if(commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if(commandList[i] as DebugCommand<int> != null)
                {
                    try
                    {
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                    catch 
                    {
        
                    }
                }

                else if (commandList[i] as DebugCommand<string> != null)
                {
                    try
                    {
                        (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                    }
                    catch
                    {
             
                    }
                }

                else if(commandList[i] as DebugCommand<int,Vector3> != null)
                {

                    try
                    {
                        (commandList[i] as DebugCommand<int,Vector3>).Invoke(int.Parse(properties[1]), string2Vector3(properties[2]));
                    }
                    catch
                    {
                      
                    }
                }
            }
        }
    }

    public Vector3 string2Vector3(string value)
    {
        string[] temp = value.Split(",");
        return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
    }

    Vector2 scroll;
    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0f;

        // 디버그 콘솔 백그라운드 설정
        GUI.Box(new Rect(0, y, Screen.width, 30), "");

        // 텍스트 박스
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.color = new Color32(255, 255, 255, 255);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        if (showHelp)
        {
            y += 30f;
            GUI.backgroundColor = new Color32(75, 75, 75, 175);
            GUI.color = new Color32(255, 255, 255, 255);
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            // 콘솔 명령어 갯수만큼 스크롤 뷰 크기 조정
            Rect viewport = new Rect(0,0,Screen.width-30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            // 스크롤 뷰 안에 디버그 콘솔 명령어 추가
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} : {command.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100 , 20);

                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();

            y+= 100;
        }
    }
}
