# V-Task-Manual

# What does this code do:

This program will create a copy of the folder you specify on the Command line,  
copy all the files from the source folder to the replica folder,  
delete all replica folder files that no longer exist in the source folder,  
record all actions in a log file, and  
perform the task periodically.  
The interval of time is of your choice, and it is defined in minutes.  

# A few observations:

If the destination folder does not exist, it will be created.  
If the log file does not exist, a new log file will be created.  
If the user inserts the path with quotation marks, the program will be terminated.  
If the user does not enter a number to set the time, the program will be terminated.  
If the user tries to repeat the path used, the program will be terminated.



