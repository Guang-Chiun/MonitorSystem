using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQControl
{
    /// <summary>
    /// 事件參數建立
    /// </summary>
    public class WarningEventArgs : EventArgs
    {
        public string EQID { get; set; }
        public int Type { get; set; }
    }

    /// <summary>
    /// Running Event事件參數建立
    /// </summary>
    public class EQRunningEventArgs : EventArgs
    {
        public string EQID { get; set; }
        public bool Running { get; set; }
    }
    
    /// <summary>
    /// 擁有事件的類別
    /// 這好像可以改成不同機台背景處理不同
    /// </summary>
    public class BackGroundProcess
    {
        //不應該給外部類別填入，所以應該設定成private
        event EventHandler Warning;
        event EventHandler EQRunning;

        public BackGroundProcess(ref Dictionary<string, EQControl> EQControlDic)
        {
            //多波委託
            foreach (var pair in EQControlDic)
            {
                Warning += pair.Value.RefreshWarningStatus;
                EQRunning += pair.Value.RefreshRunningStatus;
            }
        }

        //要有一個函數供外部呼叫，這函數會呼叫OnWarning
        public void SetWarningInfo(string EQID, int Type)
        {
            OnWarning(EQID, Type);
        }
        //這樣寫法是每個物件都會呼叫到
        protected void OnWarning(string EQID, int Type)
        {
            if (Warning != null)
            {
                WarningEventArgs e = new WarningEventArgs();
                e.EQID = EQID;
                e.Type = Type;
                Warning.Invoke(this, e);
            }
        }

        //要有一個函數供外部呼叫，這函數會呼叫OnWarning
        public void SetRunningInfo(string EQID, bool Running)
        {
            OnEQRunning(EQID, Running);
        }
        protected void OnEQRunning(string EQID, bool Running)
        {
            if (EQRunning != null)
            {
                EQRunningEventArgs e = new EQRunningEventArgs();
                e.EQID = EQID;
                e.Running = Running;
                EQRunning.Invoke(this, e);
            }
        }
    }

    
}
