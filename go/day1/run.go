package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Println("Hello World!")
	f, err := os.Open("input.txt")

	if err != nil {
		log.Fatal(err)
	}
	defer f.Close()

	reader := bufio.NewReader(f)

	file := []int{}
	for {
		line, err := reader.ReadString('\n')
		if err != nil {
			break
		}
		number, err := strconv.Atoi(strings.TrimSuffix(line, "\n"))
		if err != nil {
			log.Fatal(err)
		}
		file = append(file, number)
	}

	previous := -1
	counter := 0
	for i := range file {
		if previous != -1 && previous < file[i] {
			counter++
		}

		previous = file[i]
	}
	fmt.Println(counter)

	previous = -1
	counter = 0
	for i := 0; i < len(file)-2; i++ {
		sum := file[i] + file[i+1] + file[i+2]
		if previous != -1 && previous < sum {
			counter++
		}

		previous = sum
	}
	fmt.Println(counter)

}
