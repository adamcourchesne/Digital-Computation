using System;
using static System.Console;
using static System.Array;

namespace Bme121
{
    static class Program
    {
        static string[ ] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
        

        
        static double cellMarkProb = 0.2;
        static Random rGen = new Random( );
       
        
        

        static void Main( )
        {
			Write ( "Please insert desired board size between 1 and 12: " );
			int boardSize = int.Parse(ReadLine( ) );
			
			bool [ , ] userBoard = new bool [ boardSize, boardSize ];
			bool [ , ] hiddenBoard = new bool [ boardSize, boardSize ];
			int [ ] rowSums = new int [ boardSize ];
			int [ ] colSums = new int [ boardSize ];
			int [ ] rowSumsUser = new int [ boardSize ];
			int [ ] colSumsUser = new int [ boardSize ];
		
            for ( int i = 0; i < boardSize; i ++ ) //determines row and column values for the hidden board
            {
				for ( int j = 0; j < boardSize; j ++ )
				{
					if(rGen.NextDouble( ) < cellMarkProb ) 
					{
						hiddenBoard[ i, j ] = true;
						rowSums[ i ] += j + 1;
						colSums[ j ] += i + 1;
					}
				}
			}
            // This is the main game-play loop.
            
            bool gameNotQuit = true;
            while( gameNotQuit )
            {
                Console.Clear( );
                WriteLine( );
                WriteLine( "    Play Kakurasu!" );
                WriteLine( );

                // Display the game board.
                // TO DO: Update code to correctly display the game state.

      
          
           
                for( int row = 0; row < boardSize; row ++ )
                {
					if( row == 0 )
                    {
                        Write( "        " );
                        for( int col = 0; col < boardSize; col ++ ) //writes letters at the top
                            Write( "  {0} ", letters[ col ] );
                        WriteLine( );

                        Write( "        " );
                        for( int col = 0; col < boardSize; col ++ ) //writes the numbers at the top
                            Write( " {0,2} ", col + 1 );
                        WriteLine( );

                        Write( "        +" );
                        for( int col = 0; col < boardSize - 1; col ++ ) // prints top side of game board
                            Write( "---+" );
                        WriteLine( "---+" );
                    }

                    Write( "   {0} {1,2} |", letters[ row ], row + 1 );

                    for( int col = 0; col < boardSize; col ++ )
                    {
                        if( userBoard[ row, col ] ) Write( " X |" ); //prints x's where user selects row-column pair
                        else                       Write( "   |" );
                    }
                    WriteLine( $" {rowSumsUser[ row ]} {rowSums[ row ] } " ); //prints the desired row value and the actual row value

                    if( row < boardSize - 1 )
                    {
                        Write( "        +" );
                        for( int col = 0; col < boardSize - 1; col ++ )
                            Write( "---+" );
                        WriteLine( "---+" );
                    }
                    else
                    {
                        Write( "        +" );
                        for( int col = 0; col < boardSize - 1; col ++ )
                            Write( "---+" );
                        WriteLine( "---+" );

                        Write( "         " );
                        for( int col = 0; col < boardSize; col ++ ) //prints the actual column value
                            Write( $" {colSumsUser[ col ],2} " );
                        WriteLine( );
                        
                        Write( "         " ); 
                        for( int col = 0; col < boardSize; col ++ ) //prints the desired column value
                            Write( $" {(colSums[ col ]),2} " );
                        WriteLine( );
                        
					}
				}
                // Get the next move.
                bool notWon = true; 
				
				for (int i = 0; i < boardSize; i ++)
				{
					if (rowSumsUser [ i ] != rowSums [ i ] || colSumsUser [ i ] != colSums [ i ])
					{
						notWon = false; //requests another turn if the actual column/row values don't match the desired column/row values
					}
				}
				if (notWon)
				{
					WriteLine( "Congradulations! You won!" );
					break; //breaks game if values do match
				}
				
                WriteLine( );
                WriteLine( "   Toggle cells to match the row and column sums." );
                Write(     "   Enter a row-column letter pair in the form 'a,b' or 'quit': " );
                string s = ReadLine( );
      
                if( s == "quit" ) gameNotQuit = false;
                else
                {
                    // TO DO: Update the game state based on the user's response.
                    //        Anything invalid can just be quietly ignored.
                    
                    string[] sArray = s.Split(','); //splits the inputed values into two arrays
                    
                    if (sArray.Length == 2) //makes sure the column and row arrays are only accessed if the user input is two letters
                    {
						int indexRow = IndexOf(letters, sArray[0]); 
						int indexCol = IndexOf(letters, sArray[1]);
						
						if (indexRow != -1 && indexCol != -1 && indexRow < boardSize && indexCol < boardSize) //quietly ignores invalid inputs
						{
							userBoard[indexRow,indexCol] = !userBoard[indexRow,indexCol];

							if (userBoard[indexRow,indexCol] == true) //adds associated column/row value when a combination is set to true
							{
								rowSumsUser[indexRow] = rowSumsUser[indexRow] + (indexCol+1);
								colSumsUser[indexCol] = colSumsUser[indexCol] + (indexRow+1);
							}
							else //removes associated column/row value when a combination is set to not true
							{
								rowSumsUser[indexRow] = rowSumsUser[indexRow] - (indexCol+1);
								colSumsUser[indexCol] = colSumsUser[indexCol] - (indexRow+1);
							}
						}
					}
                }
            }

            WriteLine( );
        }
    }
}
