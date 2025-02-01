EXTERNAL CurrentSpeaker(timing, actorName)
EXTERNAL SetPortrait(timing, actorName, portrait)
EXTERNAL SetFacingDirection(timing, actorName, direction, withBounce)
EXTERNAL PlaySound(timing, soundName)
EXTERNAL PlayMusic(timing, musicName)
EXTERNAL EditFontSize(timing, fontSize, speakerOrDialogue)

-> main

=== main ===
~ CurrentSpeaker("start", "roland")
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
~ SetFacingDirection("end", "roland", "right", true)
Which Pokemon do you choose? 
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulba")
    + [Squirtle]
        -> chosen("Squirt")

=== chosen(pokemon) ===
~ SetFacingDirection("start", "roland", "right", true)
You chose {pokemon}!
-> END

=== ayesha ===
~ CurrentSpeaker("start", "simon")
~ SetPortrait("start", "simon", "simon_smiling")
~ SetFacingDirection("start", "simon", "left", true)
HELL YEAH
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