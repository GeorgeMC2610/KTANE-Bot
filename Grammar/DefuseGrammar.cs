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
        public static Grammar SymbolsGrammar => _SymbolsGrammar();

        public static Grammar SequenceGrammar => _SequenceGrammar();

        private static Grammar _StandardDefuseGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt"))));
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
            return new Grammar(allChoices);
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
            return new Grammar(allChoices);
        }

        private static Grammar _MemoryGrammar()
        {
            var nums = new Choices(new string[] {"1", "2", "3", "4"});
            var sequence = new GrammarBuilder("Numbers");
            sequence.Append(nums);

            var allChoices = new Choices(sequence);
            return new Grammar(allChoices);
        }

        private static Grammar _WiresGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(new string[] {"yellow", "blue", "black", "white", "red", "done"})));
        }

        private static Grammar _SymbolsGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Symbols.txt"))));
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

            return new Grammar(allChoices);
        }
    }
}