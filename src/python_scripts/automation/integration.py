class Task:
    def __init__(self, taskInvoker, fileName):
        self.taskInvoker = taskInvoker
        self.fileName = fileName

    def ReadFromFile(self):
        file = open(self.fileName, "r")
        return file.read()

    def WriteToFile(self, textToWrite):
        file = open(self.fileName, "a+")
        file.write(textToWrite + "\n")

    def Move(self):
        print("mov3")

    def Draw(self):
        print("draw")
        return "draw"
