using static System.Console;
using System;
using System.Text;

namespace Bme121
{
	class YahtzeeDice
	{
		Random rGen = new Random( );
		
		int[ ] fiveFaces = new int[ 5 ];

		public void Roll( )
		{
			
			for( int i = 0; i < fiveFaces.Length; i++ )
			{
				if( fiveFaces[i] == 0 ) 
				{
					fiveFaces[i] = rGen.Next(1,7); //generates random number for each face value
				}
			}
		}
		
		
		public void Unroll( string faces )
		{
			if( faces != "all" )
			{
				for( int i = 0; i < faces.Length; i++ )
				{
					int num = int.Parse( faces.Substring( i,1 ) );
					bool rightNum = false;
					for( int index = 0; index < fiveFaces.Length; index++ )
					{
						if( num == fiveFaces[index] && !rightNum && fiveFaces[index] != 0 ) // sets values to zero once their selected
						{
							fiveFaces[index] = 0;
							rightNum = true;
						}
					}
				}
			}
			else 
			{
				for( int i = 0; i < fiveFaces.Length; i++ )
				{
					fiveFaces[i] = 0; // sets all values to zero if user inputs 'all'
				}
			}
		}
		
		public int Sum( )
		{
			int sum = 0;
			
			for( int i = 0; i < fiveFaces.Length; i++ )
			{
				sum += fiveFaces[i]; //adds together all face values
			}
			
			return sum;
		}
		
		public int Sum( int face )
		{
			int newFace = 0;
			
			for( int i = 0; i < fiveFaces.Length; i++ )
			{
				if( fiveFaces[i] == face )
				{
					newFace += fiveFaces[i];// adds togetehr face values of the same value
				}
			}
				 
			return newFace;
		}
		
		public bool IsRunOf( int length ) 
		{ 
			for( int i = 1; i < 8 - length; i++)
			{
				bool incompleteRun = false; // checks if run is broken
				{
					for( int j = i; j < i + length; j++ )
					{
						if ( !incompleteRun )
						{
							bool number = false; //checks if the next number is in the fiveFaces array
							for( int k = 0; k < 5; k++ )
							{
								if( fiveFaces[k] == j )
								{
									number = true; 
								}
							}
							if( !number )
							{
								incompleteRun = true;
							}
							else if( j == i + length - 1)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		
		public bool IsSetOf( int size ) 
		{ 
			for( int i = 1; i < 7; i++ )
			{
				if( Sum(i) >= size * i) //checks if the number being searched is repeated size number of times
				{
					return true;
				}
			}
			return false;
		}
		
		public bool IsFullHouse( ) 
		{ 
			bool doubles = false; //checks if a number is repeated twice
			bool triples = false; // checks if a number is repeated 3 times
			for( int i = 1; i < 7; i++ )
			{
				if( Sum(i) == 3 * i )
				{
					triples = true;
				}
				else if ( Sum(i) == 2 * i )
				{
					doubles = true;
				}
			}
			
			if( doubles == true && triples == true ) // if there is a set of 2 and a set of 3, there is a full house
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override string ToString( )
		{
			string diceFaces = "";
			for( int i = 0; i < fiveFaces.Length; i ++ )
			{
				diceFaces += fiveFaces[i] + " ";
			}
			return diceFaces;
		}
	}
}
