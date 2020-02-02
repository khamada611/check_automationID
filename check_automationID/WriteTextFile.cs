using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace check_automationID
{
    /// <summary>
    /// テキストファイル書いてくれる人
    /// </summary>
    public class WriteTextFile
    {
        /// <summary>
        /// テキストファイル書き込み
        /// </summary>
        public void Write(string path, string text)
        {
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(
                    @path,
                    false,
                    System.Text.Encoding.GetEncoding("utf-8"));
                writer.Write(text);
                writer.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("あれ、書けない。 " + exception.Message, "ファイルエラー");
            }
        }
    }
}
