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

## The Button

The button is fairily simple. You just state the button color and then the label as it is written. If the stripe strikes, you recite the color of the stripe and then the word "stripe".

E.g.: **Red Detonate** or **White Stripe**.

<br>

## Simple Wires

The simple wires are also simple. You state just the color of the wire, then you wait until the bot repeats the color and says "next".
Unless the number of the wires is exactly six, you have to say "done" when there are no wires left. Then the bot will tell you the index of the wire you have to cut.
E.g. **Cut the n-th wire** or **Cut the last wire**.

<br>
