using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

namespace check_automationID
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, IVoiceReciever
    {
        // ここに書かれた音声のいずれかで取りに行きます。
        private string[] CommandVoices = { "キャッチ" };

        // 使う画像ファイル。
        private string NormalImage = "puronama_normal.png";
        private string ResultImage = "pronama_result.png";
        private string TimerImage = "pronama_try.png";

        private PronamaRecognizer PronamaRecognizer;
        private UIAutomationLib UIAutoLib;
        private bool ValidElement;
        private DispatcherTimer Timer;
        private WriteTextFile WriteText;

        /// <summary>
        /// いつもの初期化系
        /// </summary>
        public MainWindow()
        {
            // まー初期化。
            InitializeComponent();

            ValidElement = false;
            Timer = null;
            UIAutoLib = new UIAutomationLib();
            WriteText = new WriteTextFile();

            // 音声認識初期化と反応する言葉の登録
            PronamaRecognizer = new PronamaRecognizer();
            PronamaRecognizer.Init(this);
            PronamaRecognizer.RegistWord(CommandVoices);
            PronamaRecognizer.Start();

            // 各種パスを絶対パスに
            NormalImage = MakeAbsolutePath(NormalImage);
            ResultImage = MakeAbsolutePath(ResultImage);
            TimerImage = MakeAbsolutePath(TimerImage);

            UpdateGui();
        }

        /// <summary>
        /// 相対パス指定が意外に効かない場合が多いので。ここで絶対パスを作ります
        /// </summary>
        private string MakeAbsolutePath(string src)
        {
            return System.IO.Directory.GetCurrentDirectory() + "\\Resources\\" + src;
        }

        /// <summary>
        /// プロ生ちゃんのイメージ差し替え
        /// </summary>
        private void UpdateImage(string uri)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            PronamaImage.Source = bitmap;
        }

        /// <summary>
        /// イメージとボタンを更新。有効なAutomationElementがない場合はテキストも表示
        /// </summary>
        private void UpdateGui()
        {
            if (ValidElement)
            {
                UpdateImage(ResultImage);
                SaveButton.Content = "保存（クリア）";
            }
            else
            {
                UpdateImage(NormalImage);
                CommentTextBox.Text = "音声かボタンで指示してね。";
                SaveButton.Content = "5秒後捕縛";
            }
        }

        /// <summary>
        /// AutomationElementの情報を更新。前面にしゃしゃり出る
        /// </summary>
        private void GetNowElement()
        {
            //AutomationElementの情報を更新（このコードを実行しているマウス位置で取ります
            if (Timer != null)
            {
                Timer.Stop();
            }
            UIAutoLib.UpdateElement();
            CommentTextBox.Text = UIAutoLib.GetResultText();
            ValidElement = true;
            UpdateGui();

            // 最小化してれば元に戻し、最前面に出てくるようにする
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            this.Topmost = true;
            this.Topmost = false;
        }

        /// <summary>
        /// AutomationElementの情報のC#っぽいテキスト保存（もしきはそのキャンセル）
        /// </summary>
        private void SaveElementCode()
        {
            //保存もしくはクリアの処理。まずはファイル保存ダイアログを出す
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "Elementのファイルを保存する";
            saveDlg.FileName = @"element.cs";
            DialogResult result = saveDlg.ShowDialog();

            //パスが指定された場合はC#風のコードを書き込む。
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                WriteText.Write(saveDlg.FileName, UIAutoLib.GetCSLikeText());
            }

            // で、有効なAutomationElementはもうないという扱いに
            ValidElement = false;
            UpdateGui();
        }

        /// <summary>
        /// タイマで発火すると呼ばれる
        /// </summary>
        private void updateElementTimer(object sender, EventArgs e)
        {
            //遅延してこれが呼ばれる。AutomationElementの情報を更新
            GetNowElement();
        }

        /// <summary>
        /// タイマ発動
        /// </summary>
        private void StartTimer()
        {
            // 5秒後に発火するタイマを起動
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += new EventHandler(updateElementTimer);
            Timer.Start();
        }

        /// <summary>
        /// ボタンクリック処理
        /// </summary>
        private void ClickTest(object sender, RoutedEventArgs e)
        {
            if (ValidElement == false)
            {
                UpdateImage(TimerImage);
                CommentTextBox.Text = "おーしキャッチしにいくぞ！";
                StartTimer();
            }
            else
            {
                // 有効なAutomationElementがある場合は保存系の処理に
                SaveElementCode();
            }
       }

        /// <summary>
        /// 音声入力でこれが呼ばれます
        /// </summary>
        public bool ReuqestByVoice(string voice)
        {
            // 登録された音声が聞こえた場合にここが呼ばれます。

            if (ValidElement)
            {
                return true; //保存（クリア）をまだしてなければ無視
            }

            // AutomationElementの情報を更新
            GetNowElement();

            return true;
        }
    }
}
