using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace check_automationID
{
    /// <summary>
    /// AutomationElement関係の処理
    /// </summary>
    public class UIAutomationLib
    {
        private readonly int MaxChrOfLine = 32;

        private string AutomationId;
        private string FrameworkId;
        private string ProcessId;
        private string LocalizedControlType;
        private string Name;
        private string HelpText;
        private string pX;
        private string pY;

        /// <summary>
        /// 長いテキストのカットと改行の消去
        /// </summary>
        private string AdjustLongText(string src)
        {
            //MaxChrOfLine以上の文字をカット、あと改行系の文字列もカット
            string dst = src.Substring(0, Math.Min(MaxChrOfLine, src.Length));
            dst = dst.Replace("\r", "");
            dst = dst.Replace("\n", "");
            return dst;
        }

        /// <summary>
        /// 現在の位置に対応するAutomationElement取得
        /// </summary>
        public void UpdateElement()
        {
            // 現在の位置を算出して、その位置のAutomationElementを取得
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
            System.Windows.Point point = new System.Windows.Point(p.X, p.Y);
            AutomationElement element = AutomationElement.FromPoint(point);

            // で、使いそうなパラメータを記録
            AutomationId = element.Current.AutomationId;
            FrameworkId = element.Current.FrameworkId;
            ProcessId = element.Current.ProcessId.ToString();
            LocalizedControlType = element.Current.LocalizedControlType;
            Name = element.Current.Name;
            HelpText = element.Current.HelpText;
            pX = p.X.ToString();
            pY = p.Y.ToString();
        }

        /// <summary>
        /// 画面表示用のテキストを作成
        /// </summary>
        public string GetResultText()
        {
            // TextBoxに出すイメージを生成して出力する。
            string text = "AutomationId:\t\t" + AutomationId + "\r\n";
            text += "FrameworkId:\t\t" + FrameworkId + "\r\n";
            text += "ProcessId:\t\t" + ProcessId + "\r\n";
            text += "LocalizedControlType:\t" + LocalizedControlType + "\r\n";
            text += "Name:\t\t\t" + AdjustLongText(Name) + "\r\n";
            text += "HelpText:\t\t" + AdjustLongText(HelpText) + "\r\n";
            text += "Pos:\t\t\t(X=" + pX + ", Y=" + pY +")";
            return text;
        }

        /// <summary>
        /// Ｃ＃っぽいテキストを作成。ファイル保存を意識したもの
        /// </summary>
        public string GetCSLikeText()
        {
            //C#風のコードとしてテキストを出します。
            string text = "string AutomationId = \"" + AutomationId + "\";\r\n";
            text += "string FrameworkId = \"" + FrameworkId + "\";\r\n";
            text += "int ProcessId = " + ProcessId + ";\r\n";
            text += "string LocalizedControlType = \"" + LocalizedControlType + "\";\r\n";
            text += "string Name = \"" + Name + "\";\r\n";
            text += "string HelpText = \"" + HelpText + "\";\r\n";
            text += "int pX = " + pX + ";\r\n";
            text += "int pY = " + pY + ";\r\n";
            return text;
        }
    }
}
