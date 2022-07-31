using System.IO;
using System.Speech.Recognition;

namespace KTANE_Bot
{
    public static class DefuseGrammar
    {
        public static Grammar StandardDefuseGrammar => _StandardDefuseGrammar();
        public static Grammar BombCheckGrammar => _BombCheckGrammar();
        public static Grammar ButtonGrammar => _ButtonGrammar();
        public static Grammar MemoryGrammar => _MemoryGrammar();
        public static Grammar WiresGrammar => _WiresGrammar();
        public static Grammar KnobGrammar => _KnobGrammar();
        public static Grammar MazeGrammar => _MazeGrammar();
        public static Grammar MorseGrammar => _MorseGrammar();
        public static Grammar SymbolsGrammar => _SymbolsGrammar();
        public static Grammar PasswordGrammar => _PasswordGrammar();
        public static Grammar SequenceGrammar => _SequenceGrammar();
        public static Grammar SimonSaysGrammar => _SimonSaysGrammar();
        public static Grammar ComplicatedGrammar => _ComplicatedGrammar();
        public static Grammar WhoIsOnFirstGrammar => _WhoIsOnFirstGrammar();
        

        //METHODS TO RETRIEVE GRAMMARS.
        private static Grammar _StandardDefuseGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt")))) {Name = "Standard Defuse Grammar"};
        }
        
        //bomb checking grammar
        private static Grammar _BombCheckGrammar()
        {
            var batteryChoices = new Choices(new string[] {"none", "0", "1", "2", "more than 2", "3", "4", "5", "6"});
            var countBatteries = new GrammarBuilder(batteryChoices);
            var trueOrFalse = new Choices(new string[] {"yes", "no", "true", "false"});
            var oddEven = new Choices(new string[] {"odd", "even"});
            
            //batteries
            var battery = new GrammarBuilder("Batteries");
            battery.Append(countBatteries);
                
            //parallel port
            var parallelPort = new GrammarBuilder("Port");
            parallelPort.Append(trueOrFalse);
                
            //frk, interpreted as the word "freak".
            var frk = new GrammarBuilder("Freak");
            frk.Append(trueOrFalse);
                
            //car, interpreted as the word "car".
            var car = new GrammarBuilder("Car");
            car.Append(trueOrFalse);
                
            //vowel in serial number
            var vowel = new GrammarBuilder("Vowel");
            vowel.Append(trueOrFalse);
                
            //last number of serial number
            var digit = new GrammarBuilder("Digit");
            digit.Append(oddEven);
                
            var allChoices = new Choices(new GrammarBuilder[] {battery, vowel, parallelPort, digit, frk, car});
            return new Grammar(allChoices) {Name = "Bomb Check Grammar"};
        }

        private static Grammar _ButtonGrammar()
        {
            var labelChoices = new Choices(new string[] { "detonate", "hold", "press", "abort", "stripe"});
            var red = new GrammarBuilder("Red");
            var yellow = new GrammarBuilder("Yellow");
            var blue = new GrammarBuilder("Blue");
            var white = new GrammarBuilder("White");
            
            red.Append(labelChoices);
            yellow.Append(labelChoices);
            blue.Append(labelChoices);
            white.Append(labelChoices);

            var allChoices = new Choices(new GrammarBuilder[] {red, yellow, blue, white});
            return new Grammar(allChoices) {Name = "Button Grammar"};
        }

        private static Grammar _MemoryGrammar()
        {
            var grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Numbers", 1, 7);
            grammarBuilder.AppendDictation(category: "numbers");

            return new Grammar(grammarBuilder) {Name = "Memory Grammar"};
        }

        private static Grammar _WiresGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(new string[] {"yellow", "blue", "black", "white", "red", "done", "wrong"}))) {Name = "Wires Grammar"};
        }
        
        private static Grammar _KnobGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Knob.txt")))) {Name = "Knob Grammar"};
        }

        private static Grammar _MazeGrammar()
        {
            var numbers = new Choices("1", "2", "3", "4", "5", "6");

            var one = new GrammarBuilder("1");
            var two = new GrammarBuilder("2");
            var three = new GrammarBuilder("3");
            var four = new GrammarBuilder("4");
            var five = new GrammarBuilder("5");
            var six = new GrammarBuilder("6");
            var done = new GrammarBuilder("ESCAPE MODULE");
            
            one.Append(numbers);
            two.Append(numbers);
            three.Append(numbers);
            four.Append(numbers);
            five.Append(numbers);
            six.Append(numbers);
            
            
            var allChoices = new Choices(new GrammarBuilder[] { one, two, three, four, five, six, done });
            
            return new Grammar(allChoices) {Name = "Maze Grammar"};
        }

        private static Grammar _MorseGrammar()
        {
            var builder = new GrammarBuilder();
            
            builder.AppendDictation();
            return new Grammar(builder) {Name = "Morse Grammar"};
        }

        private static Grammar _SymbolsGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Symbols.txt")))) {Name = "Symbols Grammar"};
        }

        private static Grammar _PasswordGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Password.txt")))) {Name = "Password Grammar"};
        }

        private static Grammar _SequenceGrammar()
        {
            var letters = new Choices("alpha", "bravo", "charlie");

            var red = new GrammarBuilder("red");
            var blue = new GrammarBuilder("blue");
            var black = new GrammarBuilder("black");
            var done = new GrammarBuilder("done");
            
            red.Append(letters);
            blue.Append(letters);
            black.Append(letters);

            var allChoices = new Choices(new GrammarBuilder[] { red, black, blue, done });

            return new Grammar(allChoices) {Name = "Sequence Grammar"};
        }

        private static Grammar _ComplicatedGrammar()
        {
            var properties = new Choices("nothing", "star", "light", "star and light", "light and star");

            var red = new GrammarBuilder("red");
            var redWhite = new GrammarBuilder("red and white");
            var whiteRed = new GrammarBuilder("white and red");
            
            var blue = new GrammarBuilder("blue");
            var blueWhite = new GrammarBuilder("blue and white");
            var whiteBlue = new GrammarBuilder("white and blue");
            
            var blueRed = new GrammarBuilder("blue and red");
            var redBlue = new GrammarBuilder("red and blue");

            var white = new GrammarBuilder("white");

            var done = new GrammarBuilder("done");
            
            red.Append(properties);
            redWhite.Append(properties);
            whiteRed.Append(properties);
            blue.Append(properties);
            blueWhite.Append(properties);
            whiteBlue.Append(properties);
            blueRed.Append(properties);
            redBlue.Append(properties);
            white.Append(properties);

            var allChoices = new Choices(new GrammarBuilder[] { red, redWhite, whiteRed, blue, blueWhite, whiteBlue, blueRed, redBlue, white, done });
            
            return new Grammar(allChoices) {Name = "Complicated Grammar"};
        }

        private static Grammar _SimonSaysGrammar()
        {
            var strikeChoices = new Choices("1", "2");
            var strikeBuilder = new GrammarBuilder("strikes");
            
            strikeBuilder.Append(strikeChoices);

            var blue = new GrammarBuilder("blue");
            var green = new GrammarBuilder("green");
            var red = new GrammarBuilder("red");
            var yellow = new GrammarBuilder("yellow");
            var done = new GrammarBuilder("done");

            var allChoices = new Choices(new GrammarBuilder[] { strikeBuilder, blue, red, green, yellow, done });
            return new Grammar(allChoices) {Name = "Simon Says Grammar"};
        }

        private static Grammar _WhoIsOnFirstGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"WhoIsOnFirst.txt")))) {Name = "Who's On First Grammar"};
        }
    }
}