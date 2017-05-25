# INFOMAA-Assignment

##############
### README ###
##############

When you run this program, it will execute all experiments we have run at once.
Since we have already done this, we would like to refer to the corresponding logs (See INFOMAA-Assignment/EXPERIMENT_RESULTS). 

The variants which we have tested against our base line (meaning that we use the base line parameter values for the other parameters):
W (Width) = {50, 100, 250}
H (Height) = {500, 1000}
N (# of players) = {10, 50 65, 75, 100, 150, 250, 500}
k (# of actions) = {2, 4, 6, 8, 10}
C (Collision radius) = {3, 5, 8}
d (Speed) = {3, 5, 8}
R_1 (Reward R_1) = {1, 3, 5, 10}
R_2 (Reward R_2) = {0, -1, -3, -5, -10}
e (Epsilon) = {0, 1000, 100, 10, 2, 1}

Each variant is tested against the following default setup (our base line):
W = 250,
H = 250,
N = 100,
k = 6,
C = 3,
R_1 = 1,
R_2 = -10,
e = 0.01,
r = 5

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
