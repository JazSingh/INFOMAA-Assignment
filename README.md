# INFOMAA-Assignment

##############
### README ###
##############

When you run this program, it will execute all experiments we have run at once.
Since we have already done this, we would like to refer to the corresponding logs. 


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
   
