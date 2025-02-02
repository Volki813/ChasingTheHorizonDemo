EXTERNAL CurrentSpeaker(timing, actorName)
EXTERNAL SetPortrait(timing, actorName, portrait)
EXTERNAL SetFacingDirection(timing, actorName, direction, withBounce)
EXTERNAL PlaySound(timing, soundName)
EXTERNAL PlayMusic(timing, musicName)
EXTERNAL EditFontSize(timing, fontSize, speakerOrDialogue)
EXTERNAL PlaceActor(timng, actorName, position)
EXTERNAL MoveActor(timing, actorname, destination, faceDestination)
EXTERNAL RemoveActor(timing, actorName)

// add parameters to move actor: 
// endUpFacingRight: face right after moving if true
// timeToComplete

// scaleX, scaleY and scaleZ parameters for setPortrait

-> main

=== main ===
~ CurrentSpeaker("start", "roland")
~ SetPortrait("start", "roland", "roland_smiling")
~ PlaceActor("start", "roland", "far left")
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
~ SetPortrait("start", "roland", "roland_angry")
~ PlaceActor("end", "roland", "near left")
VIDEO GAMES!!!
~ MoveActor("start", "roland", "far right", true)
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
~ MoveActor("start", "roland", "far right", true)
~ MoveActor("end", "simon", "far left", true)
Based and true
~ CurrentSpeaker("start", "roland")
~ MoveActor("start", "roland", "center right", true)
~ MoveActor("start", "simon", "near left", false)
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