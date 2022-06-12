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
        private KTANE_Speech _ktaneSpeech;
        public static string Output { get; set; }   
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize speech recognition.
            _ktaneSpeech = new KTANE_Speech();
            _ktaneSpeech.RecognitionEngine.SpeechRecognized += DefaultSpeechRecognized;

            foreach (var voice in _ktaneSpeech.GetAllVoices())
                comboBoxVoices.Items.Add(voice);

            comboBoxVoices.SelectedIndex = 0;
            UpdateInput();
        }
        
        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            textBoxInput.Text = e.Result.Text;
            textBoxOutput.Text = _ktaneSpeech.AnalyzeSpeech(e.Result.Text);
            _ktaneSpeech.Speak(textBoxOutput.Text);

            UpdateInput();
            UpdateProperties();
        }

        private void UpdateInput()
        {
            var defuseGrammars = new Dictionary<string, string>
            {
                { "Standard Defuse Grammar", "Defuse <module>|Bomb Check|The bomb is Defused|The bomb exploded" },
                { "Complicated Grammar", "<THE COLORS OF THE WIRE> + <nothing|star|light|star and light>" },
                { "Sequence Grammar", "<COLOR OF THE WIRE> + <alpha|bravo|charlie>, To escape say \"done.\"" },
                { "Button Grammar", "<color> <label>|<color> stripe" },
                { "Simon Says Grammar", "<color that flashes last>" },
                { "Wires Grammar", "<color> (await for next wire)" },
                {
                    "Who's On First Grammar",
                    "Say what you see as is except: red color, u(r) letter(s), ar ee ee dee, el ee ee dee, their OR your pronoun," +
                    " you're or they're apostrophe, charlie echo echo (cee), u h space u h. Say \"Stop\" to stop speaking."
                },
                {
                    "Password Grammar",
                    "<military alphabet|regular alphabet> (await for next). To escape say \"escape module.\""
                },
                {
                    "Morse Grammar",
                    "<0>|<1> (await for next letter) (0 is DOT, 1 is the DASH). To escape say \"escape module\"."
                },
                {
                    "Knob Grammar",
                    "<zero or one, three times> space <zero or one, three times> (Say the upper right and lower right lights. Zero is unlit, one is lit)"
                },
                {
                    "Symbols Grammar",
                    "<symbol> (await for next symbol). I have no idea how to explain this. Check SymbolsGrammar.txt to see all available symbols."
                },
                { "Maze Grammar", "<0-6>, <0-6>. To escape say \"escape module\"." },
                { "Memory Grammar", "Numbers <all four numbers that you see>." },
                {
                    "Bomb Check Grammar",
                    "Batteries <0-6> or none or more than two|Freak <yes/true/false/no>|Car <yes/true/false/no>|Vowel <yes/true/false/no>|Port <yes/true/false/no>|Digit <odd/even>"
                }
            };

            labelGrammarInput.Text = defuseGrammars[_ktaneSpeech.RecognitionEngine.Grammars.Last().Name];
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            //start listening.
            if (!_ktaneSpeech.Enabled)
            {
                _ktaneSpeech.Enable();
                buttonStart.Text = "STOP BOT";
                buttonStart.BackColor = Color.DarkRed;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
                
            }
            
            //stop listening.
            else
            {
                _ktaneSpeech.Disable();
                buttonStart.Text = "START BOT";
                buttonStart.BackColor = Color.Green;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    @"Are you sure you want to reset the bomb? All its properties will be gone, and you will have to initialize them again.",
                    @"Reset Bomb", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            _ktaneSpeech.ResetBomb();
            UpdateProperties();
        }

        private void buttonRandomBomb_Click(object sender, EventArgs e)
        {
            _ktaneSpeech.InitializeRandomBomb();
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            labelBatteries.Text = $@"Batteries: {(_ktaneSpeech.BombProperties["Batteries"] == -1 ? "--" : _ktaneSpeech.BombProperties["Batteries"].ToString())}";
            labelFRK.Text = $@"FRK: {(_ktaneSpeech.BombProperties["Freak"] == -1 ? "--" : _ktaneSpeech.BombProperties["Freak"] == 1 ? "Yes" : "No")}";
            labelCAR.Text = $@"CAR: {(_ktaneSpeech.BombProperties["Car"] == -1 ? "--" : _ktaneSpeech.BombProperties["Car"] == 1 ? "Yes" : "No")}";
            labelPort.Text = $@"Port: {(_ktaneSpeech.BombProperties["Port"] == -1 ? "--" : _ktaneSpeech.BombProperties["Port"] == 1 ? "Yes" : "No")}";
            labelVowel.Text = $@"Vowel: {(_ktaneSpeech.BombProperties["Vowel"] == -1 ? "--" : _ktaneSpeech.BombProperties["Vowel"] == 1 ? "Yes" : "No")}";
            labelDigit.Text = $@"Digit: {(_ktaneSpeech.BombProperties["Digit"] == -1 ? "--" : _ktaneSpeech.BombProperties["Digit"] == 1 ? "Odd" : "Even")}";
        }

        private void comboBoxVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ktaneSpeech.ChangeVoice(comboBoxVoices.SelectedItem.ToString());
        }
    }
}
