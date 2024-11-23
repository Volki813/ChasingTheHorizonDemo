-> main

=== main ===
Should Alan Forte hop on Atelier Ayesha? #speaker:Roland #portrait:roland_smiling #facing:face_right
    + [hell yeah]
        -> ayesha
    + [nah]
        -> poke

=== poke ===
Which Pokemon do you choose? #portrait:roland_neutral #facing:face_left
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulba")
    + [Squirtle]
        -> chosen("Squirt")

=== chosen(pokemon) ===
You chose {pokemon}! #facing:face_right
-> END

=== ayesha ===
HELL YEAH #speaker:Simon #portrait:simon_smiling
-> END