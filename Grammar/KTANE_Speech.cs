using System;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace KTANE_Bot
{
    public class KTANE_Speech
    {
        public SpeechRecognitionEngine RecognitionEngine { get; private set; }
        
        private SpeechSynthesizer _ktaneBot;
        private SpeechRecognitionEngine
            _defaultSpeech,
            _buttonSolver,
            _memorySolver,
            _wiresSolver,
            _sequenceSolver,
            _complexSolver,
            _simonSaysSolver;

        public bool Enabled { get; private set; }

        public KTANE_Speech()
        {
            //speech synthesizer
            _ktaneBot = new SpeechSynthesizer();
            _ktaneBot.SelectVoice("Microsoft Zira Desktop");
            
            _defaultSpeech = _buttonSolver = _memorySolver = _wiresSolver = _sequenceSolver = _complexSolver = _simonSaysSolver = new SpeechRecognitionEngine();
            RecognitionEngine = new SpeechRecognitionEngine();
            RecognitionEngine.SetInputToDefaultAudioDevice();
            RecognitionEngine.LoadGrammarAsync(DefuseGrammar.StandardDefuseGrammar);
            RecognitionEngine.SpeechRecognized += DefaultSpeechRecognized;
            Disable();
        }

        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("Hello from KTANE_Speech");
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