using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMatrix {
	// This iterator iterates over the upper triangular matrix.
	// This is done in a row major fashion, starting with [0][0],
	// and includes all N*N elements of the matrix.
	public class UTMatrixEnumerator : IEnumerator {
	       public UTMatrix matrix;
	       public int currRow;
	       public int currCol;
	       public int N;
	       
	       public UTMatrixEnumerator(UTMatrix matrix) {
	       	      this.matrix = matrix;
		      N = matrix.getSize();
		      Reset();      
	       }

	       public void Reset() {
	       	      currRow = 0;
		      currCol = -1;
	       }

	       public bool MoveNext() {
	       	      currCol++;

		      if (currCol >= N) {
		      	 currCol = 0;
			 currRow++;
		      }
		      
		      if (currRow >= N) {
	       	      	 return false;
		      }

		      return true;
	       }

		object IEnumerator.Current {
			get {
			    return matrix;
			}
		}

		public int Current {
			get {
				//try {
				return matrix.get(currRow, currCol);
				//}
				//catch (IndexOutOfRangeException) {
				//	throw new InvalidOperationException();
				//}
			}
		}
	}

	public class UTMatrix : IEnumerable {
		// Must use a one dimensional array, having minumum size.
		public int[] data;
		private int size;

		// Construct an NxN Upper Triangular Matrix, initialized to 0
		// Throws an error if N is non-sensical.
		public UTMatrix(int N) {
		       if (N < 0) {
		       	  throw new System.ArgumentException("Invalid N value");
		       }

		       size = N;
		       int elements = N * (N + 1) / 2;
		       data = new int[elements];
		}

		// Returns the size of the matrix
		public int getSize() {
			return size;
		}

		// Returns an upper triangular matrix that is the summation of a & b.
		// Throws an error if a and b are incompatible.
		public static UTMatrix operator + (UTMatrix a, UTMatrix b) {
		       int n = a.getSize();
		       int m = b.getSize();

		       if (n != m) {
		       	  throw new System.ArgumentException("Incompatible matrices");
		       }
		       
		       UTMatrix newM = new UTMatrix(n);		       
		       for (int i = 0; i < n; i++) {
		       	   for (int j = 0; j < n; j++) {
			       if (i <= j) {
			       	  int val = a.get(i, j) + b.get(i, j);
			       	  newM.set(i, j, val);
			       }			      
			   }
		       }

			return newM;
		}

		// Set the value at index [r][c] to val.
		// Throws an error if [r][c] is an invalid index to alter.
		public void set(int r, int c, int val) {
		       if (r < 0 || c < 0 || r >= size || c >= size) {
		       	  throw new System.ArgumentException("Invalid index");
		       }

		       int loc = accessFunc(r, c);
		       data[loc] = val;
		}

		// Returns the value at index [r][c]
		// Throws an error if [r][c] is an invalid index
		public int get(int r, int c) {
		       if (r < 0 || c < 0 || r >= size || c >= size) {
		       	  throw new System.ArgumentException("Invalid index");
		       }

		       if (r > c) {
		       	  return 0;
		       }

		       int loc = accessFunc(r, c);
		       return data[loc];
		}

		// Returns the position in the 1D array for [r][c].
		// Throws an error if [r][c] is an invalid index
		public int accessFunc(int r, int c) {
		       if (r < 0 || c < 0 || r >= size || c >= size) {
		       	  throw new System.ArgumentException("Invalid index");
		       }
		       
		       return ( (r * size + c) - ( ( r * (r + 1) ) / 2 ) );
		}

		// Returns an enumerator for an upper triangular matrix
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public UTMatrixEnumerator GetEnumerator() {
			return new UTMatrixEnumerator(this);
		}


		public static void Main(String [] args) {
			const int N = 2;
			UTMatrix ut1 = new UTMatrix(N);
			UTMatrix ut2 = new UTMatrix(N);
			for (int r=0; r<N; r++) {
				ut1.set(r, r, 1);
				for (int c=r; c<N; c++) {
					ut2.set(r, c, 1);
				}
			}
			UTMatrix ut3 = ut1 + ut2;
			UTMatrixEnumerator ie = ut3.GetEnumerator();
			while (ie.MoveNext()) {
				Console.Write(ie.Current + " ");
			}
			Console.WriteLine();
			foreach (int v in ut3) {
				Console.Write(v + " ");
			}
			Console.WriteLine();
		}
	}
}
