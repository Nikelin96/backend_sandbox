import cv2
import numpy as np
import os
import matplotlib
matplotlib.use('TkAgg') 
from matplotlib import pyplot as matplot
import matplotlib.image as mpimg
matplot.rcParams['figure.figsize'] = [15, 5]

# absolute_path = os.path.join(os.getcwd(), 'BasicAI Server', 'Res', 'DSC_1902.JPG');
# absolute_path = os.path.join(os.getcwd())
# print(absolute_path)

img = mpimg.imread('../data/IMG_20210403_113701.jpg')
matplot.title('Simple RGB Image')
matplot.axis('off')  # Turn off axis numbers and ticks
matplot.imshow(img)
matplot.show()