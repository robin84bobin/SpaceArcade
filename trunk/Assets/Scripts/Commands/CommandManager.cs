using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager
{
    private static int maxCmdCnt = 100;
	private static List<ICommand> _executed = new List<ICommand>();
    private static List<ICommand> _undone = new List<ICommand>();

    public static string Mode = "default";

    public static void Execute(ICommand c)
    {
        if (Mode == "undo") return;

        c.time = Time.time;
        c.Execute();
        AddCommand(_executed, c);
       // Debug.Log(string.Format("Command: {0} => Execute", c.ToString()));
    }

    private static void AddCommand(List<ICommand> list, ICommand c)
    {
        if (list.Count >= maxCmdCnt)
        {
            int deleteCnt = maxCmdCnt - list.Count;
            list.RemoveRange(0, deleteCnt+1);
        }
        list.Add(c);
    }

    public static void Redo()
    {
        if (_undone.Count <= 0) return;
        ICommand c = _undone.Last<ICommand>();
        _undone.Remove(c);
        c.Execute();
        AddCommand(_executed, c);
        //Debug.Log(string.Format("Command: {0} => Redo", c.ToString()));
    }

    public static void Undo()
	{
        if (_executed.Count <= 0)  return;
        ICommand c = _executed.Last<ICommand>();
        _executed.Remove(c);
        c.Undo();
        AddCommand(_undone, c);
        //Debug.Log(string.Format("Command: {0} => Undo", c.ToString()));
    }

}

public abstract class ICommand
{
   public abstract void Execute();
   public abstract void Undo();
   public float time { get; set; }
}