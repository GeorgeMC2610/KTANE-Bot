# Keep Talking and Nobody Explodes BOT

Using C# default speech recognition engine and C# speech synthesis, I made a bot that emulates the manual of Keep Talking and Nobody Explodes video game.
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

#### Key Word: DEFUSE BUTTON

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
