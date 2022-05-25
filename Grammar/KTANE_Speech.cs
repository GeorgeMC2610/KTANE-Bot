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

    internal enum Solvers
    {
        Default,
        Check,
        Button,
        Memory,
        Wires,
        Sequence,
        Complex,
        SimonSays
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
        public KTANE_Speech()
        {
            //speech synthesizer
            _ktaneBot = new SpeechSynthesizer();
            _ktaneBot.SelectVoice("Microsoft Zira Desktop");
            
            //switch to waiting state, so the bomb solver waits for a command.
            _state = States.Waiting;
            
            //initialize default
            RecognitionEngine = new SpeechRecognitionEngine();
            RecognitionEngine.SetInputToDefaultAudioDevice();
            RecognitionEngine.LoadGrammarAsync(DefuseGrammar.StandardDefuseGrammar);
            Disable();
        }

        public string AnalyzeSpeech(string command)
        {
            switch (_state)
            {
                case States.Checking:
                    int value;
                    string message;
                    
                    if (command.EndsWith("yes") || command.EndsWith("true") || command.EndsWith("odd"))
                        value = 1;
                    else if (command.EndsWith("no") || command.EndsWith("false") || command.EndsWith("even"))
                        value = 0;
                    else if (command.EndsWith("none"))
                        value = 0;
                    else if (command.EndsWith("more than 2"))
                        value = int.MaxValue;
                    else
                        value = int.Parse(command.Split(' ')[1]);

                    _bombProperties[command.Split(' ')[0]] = value;
                    return $"{command.Split(' ')[0]} {_bombProperties[command.Split(' ')[0]]}";
                    
                    break;
                case States.Waiting:
                    if (_bomb == null)
                    {
                        if (command != "Bomb check")
                            return "You must first initialize the bomb. (Say \"Bomb check\" to do so)";

                        SwitchDefaultSpeechRecognizer(Solvers.Check);
                        _state = States.Checking;
                        return "Start checking phase.";
                    }

                    break;
                case States.Defusing:
                    break;
            }

            return "No";
        }

        private void SwitchDefaultSpeechRecognizer(Solvers solver)
        {
            RecognitionEngine.UnloadAllGrammars();

            switch (solver)
            {
                case Solvers.Default:
                    RecognitionEngine.LoadGrammarAsync(DefuseGrammar.StandardDefuseGrammar);
                    break;
                case Solvers.Check:
                    RecognitionEngine.LoadGrammarAsync(DefuseGrammar.BombCheckGrammar);
                    break;
                case Solvers.Button:
                    RecognitionEngine.LoadGrammarAsync(DefuseGrammar.ButtonGrammar);
                    break;
                case Solvers.Memory:
                    break;
                case Solvers.Wires:
                    break;
                case Solvers.Sequence:
                    break;
                case Solvers.Complex:
                    break;
                case Solvers.SimonSays:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(solver), solver, null);
            }
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

        public void Speak(string message)
        {
            _ktaneBot.SpeakAsync(message);
        }
    }
}