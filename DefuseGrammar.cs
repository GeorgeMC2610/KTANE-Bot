using System.IO;
using System.Speech.Recognition;

namespace KTANE_Bot
{
    public static class DefuseGrammar
    {
        public static Grammar StandardDefuseGrammar = new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt"))));
        public static Grammar BombCheckGrammar => bombCheckGrammar();

        //bomb checking grammar
        private static Grammar bombCheckGrammar()
        {
            var batteryChoices = new Choices(new string[] {"none", "0", "1", "2", "more than 2", "3", "4", "5", "6"});
            var countBatteries = new GrammarBuilder(batteryChoices);
            var trueOrFalse = new Choices(new string[] {"yes", "no", "true", "false"});
            var trueOrFalseChoices = new GrammarBuilder(trueOrFalse);
            var oddEven = new Choices(new string[] {"odd", "even"});
            var oddEvenChoices = new GrammarBuilder(oddEven);
                
            //batteries
            var battery = new GrammarBuilder("Batteries");
            battery.Append(countBatteries);
                
            //parallel port
            var parallelPort = new GrammarBuilder("Parallel");
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

    }
}