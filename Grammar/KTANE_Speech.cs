using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace KTANE_Bot
{
    internal enum States
    {
        Checking,
        Waiting,
        Defusing
    }
    
    public class KTANE_Speech
    {
        public SpeechRecognitionEngine RecognitionEngine { get; private set; }
        public bool Enabled { get; private set; }
        
        private Bomb _bomb;
        private States _state;
        private Dictionary<string, int> _bombProperties = new Dictionary<string, int>
        {
            {"Batteries", -1},
            {"Parallel",  -1},
            {"Freak",     -1},
            {"Car",       -1},
            {"Vowel",     -1},
            {"Digit",     -1}
        };
        private SpeechSynthesizer _ktaneBot;
        private SpeechRecognitionEngine
            _defaultSpeech,
            _buttonSolver,
            _memorySolver,
            _wiresSolver,
            _sequenceSolver,
            _complexSolver,
            _simonSaysSolver;

        public KTANE_Speech()
        {
            //speech synthesizer
            _ktaneBot = new SpeechSynthesizer();
            _ktaneBot.SelectVoice("Microsoft Zira Desktop");
            
            _defaultSpeech = _buttonSolver = _memorySolver = _wiresSolver = _sequenceSolver = _complexSolver = _simonSaysSolver = new SpeechRecognitionEngine();

            _state = States.Checking;
            
            RecognitionEngine = new SpeechRecognitionEngine();
            RecognitionEngine.SetInputToDefaultAudioDevice();
            RecognitionEngine.LoadGrammarAsync(DefuseGrammar.StandardDefuseGrammar);
            Disable();
        }

        private string AnalyzeSpeech(string command)
        {
            
        }

        public void Enable()
        {
            Enabled = true;
            RecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void Disable()
        {
            Enabled = false;
            RecognitionEngine.RecognizeAsyncCancel();
        }

        private void Speak(string message)
        {
            
            _ktaneBot.SpeakAsync(message);
        }
        
    }
}