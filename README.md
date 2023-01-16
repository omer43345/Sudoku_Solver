## Sudoku Solver

Sudoku Solver
My sudoku solver supports boards from size 1*1 to 64*64.
The user can enter the boards through the console or text file.
Puzzles are represented by a string of chars, when each char ascii represent his number.


## Menu
When you running the aplication a menu will appear.
```
Welcome to Sudoku Solver by Omer Hadad
How do you want to write a sudoku, in the console you want to use a file?
1. Console
2. File
3. Exit
Your choice:
```
You can choose 1 to enter the sudoku string through the console, 2 to enter it through a text file and 3 to exit the program.
```
Your choice: 1
Write the sudoku in the console. Use 0 for empty fields.
```
When entered 1 you will need to enter the board as the follwing:
```bash
200050106030000080010902000004001500080009000950000060049037000703010005001000600
```

```
Your choice: 2
Write the path to the file:
```
When entered 2 you will need to enter the full path to the text file that has the sudoku string as mentiond above.
## Representing Solution

To the console no matter what always the solution will be shown in two ways.


First one is in the way we get the board:
```
  298354176435176982617982354374861529186529743952743861549637218763218495821495637
```

Second one is in more visual way like a sudoku board:
```
| 2  9  8 | 3  5  4 | 1  7  6 |
| 4  3  5 | 1  7  6 | 9  8  2 |
| 6  1  7 | 9  8  2 | 3  5  4 |
-------------------------------
| 3  7  4 | 8  6  1 | 5  2  9 |
| 1  8  6 | 5  2  9 | 7  4  3 |
| 9  5  2 | 7  4  3 | 8  6  1 |
-------------------------------
| 5  4  9 | 6  3  7 | 2  1  8 |
| 7  6  3 | 2  1  8 | 4  9  5 |
| 8  2  1 | 4  9  5 | 6  3  7 |
```
## Writing Solution To Text File

When you decided to enter sudoku through a text file the sudoku solution will be shown in the console and also in a text file that contains all the solutions of the boards from text files in a string type.

The text file that holding the solutions called `Sudoku_Solutions.txt` that can be found in `Solutions` folder.
## Algorithm 
My Sudoku Solver based on the Dancing Links algorithm. This algorithm create an exact cover problem from the sudoku grid and solve it.


## How It Works?

The main idea of the algorithm is to take the sudoku grid and convert it to a sparse matrix when every row represents a possible value for a cell.
Than we converting the matrix to quadruple chained list and using the search algorithm to solve the exact cover problem.
The sparse matrix contains `size * size * number_of_constraints` columns and `size * size * size` rows for each possible value of the cell, the number of constraints in sudoku is 4 (cell, row, column,box).

In my sudoku solver Instead of using the sparse matrix, to save memory I only saved for every row that is a possible value the 4 column indexes of the 1's. By doing that I saved a lot of memory allocation and time of building the sparse matrix and the quadruple chained list.

The reason this algorithm is so fast is because the cover and uncover functions that gives you the option to remove and to return columns from the chained list very fast.

[More Information On The algorithm](https://www.ocf.berkeley.edu/~jchu/publicportal/sudoku/sudoku.paper.html)
## Tests

My sudoku solver contains number of tests for every board from 9 * 9 to 64 * 64, it also contains tests to the validation of the given board.
My project contains xunit tests,
You can run all the tests from the ` SudokuTester` class that you can found in  ` SudokuTests` folder.
