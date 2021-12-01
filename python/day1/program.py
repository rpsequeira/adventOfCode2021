import sys
import os
import math

SCRIPT_DIR = os.path.dirname(os.path.abspath(__file__))
sys.path.append(os.path.dirname(SCRIPT_DIR + "/../"))

from utils import utils

def main():
    input = loadData()
    part1(input)
    part2(input)    

def loadData():
    input = utils.readAllLines()
    for i in range(0, len(input)):
        input[i] = int(input[i])
    return input

def part1(input):
    previous = 1000000
    counter = 0
    for x in input:
        if x > previous:
            counter += 1
        previous = x
    print("AoC part 1: " + str(counter))

def part2(input):
    previous = 1000000
    counter = 0
    for i in range(0, len(input)-2):
        x = input[i] + input[i+1] + input[i+2]
        if x > previous:
            counter += 1
        previous = x
    print("AoC part 2: " + str(counter))

if __name__ == "__main__":
    main()