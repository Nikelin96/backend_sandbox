command = ""
isStarted = False
while (True):
    command = input('> ')
    if command.find('help') > -1:
        text = '''
start - to start the car
stop - to stop the car
quit - to exit
        '''
        print(text)
    elif command.find('start') > -1:
        if isStarted == True:
            print("car is already started!")
        else:
            print("started car")
        isStarted = True
    elif command.find('stop') > -1:
        if isStarted == False:
            print("car is already stopped!")
        else:
            print("stopped car")
        isStarted = False
    elif command.find('quit') > -1:
        print("quitting the car..")
    else:
        print("I don't understand that")
    previousCommand = command
