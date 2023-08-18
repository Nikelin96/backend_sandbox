from automation.integration import Task

incomingCommand = input("Please enter your command: ")

task = Task(incomingCommand)
print(task)