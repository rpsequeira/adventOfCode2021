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
    res = []
    for i in range(0, len(input)):
        cmd = input[i].split()
        res.append((cmd[0],int(cmd[1])))
    return res

def part1(input):
    x = 0
    y = 0
    for cmd in input:
        match cmd[0]:
            case "forward":
                x += cmd[1]
            case "up":
                y -= cmd[1]
            case "down":
                y += cmd[1]

    print("AoC part 1: " + str(x*y))

def part2(input):
    x = 0
    y = 0
    aim = 0
    for cmd in input:
        match cmd[0]:
            case "forward":
                x += cmd[1]
                y += cmd[1]*aim
            case "up":
                aim -= cmd[1]
            case "down":
                aim += cmd[1]

    print("AoC part 1: " + str(x*y))

if __name__ == "__main__":
    main()