using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SpeechLib; // 音声入力で使う。参照でCOMからMS Speechライブラリを。大量のWarning出ます。

namespace check_automationID
{
    /// <summary>
    /// 登録された音声のコールバック処理
    /// </summary>
    public interface IVoiceReciever
    {
        bool ReuqestByVoice(string voice);
    }

    /// <summary>
    /// 音声処理系
    /// </summary>
    class PronamaRecognizer
    {
        IVoiceReciever reciever = null;
        private SpeechLib.SpInProcRecoContext RecognizerRule = null;
        private SpeechLib.ISpeechRecoGrammar RecognizerGrammarRule = null;
        private SpeechLib.ISpeechGrammarRule RecognizerGrammarRuleGrammarRule = null;
        private bool busy;

        /// <summary>
        /// SpeechLib.SpObjectToke（マイク系）初期化
        /// </summary>
        private SpeechLib.SpObjectToken GetAudioInput()
        {
            new SpeechLib.SpObjectTokenCategory();
            SpeechLib.SpObjectTokenCategory objAudioTokenCategory = null;
            SpeechLib.SpObjectToken objAudioToken = null;
            try
            {
                objAudioTokenCategory = new SpeechLib.SpObjectTokenCategory();
                objAudioTokenCategory.SetId(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\AudioInput", false);
                objAudioToken = new SpeechLib.SpObjectToken();
                objAudioToken.SetId(objAudioTokenCategory.Default, @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Speech\AudioInput", false);
            }
            catch (Exception exception)
            {
                MessageBox.Show("マイクつながってなくない？" + exception.ToString() , "マイク初期化エラー");
            }
            return objAudioToken;
        }

        /// <summary>
        /// 音声認識システムの初期化。各種コールバック処理もここ
        /// </summary>
        public void Init(IVoiceReciever callerReciever)
        {
            reciever = callerReciever;

            busy = false;
            Boolean recognizeHit = false;
            this.RecognizerRule = new SpeechLib.SpInProcRecoContext();
            foreach (SpObjectToken recoperson in this.RecognizerRule.Recognizer.GetRecognizers()) //'Go through the SR enumeration
            {
                string language = recoperson.GetAttribute("Language");
                if (language == "411") // 411=日本語
                {
                    this.RecognizerRule.Recognizer.Recognizer = recoperson;
                    recognizeHit = true;
                    break;
                }
            }
            if (!recognizeHit)
            {
                System.Windows.Forms.MessageBox.Show("日本語認識が利用できません", "マイク初期化エラー");
            }

            this.RecognizerRule.Recognizer.AudioInput = GetAudioInput();

            this.RecognizerRule.Hypothesis +=
                delegate (int streamNumber, object streamPosition, SpeechLib.ISpeechRecoResult result)
                {
                };

            this.RecognizerRule.Recognition +=
                delegate (int streamNumber, object streamPosition, SpeechLib.SpeechRecognitionType srt, SpeechLib.ISpeechRecoResult isrr)
                {
                    if (busy)
                        return;

                    busy = true;
                    string text = isrr.PhraseInfo.GetText(0, -1, true);
                    reciever.ReuqestByVoice(text);
                    busy = false;
                };

            this.RecognizerRule.StartStream +=
                delegate (int streamNumber, object streamPosition)
                {
                };

            this.RecognizerRule.FalseRecognition +=
                delegate (int streamNumber, object streamPosition, SpeechLib.ISpeechRecoResult isrr)
                {
                };
        }

        /// <summary>
        /// 音声認識で反応する単語（複数）を登録
        /// </summary>
        public void RegistWord(string[] words)
        {
            int max = words.Length;

            this.RecognizerGrammarRule = this.RecognizerRule.CreateGrammar(0);

            this.RecognizerGrammarRule.Reset(0);
            this.RecognizerGrammarRuleGrammarRule = this.RecognizerGrammarRule.Rules.Add("TopLevelRule",
                SpeechRuleAttributes.SRATopLevel | SpeechRuleAttributes.SRADynamic);

            foreach(string word in words)
            {
                this.RecognizerGrammarRuleGrammarRule.InitialState.AddWordTransition(null, word);
            }
            this.RecognizerGrammarRule.Rules.Commit();
        }

        /// <summary>
        /// 音声認識開始
        /// </summary>
        public void Start()
        {
            this.RecognizerGrammarRule.CmdSetRuleState("TopLevelRule", SpeechRuleState.SGDSActive);
        }

        /// <summary>
        /// 音声認識停止
        /// </summary>
        public void Stop()
        {
            this.RecognizerGrammarRule.CmdSetRuleState("TopLevelRule", SpeechRuleState.SGDSInactive);
        }
    }
}
