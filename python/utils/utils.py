def readAllLines(path_to_file = "input.txt"):
    with open(path_to_file) as f:
        contents = f.readlines()
    return contents