using System.Data;
using System.Threading.Tasks;

namespace KTANE_Bot
{
    public static class Button
    {
        public static string Solve(Bomb bomb, string color, string label)
        {
            if (color == "Blue" && label == "abort")       return "Hold the button, what is the stripe colour?";
            if (bomb.Batteries > 1 && label == "detonate") return "Press and immediately release.";
            if (bomb.CAR && color == "White")              return "Hold the button, what is the stripe colour?";
            if (bomb.FRK && bomb.Batteries > 2)            return "Press and immediately release.";
            if (color == "Yellow")                         return "Hold the button, what is the stripe colour?";
            if (color == "Red" && label == "hold")         return "Press and immediately release.";
            
            return "Hold the button, what is the stripe colour?";
        }

        public static string Solve(string color)
        {
            switch (color)
            {
                case "Blue":
                    return "Release at four.";
                case "White":
                    return "Release at one.";
                case "Yellow":
                    return "Release at five.";
                default:
                    return "Release at one.";
            }
        }
    }
}