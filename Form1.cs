using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Timers;

namespace KTANE_Bot
{
    public partial class Form1 : Form
    {
        //speech recognition packages
        SpeechRecognitionEngine _recognitionEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer KtaneBOT = new SpeechSynthesizer();
        Random random = new Random();

        //bomb states
        private bool Enabled;
        private Bomb bomb;
        private string State;

        private string[] properties = {"", "", "", "", "", ""};
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize voice and speech recognition.
            KtaneBOT.SelectVoice("Microsoft Zira Desktop");
            _recognitionEngine.SetInputToDefaultAudioDevice();
            _recognitionEngine.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt")))));
            _recognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(DefaultSpeechRecognized);
        }

        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //show the input in the text box.
            textBoxInput.Text = e.Result.Text;
            if (State == "bomb checking")
            {
                switch (e.Result.Text.Split(' ')[0])
                {
                    //identify number of batteries.
                    case "Batteries":
                        switch (e.Result.Text.Split(' ')[1])
                        {
                            case "none":
                                properties[0] = "0";
                                OutputMessage("No batteries.");
                                break;
                            case "more":
                                properties[0] = "3";
                                OutputMessage("More than two batteries.");
                                break;
                            default:
                                properties[0] = e.Result.Text.Split(' ')[1];
                                OutputMessage(properties[0] + (properties[0] == "1" ? " battery" : " batteries."));
                                break;
                        }
                        break;
                    //vowel in serial number
                    case "Vowel":
                        properties[1] = e.Result.Text.Split(' ')[1];
                        OutputMessage(properties[1] == "yes" || properties[1] == "true" ? "Vowel in Serial Number." : "No vowel in serial number.");
                        break;
                    //parallel port
                    case "Parallel":
                        properties[2] = e.Result.Text.Split(' ')[1];
                        OutputMessage(properties[2] == "yes" || properties[2] == "true" ? "Parallel port." : "No parallel port.");
                        break;
                    //odd or even digit
                    case "Digit":
                        properties[3] = e.Result.Text.Split(' ')[1];
                        OutputMessage(properties[3] == "even" ? "Even last digit." : "Odd last digit.");
                        break;
                    //FRK Label.
                    case "Freak":
                        properties[4] = e.Result.Text.Split(' ')[1];
                        OutputMessage(properties[4] == "yes" || properties[4] == "true" ? "Lit FRK Label." : "No FRK Label.");
                        break;
                    //CAR label.
                    case "Car":
                        properties[5] = e.Result.Text.Split(' ')[1];
                        OutputMessage(properties[5] == "yes" || properties[5] == "true" ? "Lit CAR Label." : "No CAR Label.");
                        break;
                }

                //if there are no empty properties, initialize the bomb.
                if (!properties.Contains(string.Empty))
                {
                    bomb = new Bomb(int.Parse(properties[0]), 
                                (properties[1] == "yes" || properties[1] == "true"), 
                                (properties[2] == "yes" || properties[2] == "true"),
                                (properties[3] == "yes" || properties[3] == "true"),
                                (properties[4] == "yes" || properties[4] == "true"),
                                (properties[5] == "yes" || properties[5] == "true"));
                    OutputMessage("Done.");
                    State = "";
                }
                
                return;
            }
            
            //if the bomb is not initialized yet.
            if (bomb == null)
            {
                //at first we have to initialize the bomb
                //if the bot doesn't listen to "bomb check" do not continue.
                if (e.Result.Text != "Bomb check")
                {
                    OutputMessage("The bomb is not initialized yet, say \"bomb check\" to start configuring the bomb.");
                    return;
                }
                
                //if it does, initialize bomb checking.
                OutputMessage("Check.");
                
                //varying choices.
                Choices batteryChoices = new Choices(new string[] {"none", "0", "1", "2", "more than 2", "3", "4", "5", "6"});
                GrammarBuilder countBatteries = new GrammarBuilder(batteryChoices);
                Choices trueOrFalse = new Choices(new string[] {"yes", "no", "true", "false"});
                GrammarBuilder trueOrFalseChoices = new GrammarBuilder(trueOrFalse);
                Choices oddEven = new Choices(new string[] {"odd", "even"});
                GrammarBuilder oddEvenChoices = new GrammarBuilder(oddEven);
                
                //batteries
                GrammarBuilder battery = new GrammarBuilder("Batteries");
                battery.Append(countBatteries);
                
                //parallel port
                GrammarBuilder parallelPort = new GrammarBuilder("Parallel");
                parallelPort.Append(trueOrFalse);
                
                //frk, interpreted as the word "freak".
                GrammarBuilder frk = new GrammarBuilder("Freak");
                frk.Append(trueOrFalse);
                
                //car, interpreted as the word "car".
                GrammarBuilder car = new GrammarBuilder("Car");
                car.Append(trueOrFalse);
                
                //vowel in serial number
                GrammarBuilder vowel = new GrammarBuilder("Vowel");
                vowel.Append(trueOrFalse);
                
                //last number of serial number
                GrammarBuilder digit = new GrammarBuilder("Digit");
                digit.Append(oddEven);
                
                Choices allChoices = new Choices(new GrammarBuilder[] {battery, vowel, parallelPort, digit, frk, car});
                Grammar grammar = new Grammar(allChoices);
                _recognitionEngine.LoadGrammarAsync(grammar);
                State = "bomb checking";
            }
            else
            {
                switch (e.Result.Text)
                {
                    case "The bomb is defused":
                        OutputMessage("We did it!");
                        break;
                    case "The bomb exploded":
                        OutputMessage("You're useless.");
                        break;
                }
            }
        }

        private void OutputMessage(string message)
        {
            textBoxOutput.Text = message;
            KtaneBOT.SpeakAsync(message);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Enabled = !Enabled;

            //start listening.
            if (Enabled)
            {
                buttonStart.Text = "STOP BOT";
                buttonStart.BackColor = Color.DarkRed;
                textBoxInput.Enabled = textBoxOutput.Enabled = Enabled;
                _recognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            
            //stop listening.
            else
            {
                buttonStart.Text = "START BOT";
                buttonStart.BackColor = Color.Green;
                textBoxInput.Enabled = textBoxOutput.Enabled = Enabled;
                _recognitionEngine.RecognizeAsyncCancel();
            }
        }
        
        private void buttonExecute_Click(object sender, EventArgs e)
        {

        }
    }
}
