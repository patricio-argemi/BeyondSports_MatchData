# BeyondSports_MatchData
Assignment for BeyondSports interview process:
Some notes from the process, things I would change, and observations in general.


Class Specific Notes:
===================

----------------------------------

RoleType enum:
-there are two Unknown types because I didn't know what were those id's, I suppose they must be either Linemans or Cameras, but I didn't pay much attention to it, and just rendered as additional referees.

----------------------------------

Director class:
-I would add a sort of "camera manager" so the config wouldn't require the cameras to be added one by one to the Cameras array. This could be a simple gameobject with the cameras as children, which I did in the project hierarchy but I didn't code.

----------------------------------

Movement class:
-I only created this one because both the actors and the ball had to be moved in the same fashion, but I don't like this approach too much and I would have made the custom movement scripts for each component individually, thinking scalable-wise (if such term even exists :) ).
-The position in each axis multiplied by 0.1f for the Vector3 (yes, is disgusting) it's just to keep things simple in this demo and deal with much smaller numbers.

----------------------------------

MatchDataController class:
This one is a mess, I made it all in a single class due to a lack of time to think about a better solution, but roughly:

-I would separate the data processing into a standalone library.
-There are no strong/robust validations whatsoever, which I would of course add (collection boundaries, possible null references, parsing attempts).
-I'm not sure referencing prefabs and prefab parents to instantiate them is a good practise to be honest, I just felt it was the best option I had in the time I had.
-Same as with validations I would watch for optimization. Such as the usage of something like a HashSet for lookup, or processing the data in batches instead of doing the entire thing at once, and so on.
-This is the first time I dealt with external files in Unity, so I just searched for the file by exposing an inspector variable for the file name. I would add a .config file for the dll to store such things.
-I left a comment in the loop "else" block at the bottom stating that I tried to retrieve an existing GameObject component but it was returning null, so I had to re-search for it and use a new instance with which it worked, and that doesn't make any sense. If you can help me find an answer on that, would be nice because I'm puzzled. Maybe I just need an additional pair of eyes and I'm missing some most-stupid detail.

----------------------------------

General Notes:
=======================

----------------------------------

-It wasn't clear for me at the beginning what the data was from, I just counted the amount of different id's and figured it was football. 
-Same with the Clicker Flags in the ball data. I still don't have a clue about them so I didn't even process those
-Worth to say, it was a pretty fun little project :)

----------------------------------
