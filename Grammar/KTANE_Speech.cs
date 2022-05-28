using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        Wires,
        Button,
        Symbols,
        Memory,
        Complicated,
        Simon,
        Sequence,
        Morse,
        Knob,
        Password
    }
    
    public class KTANE_Speech
    {
        public SpeechRecognitionEngine RecognitionEngine { get; private set; }
        public bool Enabled { get; private set; }

        private KTANE_Module _defusingModule;
        private Bomb _bomb;
        private States _state;
        private Solvers _solvingModule;
        private Dictionary<string, int> _bombProperties = new Dictionary<string, int>
        {
            {"Batteries", -1},
            {"Parallel",  -1},
            {"Freak",     -1},
            {"Car",       -1},
            {"Vowel",     -1},
            {"Digit",     -1}
        };

        private Dictionary<string, Solvers> _solvingGrammar = new Dictionary<string, Solvers>
        {
            { "Defuse wires", Solvers.Wires },
            { "Defuse button", Solvers.Button },
            { "Defuse memory", Solvers.Memory },
            { "Defuse sequence", Solvers.Sequence },
            { "Defuse complicated", Solvers.Complicated },
            { "Defuse simon", Solvers.Simon }
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
                    //identify, using the following dict, what the different values mean.
                    var propertiesDictionary = new Dictionary<string, int>
                    {
                        { "yes", 1 },
                        { "true", 1 },
                        { "odd", 1 },
                        { "false", 0 },
                        { "no", 0 },
                        { "none", 0},
                        { "even", 0 },
                        { "more", int.MaxValue }
                    };
                    
                    //the value is going to be assigned according to the dict above. 
                    int value;
                    try
                    {
                        value = propertiesDictionary[command.Split(' ')[1]];
                    }
                    //if the value is not in the dict, then it's a plain number.
                    catch (KeyNotFoundException)
                    {
                        value = int.Parse(command.Split(' ')[1]);
                    }

                    //set the property to be the value we assigned before.
                    _bombProperties[command.Split(' ')[0]] = value;
                    
                    //process the message to be sent.
                    var message = "";
                    switch (command.Split(' ')[0])
                    {
                        case "Batteries":
                            message = value == int.MaxValue ? "More than two batteries." : $"{value} batteries.";
                            break;
                        case "Parallel":
                            message = value == 0 ? "No parallel port." : "Yes parallel port.";
                            break;
                        case "Freak":
                            message = value == 0 ? "No FRK label." : "Lit FRK label.";
                            break;
                        case "Car":
                            message = value == 0 ? "No CAR label." : "Lit CAR label.";
                            break;
                        case "Vowel":
                            message = value == 0 ? "No vowel in serial." : "Vowel in serial.";
                            break;
                        case "Digit":
                            message = value == 0 ? "Last digit is even." : "Last digit is odd.";
                            break;
                    }

                    if (!_bombProperties.ContainsValue(-1))
                    {
                        _bomb = new Bomb(_bombProperties["Batteries"], 
                            _bombProperties["Parallel"] == 1,
                            _bombProperties["Freak"] == 1, 
                            _bombProperties["Car"] == 1, 
                            _bombProperties["Vowel"] == 1,
                            _bombProperties["Digit"] == 0);
                        message += " Done.";
                        _state = States.Waiting;
                        SwitchDefaultSpeechRecognizer(Solvers.Default);
                    }
                    
                    return message;
                case States.Waiting:
                    //if the user hasn't yet initialized the bomb
                    if (_bomb == null)
                    {
                        if (command != "Bomb check")
                            return "You must first initialize the bomb. (Say \"Bomb check\" to do so)";

                        SwitchDefaultSpeechRecognizer(Solvers.Check);
                        _state = States.Checking;
                        return "Start checking phase.";
                    }

                    
                    //if the bomb is initialized.
                    try
                    {
                        SwitchDefaultSpeechRecognizer(_solvingGrammar[command]);
                        _solvingModule = _solvingGrammar[command];
                        _state = States.Defusing;

                        //this is just a long way to say "return the module with first letter being capital and a full stop."
                        return $"{char.ToUpper(command.Split(' ')[1][0]) + command.Split(' ')[1].Substring(1)}.";
                    }
                    catch (KeyNotFoundException)
                    {
                        switch (command)
                        {
                            case "The bomb exploded":
                                return "You're useless.";
                            case "The bomb is defused":
                                return "Good job!";
                            default:
                                return "No.";
                        }
                    }
                case States.Defusing when _bomb != null:

                    switch (_solvingModule)
                    {
                        case Solvers.Wires:
                            command = command.Replace(" ", "");
                            var seperatedWires = command.Split(new string[] { "wire" }, StringSplitOptions.None);
                            seperatedWires = seperatedWires.Take(seperatedWires.Length - 1).ToArray();
                            var wires = new Wires(_bomb, seperatedWires);

                            SwitchToDefaultProperties();
                            return $"{command.Replace("wire", ", ")}; {wires.Solve()}";

                        case Solvers.Button:
                            if (new Button(_bomb, command.Split(' ')[0], command.Split(' ')[1]).Solve() ==
                                "Press and immediately release.")
                            {
                                SwitchToDefaultProperties();
                                return @"Press and immediately release.";
                            }
                            
                            if (_defusingModule == null)
                            {
                                _defusingModule = new Button(_bomb, command.Split(' ')[0], command.Split(' ')[1]);
                                return _defusingModule.Solve();
                            }

                            SwitchToDefaultProperties();
                            return Button.Solve(command.Split(' ')[0]);
                        case Solvers.Symbols:
                            break;
                        case Solvers.Memory:
                            break;
                        case Solvers.Complicated:
                            break;
                        case Solvers.Simon:
                            break;
                        case Solvers.Sequence:
                            break;
                        case Solvers.Morse:
                            break;
                        case Solvers.Knob:
                            break;
                        case Solvers.Password:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }


                    break;
            }

            return "No";
        }

        private void SwitchDefaultSpeechRecognizer(Solvers solver)
        {
            RecognitionEngine.UnloadAllGrammars();

            var grammarDict = new Dictionary<Solvers, Grammar>
            {
                { Solvers.Default, DefuseGrammar.StandardDefuseGrammar },
                { Solvers.Check, DefuseGrammar.BombCheckGrammar},
                { Solvers.Wires, DefuseGrammar.WiresGrammar},
                { Solvers.Button, DefuseGrammar.ButtonGrammar},
                { Solvers.Symbols, null},
                { Solvers.Memory, DefuseGrammar.MemoryGrammar},
                { Solvers.Complicated, null},
                { Solvers.Simon, null},
                { Solvers.Sequence, null},
                { Solvers.Morse, null},
                { Solvers.Knob, null},
                { Solvers.Password, null},
            };

            RecognitionEngine.LoadGrammarAsync(grammarDict[solver]);
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

        private void SwitchToDefaultProperties()
        {
            _defusingModule = null;
            SwitchDefaultSpeechRecognizer(Solvers.Default);
            _solvingModule = Solvers.Default;
            _state = States.Waiting;
        }

        public void ResetBomb()
        {
            _bomb = null;
            _state = States.Waiting;
            _solvingModule = Solvers.Default;

            foreach (var key in _bombProperties.Keys.ToList())
            {
                _bombProperties[key] = -1;
            }
        }

        public void InitializeRandomBomb()
        {
            foreach (var key in _bombProperties.Keys.ToList())
            {
                _bombProperties[key] = new Random().Next(0, 2);
            }
            
            _bomb = new Bomb(_bombProperties["Batteries"], 
                _bombProperties["Parallel"] == 1,
                _bombProperties["Freak"] == 1, 
                _bombProperties["Car"] == 1, 
                _bombProperties["Vowel"] == 1,
                _bombProperties["Digit"] == 0);
            _state = States.Waiting;
            SwitchDefaultSpeechRecognizer(Solvers.Default);
        }
    }
}