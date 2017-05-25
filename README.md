# INFOMAA-Assignment

##############
### README ###
##############

When you run this program, it will execute all experiments we have run at once.
Since we have already done this, we would like to refer to the corresponding logs (See INFOMAA-Assignment/EXPERIMENT_RESULTS). 

The variants which we have tested against our base line (meaning that we use the base line parameter values for the other parameters):</br>
W (Width) = {50, 100, 250} </br>
H (Height) = {500, 1000}</br>
N (# of players) = {10, 50 65, 75, 100, 150, 250, 500}</br>
k (# of actions) = {2, 4, 6, 8, 10}</br>
C (Collision radius) = {3, 5, 8}</br>
d (Speed) = {3, 5, 8}</br>
R_1 (Reward R_1) = {1, 3, 5, 10}</br>
R_2 (Reward R_2) = {0, -1, -3, -5, -10}</br>
e (Epsilon) = {0, 1000, 100, 10, 2, 1}</br>

Each variant is tested against the following default setup (our base line):</br>
W = 250,</br>
H = 250,</br>
N = 100,</br>
k = 6,</br>
C = 3,</br>
R_1 = 1,</br>
R_2 = -10,</br>
e = 0.01,</br>
r = 5</br>

General information about the setup of this solution:

-  See Program.cs to see the overall setup of our experiments. You can see what variations for every parameter we used to check. 
   Because of statistical reasons, we repeat every experiment 25 times and log the average of the results. The logs will be written as a .csv file to your output folder (bin/Debug if you compile with debug settings).
-  Game.cs contains the basic setup of the game. Here you can see how we simulate the changes in every game step. Also, the way how we compute a collision resides in this file.
-  Player.cs is the place-to-be when it comes to determining the next action for a certain player.
-  Torus.cs is the file in which we compute the next position for a given player.

-  After simulation, we log the average payoff per action in every timestep to a scores.csv file. 
   In NumActionPlayed.csv, you can see the # of times every action is played for each timestep.
   These two files are being written after every experiment (x25, since we repeat an experiment).
   
-  Also, there are a couple of summary.csv files being written, which give a summarized log over all the variants.
