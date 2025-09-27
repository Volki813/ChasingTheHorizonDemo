INCLUDE External Functions.ink
INCLUDE Actor Variables.ink
// includes for storing External Functions and Variables so they can
// be used across all dialogues

-> main

=== main ===
~ CurrentSpeaker("start", "roland")
~ PlaceActor("start", "roland", "far left")
~ SetPortrait("start", "roland", "roland_smiling")
~ SetFacingDirection("start", "roland", "right", false)
Bluesky starts banning child porn and rape porn.
    + [make this about trans people for some reason.]
        -> manchild
    + [gas]
        -> good
    + [porn is misogyny]
        -> misogyny

=== manchild ===
~ SetPortrait("start", "roland", "roland_tense")
How dare they do this! What did trans people do to them?

~ CurrentSpeaker("start", "simon")
~ PlaceActor("start", "simon", "near right")
~ SetPortrait("start", "simon", "simon_neutral")
~ SetFacingDirection("start", "simon", "left", false)
What is even the correlation?

~ CurrentSpeaker("start", "roland")
Rape and pedophilia are part of trans culture, obviously.

~ CurrentSpeaker("start", "simon")
So what you are saying is conservatives have a point about the lgbtq community being filled with sexual predators.

~ CurrentSpeaker("start", "roland")
Have you no empathy? Trans people's rights are being stripped away and here you are malicious ship of theseus my argument?

~ CurrentSpeaker("start", "simon")
The rules are explicitly about rape scenes. You making this about trans people says more about you than it does about me.

~ CurrentSpeaker("start", "roland")
Stop painting me like a super villain.

~ CurrentSpeaker("start", "simon")
...
I'm asexual by the wa-

~CurrentSpeaker("start", "roland")
~SetPortrait("start", "roland", "roland_angry")
<b><color=\#ff0000ff>YOU FASCIST PURITAN</color></b>
->END

=== good ===
That's fire! One step closer to getting rid of the constant dehumanization of oppressed classes is awesome and bluesky seems like the kind of place that won't misuse this rule.
-> END


=== misogyny ===
~SetPortrait("start", "roland", "roland_tense")
Porn is literally filmed rape. Most actors are coerced or groomed into thinking it's their only choice or that it's normal.
Thus, they enter an industry that exploits them with no regard of their age, ethnicity, mental health, etc. whatsoever.
14 year olds get kidnapped, gang-raped and filmed and then that gets uploaded to the biggest porn site for billions to see. Guess what they did when the vicitm found out about it and wanted it removed?
Exactly. Fucking nothing because m*n are a parasitic utterly useless stain in society and hold us back in our development.
"It's empowering." Brother do you think slavery was empowering too? Literally bodies are the main attraction of the market in both cases.
There are multiple studies about how women let men do things to them during sex that they find uncomfortable but don't resist because they think that is normal and how it's supposed to be.
Not only that, but the dehumanizing acts in porn are so badly normalized that the m*n themselves don't even realize you aren't supposed to hurt your partner during sex and that it is meant to be a pleasant act.
Multiple studies also confirm that most slaves (slave referring to sex worker as sex work is just slavery) in this industry suffer from depression, ptsd, addictions, anxiety disorders, etc.
We need to criminalize people who purchase these slaves as well as the pimps.
->END

~ CurrentSpeaker("start", "roland")
~ PlaceActor("start", "roland", "far left")
~ SetPortrait("start", "roland", "roland_smiling")
~ SetFacingDirection("start", "roland", "right", false)
~ PlaySound("end", "SFX Boomy")
~ PlayMusic("start", "At Home")
~ EditFontSize("end", 20, "speaker")
Should Alan Forte hop on Atelier Ayesha?
    + [hell yeah]
        -> ayesha
    + [external functions]
        -> external
    + [gogeta]
        -> gogeta

=== external ===
~ CurrentSpeaker("start", "roland")
~ SetPortrait("start", "roland", "roland_neutral")
~ SetFacingDirection("start", "roland", "left", false)
~ MoveActor("start", "roland", "near left", 0.5)
~ SetFacingDirection("end", "roland", "right", true)
~ FadeActor("end", "roland", 3)
Test external functions? 
    + [Yeah]
        -> chosen("Yeahh")
    + [Yup]
        -> chosen("Yupp")
    + [True]
        -> chosen("Truee")

=== chosen(choice) ===
~ RemoveActor("start", "roland")
You chose {choice}!


~ PlaceActor("start", "simon", "far left")
~ PlaceActor("start", "roland", "far left")
~ SetPortrait("start", "simon", "simon_smiling")
Hey!

~ PlaceActor("start", "roland", "far left")
~ PlaceActor("start", "simon", "far left")
Hey!

~ PlaceActor("start", "simon", "far right")
~ PlaceActor("start", "roland", "far right")
Hey!

~ PlaceActor("start", "roland", "far right")
~ PlaceActor("start", "simon", "far right")
Hey!

~ MoveActor("start", "simon", "far left", 2.0)
~ MoveActor("start", "roland", "far left", 1.0)
Hey!

~ MoveActor("start", "roland", "far left", 1.0)
~ MoveActor("start", "simon", "far left", 2.0)
Hey!

~ MoveActor("start", "simon", "far right", 3.0)
~ MoveActor("start", "roland", "far right", 4.0)
Hey!

~ MoveActor("start", "roland", "far right", 1.0)
~ MoveActor("start", "simon", "far right", 4.0)
Hey!

~ PlaceActor("start", "roland", "center left")
Hey!

~ MoveActor("start", "simon", "center left", 2.0)
Hey!

~ MoveActor("start", "roland", "near left", 2.0)
~ MoveActor("start", "simon", "center left", 2.0)
Hey!

~ MoveActor("end", "simon", "near left", 2.0)
Hey!

{rolandDead: This is written if Roland is dead|This is written if Roland is alive}
{- rolandDead: 
~ MoveActor("start", "roland", "far left", 1.0)
    this is executed if Roland is dead
}

~ rolandDead = false
Roland's death status has now been set to {rolandDead}
{rolandDead: This is written if Roland is dead|This is written if Roland is alive}
{- rolandDead: 
~ MoveActor("start", "roland", "far left", 1.0)
    this is executed if Roland is dead
}


~ MoveActor("start", "roland", "far right", 1.0)
VIDEO GAMES!!!
-> END

=== ayesha ===
~ CurrentSpeaker("start", "simon")
~ SetPortrait("start", "simon", "simon_smiling")
~ PlaceActor("start", "roland", "near left")
~ PlaceActor("start", "simon", "near right")
~ SetFacingDirection("start", "simon", "left", true)
~ EditFontSize("start", -1, "speaker")
HELL YEAH

~ MoveActor("start", "roland", "far right", 1.0)
~ MoveActor("end", "simon", "far left", 2.0)
Based and trueeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee

~ CurrentSpeaker("start", "roland")
~ MoveActor("start", "simon", "near left", 1.0)
~ MoveActor("start", "roland", "center right", 3.0)
Very true

~ SetFacingDirection("start", "roland", "left", true)
~ SetFacingDirection("start", "simon", "right", true)
Based and true indeed
-> END

=== gogeta ===
~ CurrentSpeaker("start", "roland")
~ SetPortrait("start", "roland", "roland_tense")
~ PlayMusic("start", "stop")
~ EditFontSize("start", -1, "speaker")
~ EditFontSize("start", 50, "dialogue")
<color=\#0000ffff>In no conceivable manner is it okay to suggest or engage</color> in any sexual conduct with a minor.

~ EditFontSize("start", -1, "dialogue")
It takes 25 years for the human mind to fully mature.
The age of consent in most countries is built around an average period in which the growing mind is capable of handling sexual realtionships, ranging anywhere from 16 to 18 years old on average.
Any younger than this, and the minor is exposing themselves to various physical, psychological, emotional and neurochemical damages that could severely impact their ability to mature as a respectable adult.
No matter how low the age of consent is in your country or how low you think the age of consent should be, a sexual relationship where one partner is far older than the other is incredibly one-sided.
Sexual relationships are best handled when you and your partner are well-informed legal adults that have their lives together.
Children do not possess the wisdom, in-depth education, or emotional, psychological, neurochemical and <b>PHYSICAL</b> maturity to responsibly handle <b>OR</b> consent to sexual relationships.
They do <b>not</b> know better,
They are <b>naive</b> and <b>vulnerable</b>,

~ SetPortrait("start", "roland", "roland_angry")
<b><color=\#ff0000ff>AND THEY ARE NOT TO BE HARMED!</color></b>
-> END