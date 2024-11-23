-> main

=== main ===
Should Alan Forte hop on Atelier Ayesha? #speaker:Roland #portrait:roland_smiling
    + [hell yeah]
        -> ayesha
    + [nah]
        -> poke

=== poke ===
Which Pokemon do you choose? #portrait:roland_neutral
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulba")
    + [Squirtle]
        -> chosen("Squirt")

=== chosen(pokemon) ===
You chose {pokemon}! 
-> END

=== ayesha ===
HELL YEAH #speaker:Simon #portrait:simon_smiling
-> END