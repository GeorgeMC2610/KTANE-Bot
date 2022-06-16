# Keep Talking and Nobody Explodes BOT

Using C# default [Speech Recognition Engine](https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition.speechrecognitionengine?view=netframework-4.8) and C# [Speech Synthesis](https://docs.microsoft.com/en-us/dotnet/api/system.speech.synthesis.speechsynthesizer?view=netframework-4.8), I made a bot that emulates the manual of the video game Keep Talking and Nobody Explodes.
All modules can be solved using the bot, using the right key-words.

### Before you give it a go, make sure that...

+ **You are using your default microphone.** The program automatically sets the input to your default audio input device.
+ **You have installed voices for English.** Microsoft David and Microsoft Zira should appear by default. If they don't, then you might not have installed voice for English.
+ **You are using headphones.** You can use speakers too, but you might want to lower the volume, as it might interfere with the audio inputs (the bot can hear itself).
+ **You try different english accents.** This is not my fault. I rarely struggle with my Greek accent, but I haven't tried other accents.

<br>
You can use the following keywords as they're explained below, in order to disarm any bomb. 
<br> <br>

## Initializing the Bomb's Properties

#### Key-Word: BOMB CHECK

**Before defusing any module, you must first tell the bot the bomb properties, by saying "Bomb Check"**. Not all, though. Only the ones that matter. The properties that matter are:

+ A lit FRK indicator.
+ A lit CAR indicator.
+ The presence of a parallel port.
+ The presence of any vowel in the serial number.
+ The last digit of the serial number (even/odd).
+ The number of batteries.

The above affect several modules. So after you say bomb check, say the following:

+ Freak <yes|true|no|false>. (E.g., say "Freak yes" if there is a lit FRK label)
+ Car <yes|true|no|false>.
+ Port <yes|true|no|false>.
+ Vowel <yes|true|no|false>.
+ Digit <even|odd>.
+ Batteries <0-6|none|more than two>. (If there are more than two batteries, it's better to say "Batteries more than two")

You can also skip this section if you want, by clicking the **"Random Bomb"** button. This button will initialize a bomb with completely random properties. This is likely to be useful, if you want to disarm a module that has nothing to do with the bomb's properties (E.g. The Symbols, or Memory).

<br>

## The Button

#### Key-Word: DEFUSE BUTTON

The button is fairily simple. You just state the button color and then the label as it is written. If the BOT tells you to hold the button until the stripe strikes, you recite the color of the stripe and then the word "stripe".

E.g.: **Red Detonate** or **White Stripe**.

<br>

## Simple Wires

#### Key-Word: DEFUSE WIRES

The simple wires are also simple. You state just the color of the wire, then you wait until the bot repeats the color and says "next".
Unless the number of the wires is exactly six, you have to say "done" when there are no wires left. Then the bot will tell you the index of the wire you have to cut.

E.g. **Cut the n-th wire** or **Cut the last wire**.

<br>

## Wire Sequence

#### Key-Word: DEFUSE SEQUENCE

This module is also simple. You state the color of the wire and then the letter that it's connected to. The letter can either be A, B or C. Instead of A, B or C, you must say Alpha, Bravo or Charlie accoordingly.

E.g.: **Blue Bravo** or **Black Charlie**.

<br>

## Complicated Wires

#### Key-Word: DEFUSE COMPLICATED

This module is really complicated for a human to solve, but fairily easy for a BOT. After stating the wire's colors, you must say if there is any indicator afterwards (a light or a star). If there is none, you just say "nothing". The BOT will respond with a "yes" or a "no", depending wether the wire must be cut or not.

E.g.: **"Red and White, Nothing" or "Blue, Star" or "Blue and White, Star and Light"**.

<br>

## Memory

#### Key-Word: DEFUSE MEMORY

The Memory module is really straight forward. After saying the word "Numbers", state all five numbers that you see, starting from the display and then going sequentially left-to-right. Be sure to be moderately fast and clear as you state the numbers. A small misspell and it might not understand you. If it doesn't understand you, it will tell you to repeat the numbers that you see.

E.g.: **"Numbers one, four, one, two, three".**

After stating the numbers, the bot will tell you which button to press.

E.g.: **"Press three".**

<br>

## Simon Says

#### Key-Word: DEFUSE SIMON

Some of you don't even need this, especially when there's a vowel. Each round, the ***only*** thing you have to say is **the color that flashes** ***last***.
E.g.: "**red.**"

Then, the bot will respond with the rest of the colors that you have to press.
E.g.: "**press blue, red, red, yellow**."

If the bomb has one or more strikes, at any time, you can say "Strikes \<number of strikes>".
E.g.: "**Strikes 1**."
  
<br>
  
## Morse

#### Key-Word: DEFUSE MORSE

You have to be patient in this one. Starting from the first letter, the bot will sequentially ask you to state the morse code with zeros and ones (**0 is the dot and 1 is the dash**). Once you state one letter, you will state the next one. Usually, you find the word in the third letter.

E.g.: **"zero zero zero"**. (Accoording to Morse Code this is the letter 's')

After the BOT has identified the word, it will tell you which word it is and at what frequency you tune the radio to.

<br>

## The Maze

#### Key-Word: DEFUSE MAZE

I promise this is easy. The only thing you have to recite, are the coordinates. First, state the coordinates of any of the green circles. Then the white square and then the triangle.

E.g. "**one two**".

After that, using BFS, the BOT will tell you which path to follow (Down, Up, etc.). If the bot tells you that there is no path found, then something must be wrong with the circle coordinates.

### How to specify the coordinates:

![sample maze](https://raw.githubusercontent.com/cpuSonicatt/KTaNE-Bomb-Expert/HEAD/resources/mazeexample.png)

First is line **THEN** column. For example:

+ the first circle's coordinates are **1, 5**.
+ the second circle's coordinates are **5, 3**.
+ the white square's coordinates are **4, 6**.
+ the red triangle's coordinates are **5, 1**.

<br>

## The Password

#### Key-Word: DEFUSE PASSWORD

For each column, tell the bot the letter you see, either by saying the letter itself (I do not recommend this), or by using the [military alphabet](https://www.wikiwand.com/en/NATO_phonetic_alphabet#:~:text=The%2026%20code%20words%20are,%2Dray%2C%20Yankee%2C%20Zulu.). For each letter, wait until the BOT repeats it and says "next".

E.g.: "**Yankee**" or "**Y**".

If the bot finds at most five possible words, it will tell you to try one of them. Otherwise, after the first column, the bot will ask for the letters from the next column.

<br>

## Keypad

#### Key-Word: DEFUSE SYMBOLS

Just like Simon Says, there's also a group of people that just memorize the symbols. But if you can't memorize, you have to tell the bot sequentially the symbols you see. **After you state a symbol, wait until the bot repeats it and says "next".** Thereafter naming all four symbols, the bot should say the right order for you to press.

I've named the symbols after what I thought was more obvious. You can take a look at the [Text Document](https://github.com/GeorgeMC2610/KTANE-Bot/blob/master/bin/Debug/Symbols.txt) the program reads to identify the words. Otherwise here are the names:

![symbols](https://github.com/GeorgeMC2610/KTANE-Bot/blob/master/SYMBOLS.jpg)

<br>

## Who's On First

#### Key-Word: DEFUSE WHO IS ON FIRST

I've tried to make the *Who's on First* module as simple as I possibly could. You shall say all words as they're written except:

+ [when the display is blank] → **"it is blank"**
+ READ → **"read noun"**
+ RED → "**red color"**
+ REED → **"ar ee ee dee"**
+ LEED → **"el ee ee dee"**
+ YOUR → **"your pronoun"**
+ YOU'RE → **"you're apostrophe"**
+ UR → **"u r letters"**
+ THEY'RE → **"they're apostrophe"**
+ THEIR → **"their pronoun"**
+ C → **"c letter"**
+ CEE → **"charlie echo echo"**
+ UHHH → **"u h h h" (spell the letters)**
+ U → **"u letter"**
+ UH HUH → **"u h space h u h"**
+ UH UH → **"u h space u h"**
+ WHAT? → **"what questionmark"**

<br>

## The Knob

#### Key-Word: DEFUSE KNOB

The knob is by far the easiest one. Using "0" for unlit and "1" for lit, spell the six lights that you see (three on the upper left and three on lower left).

For example, for the following knob, you should say: "**0 1 1 space 1 1 1**". 

![knob](https://static.wikia.nocookie.net/ktane/images/9/95/NeedyKnob_Manual2_down2.png/revision/latest/scale-to-width-down/285?cb=20201002195717)

The BOT will respond with the corresponding position the indicator should be turned.

<br><br>

#

### Thanks for reading. Have fun!
