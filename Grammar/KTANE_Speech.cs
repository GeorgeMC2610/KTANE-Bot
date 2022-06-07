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
        WhoIsOnFirst,
        Morse,
        Knob,
        Password,
        Maze
    }
    
    public class KTANE_Speech
    {
        public SpeechRecognitionEngine RecognitionEngine { get; private set; }
        public bool Enabled { get; private set; }

        private KTANE_Module _defusingModule;
        private Bomb _bomb;
        private States _state;
        private Solvers _solvingModule;
        public Dictionary<string, int> BombProperties = new Dictionary<string, int>
        {
            {"Batteries", -1},
            {"Port",      -1},
            {"Freak",     -1},
            {"Car",       -1},
            {"Vowel",     -1},
            {"Digit",     -1}
        };

        private Dictionary<string, Solvers> _solvingGrammar = new Dictionary<string, Solvers>
        {
            { "Defuse wires", Solvers.Wires },
            { "Defuse button", Solvers.Button },
            { "Defuse symbols", Solvers.Symbols },
            { "Defuse memory", Solvers.Memory },
            { "Defuse complicated", Solvers.Complicated },
            { "Defuse simon", Solvers.Simon },
            { "Defuse sequence", Solvers.Sequence },
            { "Defuse who is on first", Solvers.WhoIsOnFirst },
            { "Defuse morse", Solvers.Morse },
            { "Defuse knob", Solvers.Knob },
            { "Defuse password", Solvers.Password },
            { "Defuse maze", Solvers.Maze }
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
                    BombProperties[command.Split(' ')[0]] = value;
                    
                    //process the message to be sent.
                    var message = "";
                    switch (command.Split(' ')[0])
                    {
                        case "Batteries":
                            message = value == int.MaxValue ? "More than two batteries." : $"{value} " + (value == 1? "battery" : "batteries");
                            break;
                        case "Port":
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

                    if (!BombProperties.ContainsValue(-1))
                    {
                        _bomb = new Bomb(BombProperties["Batteries"], 
                            BombProperties["Port"] == 1,
                            BombProperties["Freak"] == 1, 
                            BombProperties["Car"] == 1, 
                            BombProperties["Vowel"] == 1,
                            BombProperties["Digit"] == 0);
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

                        var additionalInfo = string.Empty;

                        switch (command)
                        {
                            case "Defuse memory":
                                additionalInfo = ", Stage 1";
                                break;
                            case "Defuse morse":
                                additionalInfo = ", first letter";
                                break;
                            case "Defuse who is on first":
                                additionalInfo = "is on first, what's on the display?";
                                break;
                            case "Defuse maze":
                                additionalInfo = "; Any green circle coordinates.";
                                break;
                            case "Defuse password":
                                additionalInfo = "; Column 1";
                                break;
                        }
                        
                        //this is just a long way to say "return the module with first letter being capital."
                        return $"{char.ToUpper(command.Split(' ')[1][0]) + command.Split(' ')[1].Substring(1)} {additionalInfo}";
                    }
                    catch (KeyNotFoundException)
                    {
                        var rng = new Random();
                        
                        switch (command)
                        {
                            case "The bomb exploded":
                                var dismissingMessages = new[] { "Aww :(", "It's your fault.", "Think faster.", "You're useless.", "We tried our best." };
                                return dismissingMessages[rng.Next(0, dismissingMessages.Length)];
                            
                            case "The bomb is defused":
                                var congratulatoryMessages = new[] { "Good job!", "Nice!", "You did it!", "Yay!", "Woo-hoo!", "Congratulations!" };
                                return congratulatoryMessages[rng.Next(0, congratulatoryMessages.Length)];
                            
                            case "Stop":
                                StopSpeaking();
                                return "";
                            
                            default:
                                return "No.";
                        }
                    }
                case States.Defusing when _bomb != null:

                    switch (_solvingModule)
                    {
                        //WIRES SOLVER.
                        case Solvers.Wires:
                            if (_defusingModule == null)
                            {
                                if (command == "done") return "You must give a color";
                                
                                var defusingModule = new Wires(_bomb);
                                defusingModule.AppendWire(command);
                                _defusingModule = defusingModule;
                                
                                return $"{command}; next.";
                            }

                            var wires = (Wires)_defusingModule;

                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "Done; " + wires.Solve();
                            }

                            wires.AppendWire(command);

                            if (wires.WireCount != 6) return $"{command}; next.";

                            SwitchToDefaultProperties();
                            return $"{command}; done. {wires.Solve()}";
                        
                        //BUTTON SOLVER.
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
                        
                        //SYMBOLS SOLVER.
                        case Solvers.Symbols:
                            if (_defusingModule == null)
                                _defusingModule = new Symbols(_bomb);

                            var symbols = (Symbols)_defusingModule;

                            if (symbols.InputLength < 3)
                            {
                                symbols.AppendSymbol(command);
                                return $"{command}; next.";
                            }

                            symbols.AppendSymbol(command);
                            SwitchToDefaultProperties();
                            return $"{command}; done. {symbols.Solve()}";

                        //MEMORY SOLVER.
                        case Solvers.Memory:
                            if (_defusingModule == null)
                                _defusingModule = new Memory(_bomb);

                            var memory = (Memory)_defusingModule;

                            if (!memory.SetNumbers(command.Split(' ')))
                                return @"Try again.";

                            if (memory.Stage != 5) return memory.Solve();
                            
                            SwitchToDefaultProperties();
                            return memory.Solve();

                        //COMPLEX WIRES SOLVER.
                        case Solvers.Complicated:
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }
                            
                            if (_defusingModule == null)
                                _defusingModule = new ComplexWires(_bomb);

                            var complexWire = (ComplexWires)_defusingModule;
                            complexWire.InterpretInput(command);
                            return complexWire.Solve(); 
                        
                        //SIMON SAYS SOLVER.
                        case Solvers.Simon:
                            if (_defusingModule == null)
                                _defusingModule = new Simon(_bomb);

                            var simon = (Simon)_defusingModule;

                            if (command.Contains("strikes"))
                            {
                                simon.SetStrikes(int.Parse(command.Split(' ')[1]));
                                return $"{command.Split(' ')[1]} " + (command.Split(' ')[1] == "1"? "strike" : "strikes");
                            }
                            
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }
                            
                            simon.AppendColor(command);
                            return simon.Solve();

                        //WIRE SEQUENCE SOLVER.
                        case Solvers.Sequence:
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Sequence(_bomb);

                            var sequence = (Sequence)_defusingModule;
                            sequence.InitializeValues(command.Split(' ')[0], command.Split(' ')[1]);
                            return sequence.Solve();
                        
                        //WHO'S ON FIRST.
                        case Solvers.WhoIsOnFirst:
                            if (_defusingModule == null)
                                _defusingModule = new WhoIsOnFirst(_bomb);

                            var whoIsOnFirst = (WhoIsOnFirst)_defusingModule;

                            if (whoIsOnFirst.Display == string.Empty)
                            {
                                whoIsOnFirst.Display = command;
                                return whoIsOnFirst.Solve();
                            }

                            whoIsOnFirst.Button = command;
                            SwitchToDefaultProperties();
                            return whoIsOnFirst.Solve();
                        
                        //MORSE CODE SOLVER.
                        case Solvers.Morse:
                            if (_defusingModule == null)
                                _defusingModule = new Morse(_bomb);

                            var morse = (Morse)_defusingModule;

                            if (command.Contains('.') || command.Contains('-'))
                            {
                                if (!morse.AddLetters(command.ToCharArray()))
                                    return @"Try again";
                            }
                            else if (!morse.AddLetters(command.Split(' ')))
                                return @"Try again.";
                            
                            if (morse.Solve().EndsWith("hertz."))
                                SwitchToDefaultProperties();

                            return morse.Solve();
                        
                        //KNOB SOLVER.
                        case Solvers.Knob:
                            if (_defusingModule == null)
                                _defusingModule = new Knob(_bomb);

                            var knob = (Knob)_defusingModule;
                            knob.Lights = command;
                            SwitchToDefaultProperties();
                            return knob.Solve();
                        
                        //PASSWORD SOLVER.
                        case Solvers.Password:
                            if (_defusingModule == null)
                                _defusingModule = new Password(_bomb);

                            var password = (Password)_defusingModule;

                            switch (password.AssignLetters(command.ToLower()))
                            {
                                case -1:
                                    return "There can be no duplicate letters in the password. Try again.";
                                case 2:
                                    return $"{command}; next";
                                default:
                                    if (password.Solve().StartsWith("Try") || password.Solve().StartsWith("Something"))
                                        SwitchToDefaultProperties();

                                    return password.Solve();
                            }

                        //MAZE SOLVER.
                        case Solvers.Maze:
                            if (_defusingModule == null)
                                _defusingModule = new Maze(_bomb);

                            var maze = (Maze)_defusingModule;

                            if (maze.TargetMaze == null)
                            {
                                if (maze.AssignCircle(int.Parse(command[0].ToString()), int.Parse(command[2].ToString())))
                                    return "White square coordinates.";

                                return "Try again";
                            }
                            
                            if (maze.SquareLocation.X == -1 || maze.SquareLocation.Y == -1)
                            {
                                maze.SetSquare(int.Parse(command[0].ToString()), int.Parse(command[2].ToString()));
                                return "Triangle coordinates.";
                            }

                            if (maze.TriangleLocation.X == -1 || maze.TriangleLocation.Y == -1)
                            {
                                maze.SetTriangle(int.Parse(command[0].ToString()), int.Parse(command[2].ToString()));
                                
                                if (maze.TriangleLocation.X == maze.SquareLocation.X &&
                                    maze.TriangleLocation.Y == maze.SquareLocation.Y)
                                {
                                    maze.SetTriangle(0, 0);
                                    return "Square and triangle must be in different places; try again.";
                                }
                            }

                            SwitchToDefaultProperties();
                            return maze.Solve();

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
                { Solvers.Default,      DefuseGrammar.StandardDefuseGrammar },
                { Solvers.Check,        DefuseGrammar.BombCheckGrammar },
                { Solvers.Wires,        DefuseGrammar.WiresGrammar },
                { Solvers.Button,       DefuseGrammar.ButtonGrammar },
                { Solvers.Symbols,      DefuseGrammar.SymbolsGrammar },
                { Solvers.Memory,       DefuseGrammar.MemoryGrammar },
                { Solvers.Complicated,  DefuseGrammar.ComplicatedGrammar },
                { Solvers.Simon,        DefuseGrammar.SimonSaysGrammar },
                { Solvers.Sequence,     DefuseGrammar.SequenceGrammar },
                { Solvers.WhoIsOnFirst, DefuseGrammar.WhoIsOnFirstGrammar },
                { Solvers.Morse,        DefuseGrammar.MorseGrammar },
                { Solvers.Knob,         DefuseGrammar.KnobGrammar },
                { Solvers.Password,     DefuseGrammar.PasswordGrammar },
                { Solvers.Maze,         DefuseGrammar.MazeGrammar }
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

        public void StopSpeaking()
        {
            _ktaneBot.SpeakAsyncCancelAll();
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
            SwitchToDefaultProperties();

            foreach (var key in BombProperties.Keys.ToList())
                BombProperties[key] = -1;
        }

        public void InitializeRandomBomb()
        {
            var rng = new Random();
            
            foreach (var key in BombProperties.Keys.ToList())
            {
                if (key == "Batteries")
                {
                    BombProperties[key] = rng.Next(0, 7);
                    continue;
                }
                
                BombProperties[key] = rng.Next(0, 2);
            }
            
            _bomb = new Bomb(BombProperties["Batteries"], 
                BombProperties["Port"] == 1,
                BombProperties["Freak"] == 1, 
                BombProperties["Car"] == 1, 
                BombProperties["Vowel"] == 1,
                BombProperties["Digit"] == 0);
            _state = States.Waiting;
            SwitchDefaultSpeechRecognizer(Solvers.Default);
        }
    }
}