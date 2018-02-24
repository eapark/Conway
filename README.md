# Conway
Conway's Game of Life implemented in Unity2D and 3D

### Decisions made
#### 2D
There are two classes, Life and LifeGame. LifeGame is what runs the game and is attached to the GameManager object.

I put the functions that update Life objects in a coroutine instead of Update() because I wanted to be able to control how often the Life objects got updated (ie. not every frame). In LifeGame, there are two main functions: UpdateLives and ApplyLivesUpdate. The former updates each Life object's nextState, and the latter updates a Life object's currentState to nextState.

I initially made LifeGame have a 2D array of LifeCube prefabs. But the code became wordy when I had to do GetComponent<Life>() each time. So I changed it so that the array would have Life objects instead.

The most important variables in the Life class are the x,y position and the LifeGame object. When instantiating a Life object from LifeGame class, the LifeGame object is passed in so that each Life can have access to the lifeBoard 2D array. Using the x,y position and the lifeBoard array, we can calculate how many neighbors are alive. I used a nested for loop to get the neighbors' currentState; the lines of code used may not necessarily be shorter than hard-coding the 8 neighbors' indices, but it helped simplify the code when I transitioned from 2D to 3D (didn't have to hard-code 26 neighbors!).

There's also a LifeGamePresets class. This provides three possible patterns that the game can start off with: blinker, block, and random. I got the patterns and the names from the Wikipedia page. The presets are kept the same for the 3D version to observe differences in patterns.

#### 3D
I wrote two versions for the 3D game. The versions can be toggled through the LifeGame object by abling/disabling the scripts.

The 'Life Game' version is similar to the 2D implementation, just with a 3D array of LifeCube objects. The logic for getting the number of alive neighbors is also the same; checking the number of alive neighbors through the LifeGame class's lifeBoard array.

The second version, 'Life Game Collider', uses the Physics.OverlapBox() function to check the number of alive neighbors. I make sure that the number doesn't count the LifeCube we're checking from using GameObject.ReferenceEquals(). The LifeCube collider is also tagged as "Life" to make sure we only check other Life Cubes. I made this version because I wanted to utilize Unity's functionality more, and using colliders makes the code simpler. However, this operation may be costly if there are a lot of Life blocks. In contrast, checking arrays is relatively cheaper in terms of resources, which is why I decided to keep both versions. In addition, keeping an array makes it simpler to call UpdateLives() and ApplyLivesUpdate().

### Possible improvements
One that could have been done is to make the life sprites scale relative to the screen size so that the blue background wouldn't show up or so that less of the background would show. The lack of scaling also makes the game board look cut off if the x and y dimensions are high.
