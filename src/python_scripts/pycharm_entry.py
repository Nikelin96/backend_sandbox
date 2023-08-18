class Point:
    def Move(self):
        print("mov3")

    def Draw(self):
        print("draw")



p1 = Point()
p1.Draw()
p1.w = "asd"
print(p1.w)
