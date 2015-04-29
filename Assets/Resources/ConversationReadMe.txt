To format the script:

In the tojson text file:

Each character's name should be written in quotation marks("Wife") The name in quotation marks needs to match the character name on the character.
For each line, the speaking character's line should be written on one line, then the player's response options on the next seperated by two
colons(::). On the next line should be the line numbers each response go to separated by commas. Lines are counted up starting from zero for
each character.

To end the conversation, use a 'to line' number of 0.

To advance the plot use a 'to line' number of 100. The script will convert to JSon when you start a conversation,
however you may need to restart the game or click or open the script text file before the changes go through.

Advancing the plot moves the characters to their next set of diologue. Each characters start state array needs to have the line number they 
start with for each section of the plot. Basic characters can have the same starting point but more important characters should have multiple
things to say including moving the plot forwards.